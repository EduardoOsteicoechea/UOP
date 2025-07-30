using System.Collections.Generic;
using System.Linq;

namespace Revit.Actions
{
	public static class Cast
	{
		public static List<T> CollectorItemsToList<T>
		(
			CastCollectorItemsToListArguments arguments
		)
		{
			var result = arguments.Collector.Cast<T>().ToList();

			return result;
		}
		public static List<Autodesk.Revit.DB.Element> ToElements
		(
			CastCollectorItemsToListArguments arguments
		)
		{
			var result = arguments.Collector.ToElements().ToList();

			return result;
		}
		public static List<Autodesk.Revit.DB.ElementId> ToElementIds
		(
			CastCollectorItemsToListArguments arguments
		)
		{
			var result = arguments.Collector.ToElementIds().ToList();

			return result;
		}
	}

	public class CastCollectorItemsToListArguments
	{
		public Autodesk.Revit.DB.FilteredElementCollector Collector { get; set; }
		public CastCollectorItemsToListArguments(Autodesk.Revit.DB.FilteredElementCollector collector)
		{
			Collector = collector;
		}
	}
}
