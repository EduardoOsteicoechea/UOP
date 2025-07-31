

namespace UOP.Revit.Units.Revit2024
{
	public static class BoundingBoxIntersectsFilter
	{
		public static Autodesk.Revit.DB.BoundingBoxIntersectsFilter CreateForbiddenVolumeIntersectionFilter
		(
			BoundingBoxIntersectsFilterArguments arguments
		)
		{
			return new Autodesk.Revit.DB.BoundingBoxIntersectsFilter(arguments.Outline);
		}

	}
}
