using System;
using System.Threading.Tasks;

namespace UOP
{
    public static class WRAPPER
    {
		public static void ManagedCommand(Action action)
		{
			try
			{
				action.Invoke();
			}
			catch (System.Exception e)
			{
				HandleError(e);
			}
		}

		public async static void ManagedCommandAsync(Func<Task> action)
		{
			try
			{
				await action.Invoke();
			}
			catch (System.Exception e)
			{
				HandleError(e);
			}
		}

		public static T ManagedCommand<T>(Func<T> action)
		{
			try
			{
				return action.Invoke();
			}
			catch (System.Exception e)
			{
				HandleError(e);
				throw;
			}
		}

		public static void HandleError(System.Exception e)
		{
			var filePath = System.IO.Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
				"UOP",
				"FrameworkErrors",
				$"{DateTime.Now.ToString("yyyyMMdd_HHmm")}.txt"
			);

			System.IO.File.WriteAllText(
				filePath,
				$"{e.Message}\n\n{e.StackTrace}"
			);
		}
	}
}
