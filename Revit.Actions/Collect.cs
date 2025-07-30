using Autodesk.Revit.DB;

namespace Revit.Actions
{
	public static class Collect
	{
		public static Autodesk.Revit.DB.FilteredElementCollector InstancesByType<T>
		(
			CollectInstancesByTypeArguments arguments
		)
		{
			var collector = new FilteredElementCollector(arguments.RevitDocument)
				.OfClass(typeof(T))
				.WhereElementIsNotElementType();

			return collector;
		}
	}
	public class CollectInstancesByTypeArguments
	{
		public Autodesk.Revit.DB.Document RevitDocument { get; set; }
		public CollectInstancesByTypeArguments(Autodesk.Revit.DB.Document document)
		{
			RevitDocument = document;
		}
	}
}
