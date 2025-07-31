using System;

namespace UOP.Revit.Units.Revit2024
{
	public static class Parameter
	{
		public static Autodesk.Revit.DB.Parameter GetByRevitUINameAndCategory
		(
			ParameterGetByRevitUINameAndCategoryArguments arguments
		)
		{
			if (string.IsNullOrEmpty(arguments.ParameterName))
			{
				throw new ArgumentException("The parameter name can't be an empty string", nameof(arguments.ParameterName));
			}

			Autodesk.Revit.DB.Parameter result = null;

			foreach (var element in arguments.Elements)
			{
				if (element == null)
				{
					continue;
				}

				if (element?.Category == null)
				{
					continue;
				}

				if (element.Category.Id.IntegerValue == (int)arguments.Category)
				{
					Autodesk.Revit.DB.Parameter parameter = element.LookupParameter(arguments.ParameterName);

					if (parameter != null)
					{
						return parameter;
					}
				}
			}

			return result;
		}

		public static Autodesk.Revit.DB.Parameter GetByRevitUIName
		(
			ParameterGetByRevitUINameAndCategoryArguments arguments
		)
		{
			if (string.IsNullOrEmpty(arguments.ParameterName))
			{
				throw new ArgumentException("The parameter name can't be an empty string", nameof(arguments.ParameterName));
			}

			Autodesk.Revit.DB.Parameter result = null;

			foreach (var element in arguments.Elements)
			{
				if (element == null)
				{
					continue;
				}
				Autodesk.Revit.DB.Parameter parameter = element.LookupParameter(arguments.ParameterName);

				if (parameter != null)
				{
					return parameter;
				}
			}

			return result;
		}
	}
}
