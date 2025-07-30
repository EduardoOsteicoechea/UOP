using System.Collections.Generic;
using System.Linq;
using UOP;

namespace Revit.Actions
{
	public static class Cast
	{
		public static T ElementToType<T>
		(
			CastElementToTypeArguments arguments
		)
		{
			Autodesk.Revit.DB.Element a = arguments.Item;

			if (arguments.Item is T typedItem)
			{
				return typedItem;
			}

			return default(T);
		}

		public static List<T> ElementsToType<T>
		(
			CastElementsToTypeArguments arguments
		)
		{
			var result = arguments.Items.Cast<T>().ToList();

			return result;
		}

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
}
