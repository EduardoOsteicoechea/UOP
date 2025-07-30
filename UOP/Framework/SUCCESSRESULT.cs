using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOP
{
	internal class SUCCESSRESULT
	{
		public SUCCESSRESULT(WORKFLOW uop, string message) 
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
