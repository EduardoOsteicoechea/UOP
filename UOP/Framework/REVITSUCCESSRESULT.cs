using Autodesk.Revit.UI;

namespace UOP
{
	internal class REVITSUCCESSRESULT
	{
		public REVITSUCCESSRESULT(WORKFLOW uop, string message) 
		{
			WRAPPER.ManagedCommand(() => {
				var td = new TaskDialog("Success");
				td.MainContent = message;
				td.MainIcon = TaskDialogIcon.TaskDialogIconInformation;
				uop.DocumentResults();
				td.Show();
			});
		}
	}
}
