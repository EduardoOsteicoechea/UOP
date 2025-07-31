using Autodesk.Revit.UI;
using UOP.Core;

namespace UOP.Revit.Common
{
	public class REVITSUCCESSRESULT
	{
		public REVITSUCCESSRESULT(WORKFLOW uop, string message) 
		{
			WRAPPER.ManagedCommand(() => 
			{
				var td = new TaskDialog("Success");
				td.MainContent = message;
				td.MainIcon = TaskDialogIcon.TaskDialogIconInformation;
				uop.DocumentResults();
				td.Show();
			});
		}
	}
}
