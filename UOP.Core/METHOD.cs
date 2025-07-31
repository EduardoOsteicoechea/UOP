using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using UOP.Common.Documenter;

namespace UOP.Core
{
	public class METHOD<MethodReturnType, ArgumentsObject>
	{
		public string MethodCounter { get; set; }
		public string MethodTime { get; set; }
		public string MethodName { get; set; } = "";
		public string MethodDeclaringTypeName { get; set; } = "";
		public string MethodDescriptiveName { get; set; } = "";
		public string MethodNamespace { get; set; } = "";
		public MethodReturnType ResultValue { get; set; } = default;
		public string ResultTypeName { get; set; } = default;
		public ArgumentsObject MethodArguments { get; set; }
		public Func<ArgumentsObject, MethodReturnType> MethodAction { get; set; }
		public METHODSTATE ExecutionResultState { get; set; } = METHODSTATE.NotExecuted;
		public TESTRESULT TestResult { get; set; }

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
		public bool MustDocument { get; set; } = true;

		[Newtonsoft.Json.JsonIgnore]
		public bool MustTest { get; set; } = true;

		[Newtonsoft.Json.JsonIgnore]
		public Newtonsoft.Json.JsonSerializerSettings SerializerSettings { get; set; }

		[Newtonsoft.Json.JsonIgnore]
		public DOCUMENTER MethodDocumenter { get; set; }


		public METHOD
		(
			WORKFLOW Workflow,
			DOCUMENTER Documenter,
			List<RESULT> Results,
			string methodDescriptiveName,
			bool LastMethodFailed,
			string intializationTime,
			int methodCounter,
			string documentationFileDirectoryPath,
			Func<ArgumentsObject, MethodReturnType> methodAction,
			ArgumentsObject methodArguments,
			bool mustDocument,
			bool mustTest
		)
		{
			WRAPPER.ManagedCommand(() =>
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
						mustDocument,
						mustDocument
					);

					RunMethod(Documenter, Results);

					DocumentMethod(Documenter, !string.IsNullOrEmpty(methodDescriptiveName));

					TestMethod(Workflow, Documenter);
				}
			});
		}

		private void InstantiateVariables
		(
			string methodDescriptiveName,
			string intializationTime,
			int methodCounter,
			string documentationFileDirectoryPath,
			Func<ArgumentsObject, MethodReturnType> methodAction,
			ArgumentsObject methodArguments,
			bool mustDocumentMethod,
			bool mustTest
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
				MethodDescriptiveName = $"{fileNamesBase}{methodDescriptiveName}";

				MustDocument = mustDocumentMethod;
				MustTest = mustTest;
			});
		}


		private void RunMethod
		(
			DOCUMENTER Documenter,
			List<RESULT> Results,
			bool mustDocumentPurposeNamedFile = true
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				try
				{
					ResultValue = MethodAction.Invoke(MethodArguments);

					ExecutionResultState = METHODSTATE.Success;

					UpdateResultsCollector(Results, MethodCounter, MethodDescriptiveName, ResultValue);
				}
				catch (Exception e)
				{
					ExecutionResultState = METHODSTATE.Failure;

					UpdateResultsCollector(Results, MethodCounter, MethodDescriptiveName, $"{e.Message}\n\n{e.StackTrace}");
				}
			});
		}


		private void DocumentMethod
		(
			DOCUMENTER Documenter,
			bool mustDocumentPurposeNamedFile = true
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				if
				(
					MustDocument &&
					!string.IsNullOrEmpty(DocumentationFileDirectoryPath) &&
					!string.IsNullOrEmpty(DocumentationFileName)
				)
				{
					Documenter.Document($"_{DocumentationFileName}", this);

					if (mustDocumentPurposeNamedFile)
					{
						Documenter.Document($"__{MethodDescriptiveName}", this);
					}
				}
			});
		}

		private void UpdateResultsCollector
		(
			List<RESULT> Results,
			string actionNumber,
			string actionDescription,
			dynamic resultValue
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				Results.Add(new RESULT()
				{
					ActionNumber = actionNumber,
					ActionDescription = actionDescription,
					Value = resultValue,
				});
			});
		}

		private void TestMethod
		(
			WORKFLOW workflow,
			DOCUMENTER Documenter
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				if (MustTest)
				{
					TestResult = TESTS.NonNull(ResultValue);

					TestResult.MethodName = MethodName;
					TestResult.MethodDescriptiveName = MethodDescriptiveName;
					TestResult.ResultValue = ResultValue;
					TestResult.ResultTypeName = ResultTypeName;
					TestResult.MethodTime = MethodTime;
					TestResult.MethodDeclaringTypeName = MethodDeclaringTypeName;
					TestResult.MethodNamespace = MethodNamespace;
					TestResult.MethodDeclaringTypeName = MethodTime;
					TestResult.MethodArguments = MethodArguments;

					Documenter.DocumentTest($"{DocumentationFileName}", TestResult);

					if (!TestResult.PassesTest)
					{
						ExecutionResultState = METHODSTATE.Failure;

						MessageBox.Show($"{TestResult.ResultObvervation}\n\nFailed at: {MethodDescriptiveName}", "", MessageBoxButton.OK, MessageBoxImage.Error);

						workflow.DocumentResults();
					}
				}
			});
		}
	}
}
