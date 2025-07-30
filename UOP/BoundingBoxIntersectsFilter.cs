using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UOP
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
