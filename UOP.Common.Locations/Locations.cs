

using System;

namespace UOP.Common.Locations
{
	public static class Locations
	{
		public static string FrameworkErrorsDirectoryPath { get; } = System.IO.Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
			"UOP",
			"FrameworkErrors"
		);
		public static string ChatHistoryDirectoryPath { get; } = System.IO.Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
			"UOP",
			"ChatHistory"
		);
		public static string DocumentationDirectoryPath { get; } = System.IO.Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
			"UOP",
			"Executions"
		);
	}
}
