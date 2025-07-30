using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using UOP;

namespace Revit.Actions
{
	[Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
	public class OptativeBasementParkingExceptionCheck : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			RunWorkflow(commandData.Application.ActiveUIDocument.Document);

			return Result.Succeeded;
		}

		public void RunWorkflow
		(
			Autodesk.Revit.DB.Document document
		)
		{
			var uop = new WORKFLOW();

			var referencePlanesCollector = uop.Run<FilteredElementCollector, CollectInstancesByTypeArguments>(
				Collect.InstancesByType<ReferencePlane>,
				new CollectInstancesByTypeArguments(document),
				"CollectReferencePlanes"
			);

			var referencePlanes = uop.Run<List<Element>, CastCollectorItemsToListArguments>(
				Cast.CollectorItemsToList<Element>,
				new CastCollectorItemsToListArguments(referencePlanesCollector),
				"CastCollectorItemsToReferencePlaneElementList"
			);

			uop.DocumentResults();
		}
	}
}
