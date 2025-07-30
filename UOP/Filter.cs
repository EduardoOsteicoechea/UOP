using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Revit.Actions
{
	public static class Filter
	{
		public static List<Autodesk.Revit.DB.Level> LevelsByElevation
		(
			FilterLevelsByElevationArguments arguments
		)
		{
			var filteredLevels = new List<Autodesk.Revit.DB.Level>();

			foreach (var item in arguments.Items)
			{
				if (arguments.SearchBelowOfElevation)
				{
					if (item.Elevation < arguments.FilteringElevation)
					{
						filteredLevels.Add(item);
					}
				}
				else
				{
					if (item.Elevation > arguments.FilteringElevation)
					{
						filteredLevels.Add(item);
					}
				}
			}

			return filteredLevels;
		}

		public static List<Autodesk.Revit.DB.Element> ByBuiltInCategory
		(
			FilterByBuiltInCategoryArguments arguments
		)
		{
			var result = new List<Autodesk.Revit.DB.Element>();

			if (arguments.Category.HasValue)
			{
				foreach (var item in arguments.Items)
				{
					if (item.Category != null && item.Category.Id.IntegerValue == (int)arguments.Category.Value)
					{
						result.Add(item);
					}
				}
			}

			return result;
		}

		public static double? MaximumOrMinimumByPropertyName<T>
		(
			FilterMaximumOrMinimumByPropertyNameArguments<T> arguments
		)
		{
			var propertyInfo = typeof(T).GetProperty(arguments.PropertyName);

			if (propertyInfo == null)
			{
				throw new Exception("The Property is null");
			}

			Type actualPropertyType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

			if
			(
				 actualPropertyType != typeof(double) &&
				 actualPropertyType != typeof(int) &&
				 actualPropertyType != typeof(float) &&
				 actualPropertyType != typeof(decimal) &&
				 actualPropertyType != typeof(byte) &&
				 actualPropertyType != typeof(sbyte) &&
				 actualPropertyType != typeof(short) &&
				 actualPropertyType != typeof(ushort) &&
				 actualPropertyType != typeof(uint) &&
				 actualPropertyType != typeof(long) &&
				 actualPropertyType != typeof(ulong)
			)
			{
				throw new InvalidOperationException($"Property '{arguments.PropertyName}' on type '{typeof(T).Name}' is not a numeric type.");
			}

			IEnumerable<double?> numericValues = arguments.Items.Select(item =>
			{
				object value = propertyInfo.GetValue(item);

				if (value == null)
				{
					return (double?)null;
				}

				return Math.Round(Convert.ToDouble(value), 2);
			});

			if (arguments.GetMaximum ?? true)
			{
				return numericValues.Max();
			}
			else
			{
				return numericValues.Min();
			}
		}

		public static double? MaximumOrMinimumByPropertyInfo<T>
		(
			FilterMaximumOrMinimumByPropertyInfoArguments<T> arguments
		)
		{
			Type actualPropertyType = Nullable.GetUnderlyingType(arguments.PropertyInfo.PropertyType) ?? arguments.PropertyInfo.PropertyType;

			if
			(
				 actualPropertyType != typeof(double) &&
				 actualPropertyType != typeof(int) &&
				 actualPropertyType != typeof(float) &&
				 actualPropertyType != typeof(decimal) &&
				 actualPropertyType != typeof(byte) &&
				 actualPropertyType != typeof(sbyte) &&
				 actualPropertyType != typeof(short) &&
				 actualPropertyType != typeof(ushort) &&
				 actualPropertyType != typeof(uint) &&
				 actualPropertyType != typeof(long) &&
				 actualPropertyType != typeof(ulong)
			)
			{
				throw new InvalidOperationException($"Property '{arguments.PropertyInfo.Name}' on type '{typeof(T).Name}' is not a numeric type.");
			}

			IEnumerable<double?> numericValues = arguments.Items.Select(item =>
			{
				object value = arguments.PropertyInfo.GetValue(item);

				if (value == null)
				{
					return (double?)null;
				}

				return Math.Round(Convert.ToDouble(value), 2);
			});

			if (arguments.GetMaximum ?? true)
			{
				return numericValues.Max();
			}
			else
			{
				return numericValues.Min();
			}
		}


		public static List<Autodesk.Revit.DB.Element> ElementsByParameterAndStringValue
		(
			FilterElementsByParameterAndStringValueArguments arguments
		)
		{
			string parameterName = arguments.SourceParameter.Definition.Name;

			var result = new List<Autodesk.Revit.DB.Element>();

			foreach (var item in arguments.Items)
			{
				if (item == null) continue;

				try
				{
					var castedItem = item as Autodesk.Revit.DB.Element;

					Autodesk.Revit.DB.Parameter elementParameter = item.LookupParameter(parameterName);

					if (elementParameter != null)
					{
						if (elementParameter.AsValueString() == arguments.Value)
						{
							result.Add(castedItem);
						}
					}
				}
				catch { }
			}

			return result;
		}

		public static Autodesk.Revit.DB.Element FirstElementByParameterAndStringValue
		(
			FilterElementsByParameterAndStringValueArguments arguments
		)
		{
			string parameterName = arguments.SourceParameter.Definition.Name;

			var result = new List<Autodesk.Revit.DB.Element>();

			foreach (var item in arguments.Items)
			{
				if (item == null) continue;

				try
				{
					var castedItem = item as Autodesk.Revit.DB.Element;

					Autodesk.Revit.DB.Parameter elementParameter = item.LookupParameter(parameterName);

					if (elementParameter != null)
					{
						if (elementParameter.AsValueString() == arguments.Value)
						{
							result.Add(castedItem);
						}
					}
				}
				catch { }
			}

			return result.FirstOrDefault();
		}

		public static List<Autodesk.Revit.DB.Element> ElementsByParameterStringValue<T>
		(
			FilterElementsByParameterStringValueArguments arguments
		)
		{
			var result = new List<Autodesk.Revit.DB.Element>();

			foreach (var item in arguments.Items)
			{
				try
				{
					var castedItem = item as Autodesk.Revit.DB.Element;

					Autodesk.Revit.DB.Parameter parameter = castedItem.LookupParameter(arguments.ParameterName);

					if (parameter != null)
					{
						if (parameter.AsValueString() == arguments.Value)
						{
							result.Add(castedItem);
						}
					}
				}
				catch { }
			}

			return result;
		}

		public static List<Autodesk.Revit.DB.Element> ElementsByParameterStringValueSubstring<T>
		(
			FilterElementsByParameterStringValueSubstringArguments arguments
		)
		{
			var result = new List<Autodesk.Revit.DB.Element>();

			foreach (var item in arguments.Items)
			{
				try
				{
					var castedItem = item as Autodesk.Revit.DB.Element;

					Autodesk.Revit.DB.Parameter parameter = castedItem.LookupParameter(arguments.ParameterName);

					if (parameter != null)
					{
						if (parameter.AsValueString().Contains(arguments.Value))
						{
							result.Add(castedItem);
						}
					}
				}
				catch { }
			}

			return result;
		}

		public static List<Autodesk.Revit.DB.Element> ByNameSubstring
		(
			FilterByNameSubstringArguments arguments
		)
		{
			var result = new List<Autodesk.Revit.DB.Element>();

			foreach (var item in arguments.Items)
			{
				if (item.Name.Contains(arguments.Substring))
				{
					result.Add(item);
				}
			}

			return result;
		}

		public static FilteredElementCollector ByFilter
		(
			FilterByFilterArguments arguments
		)
		{
			return new FilteredElementCollector(arguments.Document)
			.WhereElementIsNotElementType()
			.WherePasses(arguments.Filter);
		}


		public static List<T> ByType<T>
		(
			dynamic[] arguments
		)
		{
			var items = arguments[0] as List<Autodesk.Revit.DB.Element>;

			return items.OfType<T>().ToList();
		}

		public static List<T> ByLevel<T>
		(
			dynamic[] arguments
		)
		{
			var levels = arguments[0] as List<Autodesk.Revit.DB.Level>;
			var items = arguments[1] as List<T>;

			var result = new List<T>();

			foreach (var level in levels)
			{
				foreach (var item in items)
				{
					if (item != null)
					{
						var castedElement = item as Autodesk.Revit.DB.Element;

						if (castedElement.LevelId == level.Id)
						{
							result.Add(item);
						}
					}
				}
			}

			return result;
		}
	}
}
