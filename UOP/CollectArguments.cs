using Autodesk.Revit.DB;
using System.Collections.Generic;
using UOP;

namespace Revit.Actions
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
