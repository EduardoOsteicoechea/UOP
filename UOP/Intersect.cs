using Autodesk.Revit.DB;
using Revit.Actions;
using System;

namespace UOP
{
	public static class Intersect
	{
		public static XYZ GetXYZFromReferencePlanePairIntersection
		(
			IntersectGetXYZFromReferencePlanePairIntersectionArguments arguments
		)
		{
			Autodesk.Revit.DB.XYZ p1 = arguments.Plane1.BubbleEnd;
			Autodesk.Revit.DB.XYZ v1 = arguments.Plane1.Direction.Normalize();

			Autodesk.Revit.DB.XYZ p2 = arguments.Plane2.BubbleEnd;
			Autodesk.Revit.DB.XYZ v2 = arguments.Plane2.Direction.Normalize();
			Autodesk.Revit.DB.XYZ p1_p2 = p2 - p1;

			Autodesk.Revit.DB.XYZ cross_v1_v2 = v1.CrossProduct(v2);

			double denominator = cross_v1_v2.DotProduct(cross_v1_v2);

			if (Math.Abs(denominator) < 1e-9)
			{
				return null;
			}

			double t = p1_p2.CrossProduct(v2).DotProduct(cross_v1_v2) / denominator;

			var intersectionPoint = p1 + t * v1;

			return intersectionPoint;
		}
	}
}
