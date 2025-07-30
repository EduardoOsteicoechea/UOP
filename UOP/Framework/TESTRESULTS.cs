using System.Linq;

namespace UOP
{
	public static class TESTRESULTS
	{

		public static TESTRESULT GenerictFailure()
		{
			return new TESTRESULT() { PassesTest = false, ResultObvervation = $"Failed to complete action" };
		}
		public static TESTRESULT GenericSuccess(dynamic result)
		{
			return new TESTRESULT() { PassesTest = true, ResultObvervation = $"Successfully executed action (result value: '{result}')" };
		}

		public static TESTRESULT ExceptionFailure(System.Exception exception)
		{
			return new TESTRESULT() { PassesTest = false, ResultObvervation = $"{exception.Message}\n\n{exception.StackTrace}" };
		}
		public static TESTRESULT GenerictFailureSimpleValue(dynamic result)
		{
			return new TESTRESULT() { PassesTest = false, ResultObvervation = $"Failed to complete action (result value: '{result}')" };
		}
		public static TESTRESULT GenerictSuccessSimpleValue(dynamic result)
		{
			return new TESTRESULT() { PassesTest = true, ResultObvervation = $"Successfully executed action (result value: '{result}')" };
		}
		public static TESTRESULT GenericFailureCollection(dynamic result)
		{
			return new TESTRESULT() { PassesTest = false, ResultObvervation = $"Failed to generate list (result value: '{result}')" };
		}
		public static TESTRESULT GenericSuccessCollection(dynamic result)
		{
			return new TESTRESULT() { PassesTest = true, ResultObvervation = $"Successfully generated list (result value: '{result}'; count: '{result?.Count}')" };
		}
		public static TESTRESULT GenericFailureCollector(dynamic result)
		{
			return new TESTRESULT() { PassesTest = false, ResultObvervation = $"Failed to generate collector (result value: '{result}')" };
		}
		public static TESTRESULT GenericSuccessCollector(Autodesk.Revit.DB.FilteredElementCollector result)
		{
			return new TESTRESULT() { PassesTest = true, ResultObvervation = $"Successfully generated collector (result value: '{result}'; count: '{result.Count()}')" };
		}

		public static TESTRESULT GenericFailureRevitElement(dynamic result)
		{
			return new TESTRESULT() { PassesTest = false, ResultObvervation = $"Failed to generate list (result value: '{result}')" };
		}
		public static TESTRESULT GenericSuccessRevitElement(dynamic result)
		{
			return new TESTRESULT() { PassesTest = true, ResultObvervation = $"Successfully generated list (result value: '{result}'; Id: '{result.Id}')" };
		}




	}
}
