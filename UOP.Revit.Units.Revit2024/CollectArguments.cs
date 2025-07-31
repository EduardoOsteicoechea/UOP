

namespace UOP.Revit.Units.Revit2024
{
	public class CollectInstancesByTypeArguments
	{
		public Autodesk.Revit.DB.Document RevitDocument { get; set; }
		public CollectInstancesByTypeArguments(Autodesk.Revit.DB.Document document)
		{
			RevitDocument = document;
		}
	}
}
