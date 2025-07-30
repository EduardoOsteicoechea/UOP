using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UOP
{
	internal static class TESTS
	{

		public static TESTRESULT CastCollectorItemsToList
		(
			dynamic argument
		)
		{
			var result = argument;

			if (result == null)
			{
				return TESTRESULTS.GenerictFailure();
			}
			else
			{
				return TESTRESULTS.GenericSuccessCollection(result);
			}
		}


		public static TESTRESULT SimpleValue
		(
			bool argument
		)
		{
			var result = argument;

			if (result == null)
			{
				return TESTRESULTS.GenerictFailure();
			}
			else
			{
				return TESTRESULTS.GenericSuccess(result);
			}
		}


		public static TESTRESULT NullableBool
		(
			bool? argument
		)
		{
			if (argument == null)
			{
				return TESTRESULTS.GenerictFailure();
			}
			else
			{
				return TESTRESULTS.GenericSuccess(argument);
			}
		}


		public static TESTRESULT ContainsItems
		(
			bool argument
		)
		{
			var result = argument;

			if (result == null)
			{
				return TESTRESULTS.GenerictFailure();
			}
			if (result == default)
			{
				return TESTRESULTS.GenerictFailure();
			}
			else
			{
				return TESTRESULTS.GenericSuccess(result);
			}
		}


		public static TESTRESULT RevitApiCollector
		(
			FilteredElementCollector result
		)
		{
			if (result == default)
			{
				return TESTRESULTS.GenericFailureCollector(result);
			}
			else if (result == null)
			{
				return TESTRESULTS.GenericFailureCollector(result);
			}
			else
			{
				return TESTRESULTS.GenericSuccessCollector(result);
			}
		}


		public static TESTRESULT SimpleValue
		(
			dynamic argument
		)
		{
			var result = argument;

			if (result == null)
			{
				return TESTRESULTS.GenerictFailure();
			}
			else
			{
				return TESTRESULTS.GenericSuccess(result);
			}
		}

		public static TESTRESULT SimpleNullableDoubleValue
		(
			double? result
		)
		{
			if (result == null)
			{
				return TESTRESULTS.GenerictFailure();
			}
			else
			{
				return TESTRESULTS.GenericSuccess(result);
			}
		}


		public static TESTRESULT RevitApiElement
		(
			dynamic argument
		)
		{
			var result = argument;

			if (result == null)
			{
				return TESTRESULTS.GenerictFailure();
			}
			else
			{
				return TESTRESULTS.GenericSuccess(result);
			}
		}


		public static TESTRESULT List
		(
			dynamic argument
		)
		{
			var result = argument;

			if (result == null)
			{
				return TESTRESULTS.GenerictFailure();
			}
			else
			{
				return TESTRESULTS.GenericSuccessCollection(result);
			}
		}


	}
}
