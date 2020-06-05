using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LNG
{
	public class Parser
	{
		// --------- PUBLIC MEMBERS ---------

		// For File
		public Parser(string filepath)
		{
			if (!File.Exists(filepath))
			{
				Console.Write("Filepath: (\"" + filepath + "\") is invalid\n");
				errors++;
				return;
			}

			string[] lines = File.ReadAllLines(filepath);
			FileBuffer = new List<string>(lines);
			FilePath = filepath;
		}

		// Without file
		public Parser(List<string> src)
		{
			FileBuffer = new List<string>(src);
		}

		public void Parse()
		{
			Lex();
			Console.Write("\nFinished Parsing. Errors: " + errors + ", Warnings: " + warnings + "\n");
		}

		// --------- PRIVATE MEMBERS ---------

		private void Lex()
		{
			string CurrentSection = "";

			for (int lineIndex = 0; lineIndex < FileBuffer.Count; lineIndex++)
			{
				string CurrentLine = FileBuffer[lineIndex];
				#region FindSection
				if (CurrentLine.Contains("@SECTION"))
				{
					// Check if section end is called
					if (CurrentLine.Contains("[END]"))
					{
						if (Assert(!(CurrentSection.Length <= 0), "Line: " + (lineIndex + 1) + ": No current section!", AssertType.Error)) { return; }
						CurrentSection = "";
						continue;
					}

					// New Section Started before Section end
					if (Assert(CurrentSection.Length <= 0, "Line " + (lineIndex + 1) + " : New section started before end of last section '" + CurrentSection + "'.Did you forgot to call @SECTION[END] ?\n", AssertType.Error)) { return; }

					// If a new section is started and there are no current bound sections
					int fbracket = CurrentLine.IndexOf('(');
					int sbracket = CurrentLine.IndexOf(')');

					if (Assert(fbracket != -1, "Line: " + (lineIndex + 1) + ": Expected '('", AssertType.Error)) { return; }
					if (Assert(sbracket != -1, "Line: " + (lineIndex + 1) + ": Expected ')'", AssertType.Error)) { return; }

					CurrentSection = CurrentLine.Substring(fbracket + 1, (sbracket - fbracket) - 1);

					continue;
				}
				#endregion

				// If section is set and current line is not empty
				else if (CurrentSection.Length > 0 && CurrentLine.Length > 0)
				{
					int fsbracket = CurrentLine.IndexOf("[");
					int ssbracket = CurrentLine.IndexOf("]", fsbracket + 1);
					int fbracket = CurrentLine.IndexOf("(", ssbracket + 1);
					int sbracket = CurrentLine.IndexOf(")", fbracket);
					int colon = CurrentLine.IndexOf(":", sbracket + 1);
					int fcbracket = CurrentLine.IndexOf("{", colon + 1);
					int scbracket = CurrentLine.IndexOf("}", fcbracket + 1);
					int fquote = CurrentLine.IndexOf("\"", fcbracket + 1);
					int squote = CurrentLine.IndexOf("\"", fquote + 1);

					if (Assert(fsbracket != -1, "Line: " + (lineIndex + 1) + ": Expected '['", AssertType.Error)) { return; }
					if (Assert(ssbracket != -1, "Line: " + (lineIndex + 1) + ": Expected ']'", AssertType.Error)) { return; }

					if (Assert(fbracket != -1, "Line: " + (lineIndex + 1) + ": Expected '('", AssertType.Error)) { return; }
					if (Assert(sbracket != -1, "Line: " + (lineIndex + 1) + ": Expected ')'", AssertType.Error)) { return; }

					if (Assert(fcbracket != -1, "Line: " + (lineIndex + 1) + ": Expected '{'", AssertType.Error)) { return; }
					if (Assert(scbracket != -1, "Line: " + (lineIndex + 1) + ": Expected '}'", AssertType.Error)) { return; }

					if (Assert(colon != -1, "Line: " + (lineIndex + 1) + ": Expected ':'", AssertType.Error)) { return; }

					string typeStr = CurrentLine.Substring(fsbracket + 1, (ssbracket - fsbracket) - 1);
					Types type = Property.StrToType(typeStr);

					if (type == Types.STRING)
					{
						if (Assert(fquote != -1, "Line: " + (lineIndex + 1) + ": Expected '\"'", AssertType.Error)) { return; }
						if (Assert(squote != -1, "Line: " + (lineIndex + 1) + ": Expected Closing '\"'", AssertType.Error)) { return; }
					}
					else
					{
						// If quotes are used but type != String
						if (fquote != -1 || squote != -1)
						{
							if (Assert(false, "Line: " + (lineIndex + 1) + ": Expected 'String', got " + Property.TypeToStr(type) + " instead", AssertType.Error)) { return; }
							errors++;
							return;
						}
					}

					string name = CurrentLine.Substring(fbracket + 1, (sbracket - fbracket) - 1);

					Property prop = null;

					// First we split the string using the delim ','
					List<string> Out;
					string Params;

					if (type == Types.STRING)
					{
						Params = CurrentLine.Substring(fquote + 1, (squote - fquote) - 1);
					}
					else
					{
						Params = CurrentLine.Substring(fcbracket + 1, (scbracket - fcbracket) - 1);
					}

					// Split string using , delim
					string[] split = Params.Split(',');
					Out = new List<string>(split);

					prop = SetPropertyValues(type, Out);
					// SetName is common in all properties.
					prop.SetName(name);

					foreach (var props in Properties)
					{
						if ((props.Key == CurrentSection) && (props.Value.GetName() == name)) {
							if (Assert(false, "Line: " + (lineIndex + 1) + " : Duplicate Key \"" + name + "\" Found in Section \"" + CurrentSection + "\"", AssertType.Warning)) { }
						}
					}

					Properties.Add(new KeyValuePair<string, Property>(CurrentSection, prop));
					// Sort
					Properties = Properties.OrderBy(kvp => kvp.Key).ToList();

					continue;
				}
			}
		}

		private enum AssertType
		{
			Error, Warning, Info
		}

		bool Assert(bool condition, string toThrow, AssertType type)
		{
			if (!condition)
			{
				Console.Write(type + ": " + toThrow + "\n");
				switch (type)
				{
					case AssertType.Error:
						errors++;
						break;
					case AssertType.Warning:
						warnings++;
						break;
					case AssertType.Info:
						break;
					default:
						break;
				}
				return true;
			}
			return false;
		}

		private Property SetPropertyValues(Types type, List<string> vals)
		{
			if (type == Types.STRING)
			{
				return new StringProperty(vals[0]);
			}

			if (Property.ParentType(type) == Types.FLOAT)
			{
				List<float> arr = new List<float>();

				foreach (var toConvert in vals)
				{
					arr.Add(float.Parse(toConvert));

				}

				switch (type)
				{
					case Types.FLOAT:
						return new FloatProperty(arr[0]);
					case Types.FLOAT2:
						return new Float2Property(new Float2(arr[0], arr[1]));
					case Types.FLOAT3:
						return new Float3Property(new Float3(arr[0], arr[1], arr[2]));
					case Types.FLOAT4:
						return new Float4Property(new Float4(arr[0], arr[1], arr[2], arr[3]));

					default:
						break;
				}
			}

			if (Property.ParentType(type) == Types.INT)
			{
				List<int> arr = new List<int>();

				foreach (var toConvert in vals)
				{
					arr.Add(int.Parse(toConvert));
				}

				switch (type)
				{
					case Types.INT:
						return new IntProperty(arr[0]);
					case Types.INT2:
						return new Int2Property(new Int2(arr[0], arr[1]));
					case Types.INT3:
						return new Int3Property(new Int3(arr[0], arr[1], arr[2]));
					case Types.INT4:
						return new Int4Property(new Int4(arr[0], arr[1], arr[2], arr[3]));

					default:
						break;
				}
			}

			if (Property.ParentType(type) == Types.BOOL)
			{
				List<bool> arr = new List<bool>();

				foreach (var toConvert in vals)
				{
					arr.Add(bool.Parse(toConvert));
				}

				switch (type)
				{
					case Types.BOOL:
						return new BoolProperty(arr[0]);
					case Types.BOOL2:
						return new Bool2Property(new Bool2(arr[0], arr[1]));
					case Types.BOOL3:
						return new Bool3Property(new Bool3(arr[0], arr[1], arr[2]));
					case Types.BOOL4:
						return new Bool4Property(new Bool4(arr[0], arr[1], arr[2], arr[3]));

					default:
						break;
				}
			}
			return null;
		}

		// Get all properties
		public List<KeyValuePair<string, Property>> GetProperties()
		{
			return Properties;
		}

		// Get a property
		public KeyValuePair<string, Property> GetProperty(string section, string key)
		{
			foreach (var property in Properties)
			{
				if (property.Key == section && property.Value.GetName() == key)
				{
					return property;
				}
			}
			return new KeyValuePair<string, Property>();
		}

		public void AddProperty(string section, Property prop)
		{
			foreach (var property in Properties)
			{
				if (property.Key == section)
				{
					if (property.Value.GetName() == prop.GetName())
					{
						if (Assert(property.Value.GetName() != prop.GetName(), "Warning: A Property named '" + prop.GetName() + "' already exists. Ignoring", AssertType.Warning)) { return; }
					}
				}
			}
			Properties.Add(new KeyValuePair<string, Property>(section, prop));

			// Sort
			Properties = Properties.OrderBy(kvp => kvp.Key).ToList();
		}

		public void Flush()
		{
			if (errors > 0) { Console.Write("Error: Could not write to file due to errors found while parsing.\n"); return; }

			// Sort
			Properties = Properties.OrderBy(kvp => kvp.Key).ToList();

			// Create Section
			List<string> file = new List<string>();
			string currentSection = "";

			for (int x = 0; x < Properties.Count; x++)
			{
				// Set the section
				if (currentSection.Length <= 0)
				{
					currentSection = Properties[x].Key;
					if (x == 0) { file.Add("@SECTION (" + currentSection + ")"); }
					else { file.Add("\n@SECTION (" + currentSection + ")"); }
				}
				else if (currentSection != Properties[x].Key)
				{
					// If a new section has been started, we end the section
					file.Add("@SECTION [END]");
					currentSection = Properties[x].Key;
					// Then we create the new section
					file.Add("\n@SECTION (" + currentSection + ")");
				}

				string TypePart = "[" + Property.TypeToStr(Properties[x].Value.GetType()) + "]";
				string NamePart = "(" + Properties[x].Value.GetName() + ")";

				string ValuesPart;

				if (Property.ParentType(Properties[x].Value.GetType()) == Types.STRING)
				{
					ValuesPart = "{\"" + Properties[x].Value.ValuesToString() + "\"}";
				}
				else
				{
					ValuesPart = "{" + Properties[x].Value.ValuesToString() + "}";
				}

				string FormattedLine = TypePart + NamePart + ": " + ValuesPart;
				file.Add(FormattedLine);

				if ((x + 1) == Properties.Count)
				{
					if (!(currentSection.Length <= 0))
					{
						file.Add("@SECTION [END]");
					}
				}

			}

			if (!File.Exists(FilePath)) {Console.Write( "Error: Failed to open file\n"); errors++; }
			else
			{
				File.WriteAllLines(FilePath, file);
			}
			Console.Write("\nFlush Finished. Errors: " + errors + ", Warnings: " + warnings + "\n");
		}

		// Variables
		private List<string> FileBuffer;
		private string FilePath;
		private List<KeyValuePair<string, Property>> Properties = new List<KeyValuePair<string, Property>>();
		int errors, warnings = 0;
	}
}
