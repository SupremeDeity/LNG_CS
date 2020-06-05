using System;
using LNG;

namespace Example
{
	class Program
	{
		static void Main(string[] args)
		{
			string file = "";
			for (int i = 0; i < args.Length; i++)
			{
				if(args[i] == "-f")
				{
					if(!((i+1) >= args.Length)) {
						file = args[i + 1];
					}
					else
					{
						Console.Write("File path not provided after arg -f.\n");
						Console.Read();
						return;
					}
				}
			}

			if (file.Length > 0)
			{
				Parser parser = new Parser(file);
				parser.Parse();

				string section = "";

				foreach (var prop in parser.GetProperties()) {
					if (section != prop.Key)
					{
						section = prop.Key;
						Console.Write("Section: " + prop.Key + "\n");
					}
					Console.Write("\tKey: " + prop.Value.GetName() + "\tValue: " + prop.Value.ValuesToString() + "\n");
				}
			}

			else
			{
				Console.Write("No file provided to be parsed. Use -f arg together with path to .lng file.\n");
			}

			Console.Read();
		}
	}
}
