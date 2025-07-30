using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOP
{
	public class WORKFLOW
	{
		private string InitializationTime { get; set; } = DateTime.Now.ToString("yyyyMMdd_HHmm");
		private string DocumentationDirectoryPath { get; set; } = System.IO.Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
			"UOP",
			"Executions"
		);
		private string FrameworkErrorsDirectoryPath { get; set; } = System.IO.Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
			"UOP",
			"FrameworkErrors"
		);
		private string DocumentationTimedDirectoryPath { get; set; }
		private bool MustDocument { get; set; } = true;
		private int ActionCounter { get; set; } = 0;
		public List<RESULT> Results { get; set; } = new List<RESULT>();
		public bool PreviousMethodFailed { get; set; }
		private DOCUMENTER Documenter { get; set; }

		public WORKFLOW
		(
			string documentationDirectoryPath = "",
			bool mustDocument = true
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				if (!string.IsNullOrEmpty(documentationDirectoryPath))
				{
					DocumentationDirectoryPath = documentationDirectoryPath;
				}

				if (!mustDocument)
				{
					MustDocument = false;
				}

				DocumentationTimedDirectoryPath = Path.Combine(DocumentationDirectoryPath, InitializationTime);
				CreateRequiredDirectories();
				Documenter = new DOCUMENTER(DocumentationTimedDirectoryPath);
			});
		}

		public MethodReturnType Run<MethodReturnType, ArgumentsObject>
		(
			Func<ArgumentsObject, MethodReturnType> action,
			ArgumentsObject actionArguments,
			string methodDescriptiveName = ""
		)
		{
			return WRAPPER.ManagedCommand<MethodReturnType>(() =>
			{
				MethodReturnType result = default;

				if (!PreviousMethodFailed)
				{
					var uopMethod = new METHOD<MethodReturnType, ArgumentsObject>
					(
						Documenter,
						Results,
						methodDescriptiveName,
						PreviousMethodFailed,
						InitializationTime,
						ActionCounter,
						DocumentationTimedDirectoryPath,
						action,
						actionArguments,
						MustDocument
					);

					ActionCounter++;

					DocumentExceptionIfAny<MethodReturnType, ArgumentsObject>(uopMethod);

					ValidateIfMethodFailed<MethodReturnType, ArgumentsObject>(uopMethod);

					return result;
				}

				return result;
			});
		}

		private void CreateRequiredDirectories()
		{
			WRAPPER.ManagedCommand(() =>
			{
				CreateDirectoryIfPossible(DocumentationDirectoryPath);
				CreateDirectoryIfPossible(DocumentationTimedDirectoryPath);
				CreateDirectoryIfPossible(FrameworkErrorsDirectoryPath);
			});
		}

		private void CreateDirectoryIfPossible(string path)
		{
			WRAPPER.ManagedCommand(() =>
			{
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
			});
		}

		private void DocumentExceptionIfAny<MethodReturnType, ArgumentsObject>
		(
			METHOD<MethodReturnType, ArgumentsObject> uopMethod
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				if (MustDocument)
				{
					if (uopMethod.ExecutionResultState == METHODSTATE.Failure)
					{
						Documenter.Document(
							$"___{uopMethod.DocumentationFileName}_{"ERRORS"}",
							uopMethod.ResultValue
						);
					}
				}
			});
		}

		private void ValidateIfMethodFailed<MethodReturnType, ArgumentsObject>
		(
			METHOD<MethodReturnType, ArgumentsObject> uopMethod
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				if (uopMethod.ExecutionResultState == METHODSTATE.Failure)
				{
					PreviousMethodFailed = true;
				}
			});
		}

		public void DocumentResults()
		{
			WRAPPER.ManagedCommand(() =>
			{
				Documenter.Document(
					"____RESULTS",
					Results
				);
			});
		}
	}
}
