using System.Collections.Generic;
using UOP;

namespace Revit.Actions
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
			WRAPPER.ManagedCommand(() =>
			{
				Plane1 = plane1;
				Plane2 = plane2;
			});
		}
	}




}
