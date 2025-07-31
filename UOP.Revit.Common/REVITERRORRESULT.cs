using Autodesk.Revit.UI;
using UOP.Core;

namespace UOP.Revit.Common
{
	public class REVITERRORRESULT
	{
		public REVITERRORRESULT(WORKFLOW uop, string message) 
		{
			WRAPPER.ManagedCommand(() => 
			{
				var td = new TaskDialog("Error");
				td.MainContent = message;
				td.MainIcon = TaskDialogIcon.TaskDialogIconError;
				uop.DocumentResults();
				td.Show();
			});
		}
	}
}
