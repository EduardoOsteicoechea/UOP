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
			return WRAPPER.ManagedCommand<T>(() =>
			{
				Autodesk.Revit.DB.Element a = arguments.Item;

				if (arguments.Item is T typedItem)
				{
					return typedItem;
				}

				return default(T);
			});
		}

		public static List<T> ElementsToType<T>
		(
			CastElementsToTypeArguments arguments
		)
		{
			return WRAPPER.ManagedCommand<List<T>>(() =>
			{
				var result = arguments.Items.Cast<T>().ToList();

				return result;
			});
		}

		public static List<T> CollectorItemsToList<T>
		(
			CastCollectorItemsToListArguments arguments
		)
		{
			return WRAPPER.ManagedCommand<List<T>>(() =>
			{
				var result = arguments.Collector.Cast<T>().ToList();

				return result;
			});
		}

		public static List<Autodesk.Revit.DB.Element> ToElements
		(
			CastCollectorItemsToListArguments arguments
		)
		{
			return WRAPPER.ManagedCommand<List<Autodesk.Revit.DB.Element>>(() =>
			{
				var result = arguments.Collector.ToElements().ToList();

				return result;
			});
		}
		public static List<Autodesk.Revit.DB.ElementId> ToElementIds
		(
			CastCollectorItemsToListArguments arguments
		)
		{
			return WRAPPER.ManagedCommand<List<Autodesk.Revit.DB.ElementId>>(() =>
			{
				var result = arguments.Collector.ToElementIds().ToList();

				return result;
			});
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

	public class CastElementToTypeArguments
	{
		public Autodesk.Revit.DB.Element Item { get; set; }
		public CastElementToTypeArguments(Autodesk.Revit.DB.Element item)
		{
			Item = item;
		}
	}

	public class CastElementsToTypeArguments
	{
		public List<Autodesk.Revit.DB.Element> Items { get; set; }
		public CastElementsToTypeArguments(List<Autodesk.Revit.DB.Element> items)
		{
			Items = items;
		}
	}
}
