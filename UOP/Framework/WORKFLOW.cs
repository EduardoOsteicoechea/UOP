using System;
using System.Collections.Generic;
using System.IO;

namespace UOP
{
	public class WORKFLOW
	{
		private string InitializationTime { get; set; } = DateTime.Now.ToString("yyyyMMdd_HHmmss");
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
		private bool MustTest { get; set; } = true;
		private int ActionCounter { get; set; } = 1;
		public List<RESULT> Results { get; set; } = new List<RESULT>();
		public bool LastMethodFailed { get; set; }
		private DOCUMENTER Documenter { get; set; }

		public WORKFLOW
		(
			string documentationDirectoryPath = "",
			bool mustDocument = true,
			bool mustTest = true
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				if (!string.IsNullOrEmpty(documentationDirectoryPath))
				{
					DocumentationDirectoryPath = documentationDirectoryPath;
				}

				MustDocument = mustDocument;
				MustTest = mustTest;

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
				if (!LastMethodFailed)
				{
					var uopMethod = new METHOD<MethodReturnType, ArgumentsObject>
					(
						this,
						Documenter,
						Results,
						methodDescriptiveName,
						LastMethodFailed,
						InitializationTime,
						ActionCounter,
						DocumentationTimedDirectoryPath,
						action,
						actionArguments,
						MustDocument,
						MustTest
					);

					ActionCounter++;

					DocumentExceptionIfAny<MethodReturnType, ArgumentsObject>(uopMethod);

					ValidateIfMethodFailed<MethodReturnType, ArgumentsObject>(uopMethod);

					return uopMethod.ResultValue;
				}

				return default;
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
							$"____ERROR_{uopMethod.DocumentationFileName}",
							uopMethod
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
					LastMethodFailed = true;
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
