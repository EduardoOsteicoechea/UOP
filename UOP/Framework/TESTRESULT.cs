using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UOP
{
	public class TESTRESULT<MethodReturnType, ArgumentsObject>
	{
		public string MethodName { get; set; } = "";
		public bool PassesTest { get; set; } = false;
		public string ResultObvervation { get; set; } = "No observations were provided";
		public MethodReturnType ResultValue { get; set; } = default;
		public string ResultTypeName { get; set; } = default;
		public string MethodTime { get; set; } = "";
		public string MethodDeclaringTypeName { get; set; } = "";
		public string MethodNamespace { get; set; } = "";
		public ArgumentsObject MethodArguments { get; set; }
		public Func<ArgumentsObject, MethodReturnType> MethodAction { get; set; }
	}
}
