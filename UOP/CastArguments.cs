using System.Collections.Generic;
using System.Linq;
using UOP;

namespace Revit.Actions
{

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
