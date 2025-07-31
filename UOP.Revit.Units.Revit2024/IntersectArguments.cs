

namespace UOP.Revit.Units.Revit2024
{

	public class IntersectGetXYZFromReferencePlanePairIntersectionArguments
	{
		public Autodesk.Revit.DB.ReferencePlane Plane1 { get; set; }
		public Autodesk.Revit.DB.ReferencePlane Plane2 { get; set; }
		public IntersectGetXYZFromReferencePlanePairIntersectionArguments
		(
			Autodesk.Revit.DB.ReferencePlane plane1,
			Autodesk.Revit.DB.ReferencePlane plane2
		)
		{
				Plane1 = plane1;
				Plane2 = plane2;
		}
	}
}
