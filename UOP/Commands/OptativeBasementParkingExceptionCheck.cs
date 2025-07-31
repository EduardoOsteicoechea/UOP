using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Revit.Actions;
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

			var referencePlanesCollector = 
			uop.Run<FilteredElementCollector, CollectInstancesByTypeArguments>(
				Collect.InstancesByType<ReferencePlane>,
				new CollectInstancesByTypeArguments(document)
				,"CollectReferencePlanes"
			);

			var referencePlanes = 
			uop.Run<List<Autodesk.Revit.DB.Element>, CastCollectorItemsToListArguments>(
				Cast.CollectorItemsToList<Element>,
				new CastCollectorItemsToListArguments(referencePlanesCollector)
				,"CastElementsListFromCollector"
			);

			var nameParameter = 
			uop.Run<Autodesk.Revit.DB.Parameter, ParameterGetByRevitUINameAndCategoryArguments>(
				Parameter.GetByRevitUIName,
				new ParameterGetByRevitUINameAndCategoryArguments(referencePlanes, "Name", BuiltInCategory.OST_GenericModel)
				,"GetReferencePlanesNameParameter"
			);

			if (nameParameter == null && !uop.LastMethodFailed)
			{
				new ERRORRESULT(uop, "Error al obtener el parámetro 'Name' de los planos de Referencia");
				return;
			}

			var libReferencePlaneElement = 
			uop.Run<Autodesk.Revit.DB.Element, FilterElementsByParameterAndStringValueArguments>(
				Filter.FirstElementByParameterAndStringValue,
				new FilterElementsByParameterAndStringValueArguments(referencePlanes, nameParameter, "L.I.B")
				, "GetlibReferencePlaneElement"				
			);

			var ldp1ReferencePlaneElement = 
			uop.Run<Autodesk.Revit.DB.Element, FilterElementsByParameterAndStringValueArguments>(
				Filter.FirstElementByParameterAndStringValue,
				new FilterElementsByParameterAndStringValueArguments(referencePlanes, nameParameter, "L.D.P 1")
				, "Getldp1ReferencePlaneElement"				
			);

			var ldp2ReferencePlaneElement =
			uop.Run<Autodesk.Revit.DB.Element, FilterElementsByParameterAndStringValueArguments>(
				Filter.FirstElementByParameterAndStringValue,
				new FilterElementsByParameterAndStringValueArguments(referencePlanes, nameParameter, "L.D.P 2")
				, "Getldp2ReferencePlaneElement"				
			);

			var ldpfReferencePlaneElement = 
			uop.Run<Autodesk.Revit.DB.Element, FilterElementsByParameterAndStringValueArguments>(
				Filter.FirstElementByParameterAndStringValue,
				new FilterElementsByParameterAndStringValueArguments(referencePlanes, nameParameter, "L.D.P F")
				, "GetldpfReferencePlaneElement"				
			);

			var allLevelsCollector = 
			uop.Run<FilteredElementCollector, CollectInstancesByTypeArguments>(
				Collect.InstancesByType<Autodesk.Revit.DB.Level>,
				new CollectInstancesByTypeArguments(document),
				"CollectReferencePlanes"				
			);

			var levelElements = 
			uop.Run<List<Element>, CastCollectorItemsToListArguments>(
				Cast.CollectorItemsToList<Element>,
				new CastCollectorItemsToListArguments(allLevelsCollector),
				"CastElementListFromCollector"				
			);

			var levels = 
			uop.Run<List<Autodesk.Revit.DB.Level>, CastElementsToTypeArguments>(
				Cast.ElementsToType<Autodesk.Revit.DB.Level>,
				new CastElementsToTypeArguments(levelElements),
				"CastElementsToLevels"				
			);

			var levelsBellowElevationZero = 
			uop.Run<List<Autodesk.Revit.DB.Level>, FilterLevelsByElevationArguments>(
				Filter.LevelsByElevation,
				new FilterLevelsByElevationArguments(levels, 0.0, true),
				"FilterLevelsBelowElevationZero"
			);

			var revitModelHasBasements =
			uop.Run<bool, CollectionContainsItemsArguments<Autodesk.Revit.DB.Level>>(
				Collection.ContainsItems<Autodesk.Revit.DB.Level>,
				new CollectionContainsItemsArguments<Autodesk.Revit.DB.Level>(levelsBellowElevationZero),
				"ValidateIfModelHasBasements"
			);

			if (revitModelHasBasements == false && !uop.LastMethodFailed)
			{
				new SUCCESSRESULT(uop, "No hay subsuelos que validar.");
				uop.DocumentResults();
				return;
			}

			var allRoomsCollector = 
			uop.Run<Autodesk.Revit.DB.FilteredElementCollector, CollectInstancesByTypeArguments>(
				Collect.InstancesByType<Autodesk.Revit.DB.SpatialElement>,
				new CollectInstancesByTypeArguments(document),
				"CollectAllRooms"
			);

			var allRooms = 
			uop.Run<List<Autodesk.Revit.DB.Element>, CastCollectorItemsToListArguments>(
				Cast.CollectorItemsToList<Element>,
				new CastCollectorItemsToListArguments(allRoomsCollector),
				"CastRoomListFromCollector"
			);

			var modelHasRooms = 
			uop.Run<bool, CollectionContainsItemsArguments<Autodesk.Revit.DB.Element>>(
				Collection.ContainsItems<Autodesk.Revit.DB.Element>,
				new CollectionContainsItemsArguments<Autodesk.Revit.DB.Element>(allRooms),
				"ValidateIfModelHasRooms"				
			);

			if (modelHasRooms == false && !uop.LastMethodFailed)
			{
				new ERRORRESULT(uop, "El modelo no tiene instancias de 'Rooms'.");
				uop.DocumentResults();
				return;
			}

			var parkingIdentifyerParameter =
			uop.Run<Autodesk.Revit.DB.Parameter, ParameterGetByRevitUINameAndCategoryArguments>(
				Parameter.GetByRevitUINameAndCategory,
				new ParameterGetByRevitUINameAndCategoryArguments(allRooms, "MC_Tipo de Local/Área Descubierta", BuiltInCategory.OST_Rooms),
				"Get_MC_Tipo_de_Local-Area_Descubierta_Parameter" 				
			);

			if (parkingIdentifyerParameter == null && !uop.LastMethodFailed)
			{
				new ERRORRESULT(uop, "Error obteniendo el parámetro 'Get_MC_Tipo de Local/Área Descubierta_Parameter'.");
				uop.DocumentResults();
				return;
			}

			var carsParkingRooms = 
			uop.Run<List<Autodesk.Revit.DB.Element>, FilterElementsByParameterAndStringValueArguments>(
				Filter.ElementsByParameterAndStringValue,
				new FilterElementsByParameterAndStringValueArguments(allRooms, parkingIdentifyerParameter, "Estacionamiento Vehicular"),
				"FilterCarsParkingRooms"				
			);

			var byciclesParkingRooms = 
			uop.Run<List<Autodesk.Revit.DB.Element>, FilterElementsByParameterAndStringValueArguments>(
				Filter.ElementsByParameterAndStringValue,
				new FilterElementsByParameterAndStringValueArguments(allRooms, parkingIdentifyerParameter, "Estacionamiento de Bicicletas"),
				"FilterByciclesParkingRooms"				
			);

			var revitModelHasCarParkingRooms = 
			uop.Run<bool, CollectionContainsItemsArguments<Autodesk.Revit.DB.Element>>(
				Collection.ContainsItems<Autodesk.Revit.DB.Element>,
				new CollectionContainsItemsArguments<Autodesk.Revit.DB.Element>(carsParkingRooms),
				"ValidateIfModelHasCarParkingRooms"				
			);

			var revitModelHasBicyclesParkingRooms = 
			uop.Run<bool, CollectionContainsItemsArguments<Autodesk.Revit.DB.Element>>(
				Collection.ContainsItems<Autodesk.Revit.DB.Element>,
				new CollectionContainsItemsArguments<Autodesk.Revit.DB.Element>(byciclesParkingRooms),
				"ValidateIfModelHasBicyclesParkingRooms"				
			);

			if (
				!revitModelHasCarParkingRooms &&
				!revitModelHasBicyclesParkingRooms &&
				!uop.LastMethodFailed
			)
			{
				new SUCCESSRESULT(uop, "No hay instancias de 'Room' que validar");
				uop.DocumentResults();
				return;
			}

			var LIBvsLDP1IntersectionPoint = 
			uop.Run<Autodesk.Revit.DB.XYZ, IntersectGetXYZFromReferencePlanePairIntersectionArguments>(
				Intersect.GetXYZFromReferencePlanePairIntersection,
				new IntersectGetXYZFromReferencePlanePairIntersectionArguments((ReferencePlane)libReferencePlaneElement, (ReferencePlane)ldp1ReferencePlaneElement),
				"GetLFD-LDP1IntersectionPointByPlanePairIntersection"				
			);

			var LIBvsLDP2IntersectionPoint = 
			uop.Run<Autodesk.Revit.DB.XYZ, IntersectGetXYZFromReferencePlanePairIntersectionArguments>(
				Intersect.GetXYZFromReferencePlanePairIntersection,
				new IntersectGetXYZFromReferencePlanePairIntersectionArguments((ReferencePlane)libReferencePlaneElement, (ReferencePlane)ldp2ReferencePlaneElement),
				"GetLFD-LDP2IntersectionPointByPlanePairIntersection"				
			);

			var LDPFvsLDP1IntersectionPoint =
			uop.Run<Autodesk.Revit.DB.XYZ, IntersectGetXYZFromReferencePlanePairIntersectionArguments>(
				Intersect.GetXYZFromReferencePlanePairIntersection,
				new IntersectGetXYZFromReferencePlanePairIntersectionArguments((ReferencePlane)ldpfReferencePlaneElement, (ReferencePlane)ldp1ReferencePlaneElement),
				"GetLDPF-LDP1IntersectionPointByPlanePairIntersection"				
			);

			var LDPFvsLDP2IntersectionPoint = 
			uop.Run<Autodesk.Revit.DB.XYZ, IntersectGetXYZFromReferencePlanePairIntersectionArguments>(
				Intersect.GetXYZFromReferencePlanePairIntersection,
				new IntersectGetXYZFromReferencePlanePairIntersectionArguments((ReferencePlane)ldpfReferencePlaneElement, (ReferencePlane)ldp2ReferencePlaneElement),
				"GetLDPF-LDP2IntersectionPointByPlanePairIntersection"				
			);


			if (!uop.LastMethodFailed)
			{
				new SUCCESSRESULT(uop, "Flujo de trabajo completado.");
				uop.DocumentResults();
			}
		}
	}
}
