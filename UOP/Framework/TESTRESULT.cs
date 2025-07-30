using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOP
{
	public class TESTRESULT
	{
		public string MethodName { get; set; } = "";
		public string MethodDescriptiveName { get; set; } = "";
		public bool PassesTest { get; set; } = false;
		public string ResultObvervation { get; set; } = "No observations were provided";
		public dynamic ResultValue { get; set; } = default;
		public string ResultTypeName { get; set; } = default;
		public string MethodTime { get; set; } = "";
		public string MethodDeclaringTypeName { get; set; } = "";
		public string MethodNamespace { get; set; } = "";
		public dynamic MethodArguments { get; set; }
	}
}
