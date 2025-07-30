//using Autodesk.Revit.DB;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace UOP
//{
//	internal static class TESTS
//	{

//		public static TESTRESULT<T1, T2> CastCollectorItemsToList<T1, T2>
//		(
//			dynamic argument
//		)
//		{
//			var result = argument;

//			if (result == null)
//			{
//				return TESTRESULTS.GenerictFailure<T1, T2>();
//			}
//			else
//			{
//				return TESTRESULTS.GenericSuccessCollection<T1, T2>(result);
//			}
//		}


//		public static TESTRESULT<T1, T2> SimpleValue<T1, T2>
//		(
//			bool argument
//		)
//		{
//			var result = argument;

//			if (result == null)
//			{
//				return TESTRESULTS.GenerictFailure<T1, T2>();
//			}
//			else
//			{
//				return TESTRESULTS.GenericSuccess<T1, T2>(result);
//			}
//		}


//		public static TESTRESULT<T1, T2> NullableBool<T1, T2>
//		(
//			bool? argument
//		)
//		{
//			if (argument == null)
//			{
//				return TESTRESULTS.GenerictFailure<T1, T2>();
//			}
//			else
//			{
//				return TESTRESULTS.GenericSuccess<T1, T2>(argument);
//			}
//		}


//		public static TESTRESULT<T1, T2> ContainsItems<T1, T2>
//		(
//			bool argument
//		)
//		{
//			var result = argument;

//			if (result == null)
//			{
//				return TESTRESULTS.GenerictFailure<T1, T2>();
//			}
//			if (result == default)
//			{
//				return TESTRESULTS.GenerictFailure<T1, T2>();
//			}
//			else
//			{
//				return TESTRESULTS.GenericSuccess<T1, T2>(result);
//			}
//		}


//		public static TESTRESULT<T1, T2> RevitApiCollector<T1, T2>
//		(
//			FilteredElementCollector result
//		)
//		{
//			if (result == default)
//			{
//				return TESTRESULTS.GenericFailureCollector<T1, T2>(result);
//			}
//			else if (result == null)
//			{
//				return TESTRESULTS.GenericFailureCollector<T1, T2>(result);
//			}
//			else
//			{
//				return TESTRESULTS.GenericSuccessCollector<T1, T2>(result);
//			}
//		}


//		public static TESTRESULT<T1, T2> SimpleValue<T1, T2>
//		(
//			dynamic argument
//		)
//		{
//			var result = argument;

//			if (result == null)
//			{
//				return TESTRESULTS.GenerictFailure<T1, T2>();
//			}
//			else
//			{
//				return TESTRESULTS.GenericSuccess<T1, T2>(result);
//			}
//		}

//		public static TESTRESULT<T1, T2> SimpleNullableDoubleValue<T1, T2>
//		(
//			double? result
//		)
//		{
//			if (result == null)
//			{
//				return TESTRESULTS.GenerictFailure<T1, T2>();
//			}
//			else
//			{
//				return TESTRESULTS.GenericSuccess<T1, T2>(result);
//			}
//		}


//		public static TESTRESULT<T1, T2> RevitApiElement<T1, T2>
//		(
//			dynamic argument,
//			Type returnType,
//			Type argumentsType
//		)
//		{
//			var result = argument;

//			if (result == null)
//			{
//				return new TESTRESULT { PassesTest = false, ResultObvervation = $"Failed to complete action" };
//			}
//			else
//			{
//				return TESTRESULTS.GenericSuccess<T1, T2>(result);
//			}
//		}


//		public static TESTRESULT<T1, T2> List<T1, T2>
//		(
//			dynamic argument
//		)
//		{
//			var result = argument;

//			if (result == null)
//			{
//				return TESTRESULTS.GenerictFailure<T1, T2>();
//			}
//			else
//			{
//				return TESTRESULTS.GenericSuccessCollection<T1, T2>(result);
//			}
//		}


//	}
//}
