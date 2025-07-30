using System.Collections.Generic;
using System.Linq;
using UOP;

namespace Revit.Actions
{
	public class CollectionAreAllValuesNotNullArguments<T>
	{
		public System.Collections.Generic.ICollection<T> Items { get; set; }
		public CollectionAreAllValuesNotNullArguments
		(
			List<T> items
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				Items = items;
			});
		}
	}

	public class CollectionContainsItemsArguments<T>
	{
		public System.Collections.Generic.ICollection<T> Items { get; set; }
		public CollectionContainsItemsArguments
		(
			List<T> items
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				Items = items;
			});
		}
	}

	public class CollectionContainsNullValuesArguments<T>
	{
		public System.Collections.Generic.ICollection<T> Items { get; set; }
		public CollectionContainsNullValuesArguments
		(
			List<T> items
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				Items = items;
			});
		}
	}

	public class CollectionAreAllElementsOnListArguments
	{
		public List<Autodesk.Revit.DB.Element> ElementsToCheck { get; set; }
		public List<Autodesk.Revit.DB.Element> List { get; set; }
		public CollectionAreAllElementsOnListArguments
		(
			List<Autodesk.Revit.DB.Element> elementsToCheck,
			List<Autodesk.Revit.DB.Element> list
		)
		{
			WRAPPER.ManagedCommand(() =>
			{
				ElementsToCheck = elementsToCheck;
				List = list;
			});
		}
	}





}
