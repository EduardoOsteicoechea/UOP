
using System.Linq;

namespace UOP.Revit.Units.Revit2024
{
	public class Collection
	{
		public static bool? AreAllValuesNotNull<T>
		(
			CollectionAreAllValuesNotNullArguments<T> arguments
		)
		{
			return arguments.Items.All(a => a != null);
		}

		public static bool? ContainsNullValues<T>
		(
			CollectionContainsNullValuesArguments<T> arguments
		)
		{
			return arguments.Items.Any(a => a == null);
		}

		public static bool ContainsItems<T>
		(
			CollectionContainsItemsArguments<T> arguments
		)
		{
			return arguments.Items.Count() > 0;
		}


		public static bool AreAllElementsOnList
		(
			CollectionAreAllElementsOnListArguments arguments
		)
		{
			foreach (var item in arguments.ElementsToCheck)
			{
				bool foundInList = false;

				foreach (var listItem in arguments.List)
				{
					if (item.Id == listItem.Id)
					{
						foundInList = true;
						break;
					}
				}

				if (!foundInList)
				{
					return false;
				}
			}

			return true;
		}
	}
}
