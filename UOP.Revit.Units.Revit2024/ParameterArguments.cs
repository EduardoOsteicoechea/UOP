using System.Collections.Generic;

namespace UOP.Revit.Units.Revit2024
{

	public class ParameterGetByRevitUINameAndCategoryArguments
	{
		public List<Autodesk.Revit.DB.Element> Elements { get; set; }
		public string ParameterName { get; set; }
		public Autodesk.Revit.DB.BuiltInCategory? Category { get; set; }
		public ParameterGetByRevitUINameAndCategoryArguments
		(
			List<Autodesk.Revit.DB.Element> elements,
			string parameterName,
			Autodesk.Revit.DB.BuiltInCategory? category = null
		)
		{
			Elements = elements;
			ParameterName = parameterName;
			Category = category;
		}
	}
}
