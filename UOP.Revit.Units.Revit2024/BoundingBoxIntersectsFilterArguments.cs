

namespace UOP.Revit.Units.Revit2024
{
	public class BoundingBoxIntersectsFilterArguments
	{
		public Autodesk.Revit.DB.Outline Outline {  get; set; }
		public BoundingBoxIntersectsFilterArguments(Autodesk.Revit.DB.Outline outline) 
		{
			Outline = outline;
		}
	}
}
