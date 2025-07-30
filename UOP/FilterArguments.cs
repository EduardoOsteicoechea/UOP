using System.Collections.Generic;
using UOP;

namespace Revit.Actions
{

	public class FilterLevelsByElevationArguments
	{
		public List<Autodesk.Revit.DB.Level> Items { get; set; }
		public double FilteringElevation { get; set; }
		public bool SearchBelowOfElevation { get; set; }
		public FilterLevelsByElevationArguments
		(
			List<Autodesk.Revit.DB.Level> items,
			double filteringElevation,
			bool searchBelowOfElevation
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				Items = items;
				FilteringElevation = filteringElevation;
				SearchBelowOfElevation = searchBelowOfElevation;
			});
		}
	}

	public class FilterByBuiltInCategoryArguments
	{
		public List<Autodesk.Revit.DB.Element> Items { get; set; }
		public Autodesk.Revit.DB.BuiltInCategory? Category { get; set; }
		public FilterByBuiltInCategoryArguments
		(
			List<Autodesk.Revit.DB.Element> items,
			Autodesk.Revit.DB.BuiltInCategory category
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				Items = items;
				Category = category;
			});
		}
	}

	public class FilterMaximumOrMinimumByPropertyNameArguments<T>
	{
		public List<T> Items { get; set; }
		public string PropertyName { get; set; }
		public bool? GetMaximum { get; set; }
		public FilterMaximumOrMinimumByPropertyNameArguments
		(
		List<T> items,
		string propertyName,
		bool? getMaximum
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				Items = items;
				PropertyName = propertyName;
				GetMaximum = getMaximum;
			});
		}
	}

	public class FilterMaximumOrMinimumByPropertyInfoArguments<T>
	{
		public List<T> Items { get; set; }
		public System.Reflection.PropertyInfo PropertyInfo { get; set; }
		public bool? GetMaximum { get; set; }
		public FilterMaximumOrMinimumByPropertyInfoArguments
		(
			List<T> items,
			System.Reflection.PropertyInfo propertyInfo,
			bool? getMaximum
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				Items = items;
				PropertyInfo = propertyInfo;
				GetMaximum = getMaximum;
			});
		}
	}

	public class FilterElementsByParameterAndStringValueArguments
	{
		public List<Autodesk.Revit.DB.Element> Items { get; set; }
		public Autodesk.Revit.DB.Parameter SourceParameter { get; set; }
		public string Value { get; set; }
		public FilterElementsByParameterAndStringValueArguments
		(
			List<Autodesk.Revit.DB.Element> items,
			Autodesk.Revit.DB.Parameter sourceParameter,
			string value
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				Items = items;
				SourceParameter = sourceParameter;
				Value = value;
			});
		}
	}

	public class FilterElementsByParameterStringValueArguments
	{
		public List<Autodesk.Revit.DB.Element> Items { get; set; }
		public string ParameterName { get; set; }
		public string Value { get; set; }
		public FilterElementsByParameterStringValueArguments
		(
			List<Autodesk.Revit.DB.Element> items,
			string parameterName,
			string value
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				Items = items;
				ParameterName = parameterName;
				Value = value;
			});
		}
	}

	public class FilterElementsByParameterStringValueSubstringArguments
	{
		public List<Autodesk.Revit.DB.Element> Items { get; set; }
		public string ParameterName { get; set; }
		public string Value { get; set; }
		public FilterElementsByParameterStringValueSubstringArguments
		(
			List<Autodesk.Revit.DB.Element> items,
			string parameterName,
			string value
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				Items = items;
				ParameterName = parameterName;
				Value = value;
			});
		}
	}

	public class FilterByNameSubstringArguments
	{
		public List<Autodesk.Revit.DB.Element> Items { get; set; }
		public string Substring { get; set; }
		public FilterByNameSubstringArguments
		(
			List<Autodesk.Revit.DB.Element> items,
			string substring
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				Items = items;
				Substring = substring;
			});
		}
	}
	
	
	
	public class FilterByFilterArguments
	{
		public Autodesk.Revit.DB.Document Document { get; set; }
		public Autodesk.Revit.DB.ElementFilter Filter { get; set; }
		public FilterByFilterArguments
		(
			Autodesk.Revit.DB.Document document,
			Autodesk.Revit.DB.ElementFilter filter
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				this.Document = document;
				this.Filter = filter;
			});
		}
	}






}
