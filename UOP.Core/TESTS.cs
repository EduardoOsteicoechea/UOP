

namespace UOP.Core
{
	internal static class TESTS
	{
		public static TESTRESULT NonNull
		(
			dynamic result
		)
		{
			if (result == null)
			{
				return new TESTRESULT() { PassesTest = false, ResultObvervation = $"The action returned a null value." };
			}
			else
			{
				return new TESTRESULT() { PassesTest = true, ResultObvervation = $"Successfully executed action (result value: '{result}')" };
			}
		}

		public static TESTRESULT NonNull
		(
			bool result
		)
		{
			if (result == null)
			{
				return new TESTRESULT() { PassesTest = false, ResultObvervation = $"The action returned a null value." };
			}
			else
			{
				return new TESTRESULT() { PassesTest = true, ResultObvervation = $"Successfully executed action (result value: '{result}')" };
			}
		}



	}
}
