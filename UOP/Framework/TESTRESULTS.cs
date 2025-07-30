using System.Linq;

namespace UOP
{
	public static class TESTRESULTS
	{

		public static TESTRESULT<T1, T2> GenerictFailure<T1,T2>()
		{
			return new TESTRESULT<T1, T2>() { PassesTest = false, ResultObvervation = $"Failed to execute action" };
		}
		public static TESTRESULT<T1, T2> GenericSuccess<T1,T2>(dynamic result)
		{
			return new TESTRESULT<T1, T2>() { PassesTest = true, ResultObvervation = $"Successfully executed action (result value: '{result}')" };
		}

		public static TESTRESULT<T1, T2> ExceptionFailure<T1,T2>(System.Exception exception)
		{
			return new TESTRESULT<T1, T2>() { PassesTest = false, ResultObvervation = $"{exception.Message}\n\n{exception.StackTrace}" };
		}
		public static TESTRESULT<T1, T2> GenerictFailureSimpleValue<T1,T2>(dynamic result)
		{
			return new TESTRESULT<T1, T2>() { PassesTest = false, ResultObvervation = $"Failed to execute action (result value: '{result}')" };
		}
		public static TESTRESULT<T1, T2> GenerictSuccessSimpleValue<T1,T2>(dynamic result)
		{
			return new TESTRESULT<T1, T2>() { PassesTest = true, ResultObvervation = $"Successfully executed action (result value: '{result}')" };
		}
		public static TESTRESULT<T1, T2> GenericFailureCollection<T1,T2>(dynamic result)
		{
			return new TESTRESULT<T1, T2>() { PassesTest = false, ResultObvervation = $"Failed to generated list (result value: '{result}')" };
		}
		public static TESTRESULT<T1, T2> GenericSuccessCollection<T1,T2>(dynamic result)
		{
			return new TESTRESULT<T1, T2>() { PassesTest = true, ResultObvervation = $"Successfully generated list (result value: '{result}'; count: '{result?.Count}')" };
		}
		public static TESTRESULT<T1, T2> GenericFailureCollector<T1,T2>(dynamic result)
		{
			return new TESTRESULT<T1, T2>() { PassesTest = false, ResultObvervation = $"Failed to generated collector (result value: '{result}')" };
		}
		public static TESTRESULT<T1, T2> GenericSuccessCollector<T1,T2>(Autodesk.Revit.DB.FilteredElementCollector result)
		{
			return new TESTRESULT<T1, T2>() { PassesTest = true, ResultObvervation = $"Successfully generated collector (result value: '{result}'; count: '{result.Count()}')" };
		}
		public static TESTRESULT<T1, T2> GenericFailureRevitElement<T1,T2>(dynamic result)
		{
			return new TESTRESULT<T1, T2>() { PassesTest = false, ResultObvervation = $"Failed to generated list (result value: '{result}')" };
		}
		public static TESTRESULT<T1, T2> GenericSuccessRevitElement<T1,T2>(dynamic result)
		{
			return new TESTRESULT<T1, T2>() { PassesTest = true, ResultObvervation = $"Successfully generated list (result value: '{result}'; Id: '{result.Id}')" };
		}
	}
}
