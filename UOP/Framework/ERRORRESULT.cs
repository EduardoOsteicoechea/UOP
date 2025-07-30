using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOP
{
	internal class ERRORRESULT
	{
		public ERRORRESULT(WORKFLOW uop, string message) 
		{
			WRAPPER.ManagedCommand(() => {
				var td = new TaskDialog("Error");
				td.MainContent = message;
				td.MainIcon = TaskDialogIcon.TaskDialogIconError;
				uop.DocumentResults();
				td.Show();
			});
		}
	}
}
