using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UOP
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
