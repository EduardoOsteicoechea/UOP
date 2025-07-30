using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOP
{
	public class METHOD<MethodReturnType, ArgumentsObject>
	{
		public string MethodCounter { get; set; }
		public string MethodTime { get; set; }
		public string MethodName { get; set; } = "";
		public string MethodDeclaringTypeName { get; set; } = "";
		public string MethodPurposeOnAlgorithmFileName { get; set; } = "";
		public string MethodNamespace { get; set; } = "";
		public MethodReturnType ResultValue { get; set; } = default;
		public string ResultTypeName { get; set; } = default;
		public ArgumentsObject MethodArguments { get; set; }
		public Func<ArgumentsObject, MethodReturnType> MethodAction { get; set; }
		public METHODSTATE ExecutionResultState { get; set; } = METHODSTATE.NotExecuted;
		public TESTRESULT<MethodReturnType, ArgumentsObject> TestResult { get; set; }

		[Newtonsoft.Json.JsonIgnore]
		public string DocumentationFileName { get; set; } = "";

		[Newtonsoft.Json.JsonIgnore]
		public Type ResultType { get; set; } = default;

		[Newtonsoft.Json.JsonIgnore]
		public string DocumentationFileDirectoryPath { get; set; } = "";
		[Newtonsoft.Json.JsonIgnore]
		public string DocumentationFileTimedDirectoryPath { get; set; } = "";

		[Newtonsoft.Json.JsonIgnore]
		public StringBuilder MethodDocumentationStringBuilder { get; set; } = new StringBuilder();

		[Newtonsoft.Json.JsonIgnore]
		public bool MustDocumentMethod { get; set; } = true;

		[Newtonsoft.Json.JsonIgnore]
		public Newtonsoft.Json.JsonSerializerSettings SerializerSettings { get; set; }

		[Newtonsoft.Json.JsonIgnore]
		public DOCUMENTER MethodDocumenter { get; set; }


		public METHOD
		(
			DOCUMENTER Documenter,
			List<RESULT> Results,
			string methodDescriptiveName,
			bool LastMethodFailed,
			string intializationTime,
			int methodCounter,
			string documentationFileDirectoryPath,
			Func<ArgumentsObject, MethodReturnType> methodAction,
			ArgumentsObject methodArguments,
			bool mustDocumentMethod,
			Func<MethodReturnType, TESTRESULT<MethodReturnType, ArgumentsObject>> test = null
		)
		{
			if (!LastMethodFailed)
			{
				InstantiateVariables(
					methodDescriptiveName,
					intializationTime,
					methodCounter,
					documentationFileDirectoryPath,
					methodAction,
					methodArguments,
					mustDocumentMethod
				);

				RunAndDocumentMethodIfRequired(Documenter, Results, LastMethodFailed, !string.IsNullOrEmpty(methodDescriptiveName));

				if (test != null) 
				{
					TestResult = test.Invoke(ResultValue);

					TestResult.MethodAction = methodAction;
					TestResult.MethodArguments = methodArguments;

					Documenter.Document($"{DocumentationFileName}_TEST", TestResult);
				}
			}
		}

		private void InstantiateVariables
		(
			string methodDescriptiveName,
			string intializationTime,
			int methodCounter,
			string documentationFileDirectoryPath,
			Func<ArgumentsObject, MethodReturnType> methodAction,
			ArgumentsObject methodArguments,
			bool mustDocumentMethod
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				MethodTime = intializationTime;
				MethodAction = methodAction;
				MethodName = methodAction.Method.Name;
				MethodDeclaringTypeName = methodAction.Method.DeclaringType?.Name ?? "";
				MethodNamespace = methodAction.Method.DeclaringType?.Namespace ?? "";
				MethodArguments = methodArguments;
				ResultType = typeof(MethodReturnType);
				ResultTypeName = ResultType.FullName;

				MethodCounter = methodCounter.ToString().PadLeft(3, '0');

				string fileNamesBase = $"{MethodCounter.ToString().PadLeft(3, '0')}_";
				DocumentationFileDirectoryPath = documentationFileDirectoryPath;
				DocumentationFileTimedDirectoryPath = Path.Combine(DocumentationFileDirectoryPath, MethodTime);
				DocumentationFileName = $"{fileNamesBase}{MethodName}";
				MethodPurposeOnAlgorithmFileName = $"{fileNamesBase}{methodDescriptiveName}";

				MustDocumentMethod = mustDocumentMethod;
			});
		}


		private void RunAndDocumentMethodIfRequired
		(
			DOCUMENTER Documenter,
			List<RESULT> Results,
			bool MethodFailed,
			bool mustDocumentPurposeNamedFile = true
		)
		{
			try
			{
				ResultValue = MethodAction.Invoke(MethodArguments);

				ExecutionResultState = METHODSTATE.Success;

				Results.Add(new RESULT()
				{
					ActionNumber = MethodCounter,
					ActionDescription = MethodPurposeOnAlgorithmFileName,
					Value = ResultValue,
				});
			}
			catch (Exception e)
			{
				ExecutionResultState = METHODSTATE.Failure;

				MethodFailed = true;

				Results.Add(new RESULT()
				{
					ActionNumber = MethodCounter,
					ActionDescription = MethodPurposeOnAlgorithmFileName,
					Value = $"{e.Message}\n\n{e.StackTrace}",
				});
			}
			finally
			{
				if
				(
					MustDocumentMethod &&
					!string.IsNullOrEmpty(DocumentationFileDirectoryPath) &&
					!string.IsNullOrEmpty(DocumentationFileName)
				)
				{
					Documenter.Document($"_{DocumentationFileName}", this);

					if (mustDocumentPurposeNamedFile)
					{
						Documenter.Document($"__{MethodPurposeOnAlgorithmFileName}", this);
					}
				}
			}
		}
	}
}
