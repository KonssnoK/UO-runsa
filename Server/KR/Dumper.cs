using System.IO;
using System;
namespace Server
{
	public static class Dumper
	{
		public static void InternalDump(string Message, Exception Location)
		{
			Dump("InternalCrash.txt", Message, Location);
		}

		public static void MessageDump(string FileName,string Message)
		{
			Dump(FileName, Message, null);
		}
		public static void Dump(string FileName, string Message , Exception Location)
		{
			Console.WriteLine(string.Format("{0} Internal Dump Created.",Message));
			//WriteItDown
			using (StreamWriter op = new StreamWriter(FileName, true))
			{
				if (Location != null)
				{
					op.WriteLine("Server Handled Crash Report");
				}
				op.WriteLine(Message);
				if (Location != null)
				{
					op.WriteLine("===================");
					op.WriteLine(Location);
					op.WriteLine();
					op.WriteLine();
				}
			}
		}
	}
}