namespace LNG
{
	// All types of properties
	public enum Types
	{
		NONE,
		STRING,
		FLOAT, FLOAT2, FLOAT3, FLOAT4,
		INT, INT2, INT3, INT4,
		BOOL, BOOL2, BOOL3, BOOL4
	}

	/* Base Class For properties. Holds Type, Name & Value*/
	public abstract class Property
	{

		public abstract new Types GetType();
		public abstract string GetName();
		public abstract void SetName(string name);
		public abstract string ValuesToString();

		//public static bool operator !=( Property p)
		//{
		//	return GetType() != p.GetType();
		//}

		//public static bool operator ==( Property p)
		//{
		//	return this->GetType() == p.GetType();
		//}

		public static Types StrToType(string type)
		{
			if (type == "String") { return Types.STRING; }
			else if (type == "Float") { return Types.FLOAT; }
			else if (type == "Float2") { return Types.FLOAT2; }
			else if (type == "Float3") { return Types.FLOAT3; }
			else if (type == "Float4") { return Types.FLOAT4; }
			else if (type == "Int") { return Types.INT; }
			else if (type == "Int2") { return Types.INT2; }
			else if (type == "Int3") { return Types.INT3; }
			else if (type == "Int4") { return Types.INT4; }
			else if (type == "Bool") { return Types.BOOL; }
			else if (type == "Bool2") { return Types.BOOL2; }
			else if (type == "Bool3") { return Types.BOOL3; }
			else if (type == "Bool4") { return Types.BOOL4; }
			else { return Types.NONE; }
		}
		public static Types ParentType(Types type)
		{
			switch (type)
			{
				case Types.STRING:
					return Types.STRING;
				case Types.FLOAT:
					return Types.FLOAT;
				case Types.FLOAT2:
					return Types.FLOAT;
				case Types.FLOAT3:
					return Types.FLOAT;
				case Types.FLOAT4:
					return Types.FLOAT;
				case Types.INT:
					return Types.INT;
				case Types.INT2:
					return Types.INT;
				case Types.INT3:
					return Types.INT;
				case Types.INT4:
					return Types.INT;
				case Types.BOOL:
					return Types.BOOL;
				case Types.BOOL2:
					return Types.BOOL;
				case Types.BOOL3:
					return Types.BOOL;
				case Types.BOOL4:
					return Types.BOOL;
				default:
					return Types.NONE;
			}
		}

		public static string TypeToStr(Types type)
		{
			switch (type)
			{
				case Types.STRING:
					return "String";
				case Types.FLOAT:
					return "Float";
				case Types.FLOAT2:
					return "Float2";
				case Types.FLOAT3:
					return "Float3";
				case Types.FLOAT4:
					return "Float4";
				case Types.INT:
					return "Int";
				case Types.INT2:
					return "Int2";
				case Types.INT3:
					return "Int3";
				case Types.INT4:
					return "Int4";
				case Types.BOOL:
					return "Bool";
				case Types.BOOL2:
					return "Bool2";
				case Types.BOOL3:
					return "Bool3";
				case Types.BOOL4:
					return "Bool4";
				default:
					return "NONE";
			}

		}

	};

	public class StringProperty : Property
	{
		public StringProperty() { Value = ""; }
		public StringProperty(string val) { Value = val; }
		public StringProperty(string name, string val) { Name = name; Value = val; }

		public void SetValue(string value) { Value = value; }

		public string GetValue() { return Value; }

		public override Types GetType()
		{
			return Types.STRING;
		}

		public override string GetName()
		{
			return Name;
		}

		public override void SetName(string name)
		{
			Name = name;
		}

		public override string ValuesToString()
		{
			return Value;
		}

		private string Name;
		private string Value;
	}

	public class FloatProperty : Property
	{
		public FloatProperty() { Value = 0.0f; }
		public FloatProperty(float val) { Value = val; }
		public FloatProperty(string name, float val) { Name = name; Value = val; }

		public void SetValue(float value) { Value = value; }

		public float GetValue() { return Value; }

		public override Types GetType()
		{
			return Types.FLOAT;
		}

		public override string GetName()
		{
			return Name;
		}

		public override void SetName(string name)
		{
			Name = name;
		}

		public override string ValuesToString()
		{
			return Value.ToString();
		}

		private string Name;
		private float Value;
	}

	public class Float2Property : Property
	{
		public Float2Property() { Value = new Float2(); }
		public Float2Property(Float2 val) { Value = val; }
		public Float2Property(string name, Float2 val) { Name = name; Value = val; }

		public void SetValue(Float2 value) { Value = value; }

		public Float2 GetValue() { return Value; }

		public override Types GetType()
		{
			return Types.FLOAT2;
		}

		public override string GetName()
		{
			return Name;
		}

		public override void SetName(string name)
		{
			Name = name;
		}

		public override string ValuesToString()
		{
			return Value.x.ToString() + ", " + Value.y.ToString();
		}

		private string Name;
		private Float2 Value;
	}

	public class Float3Property : Property
	{
		public Float3Property() { Value = new Float3(); }
		public Float3Property(Float3 val) { Value = val; }
		public Float3Property(string name, Float3 val) { Name = name; Value = val; }

		public void SetValue(Float3 value) { Value = value; }

		public Float3 GetValue() { return Value; }

		public override Types GetType()
		{
			return Types.FLOAT3;
		}

		public override string GetName()
		{
			return Name;
		}

		public override void SetName(string name)
		{
			Name = name;
		}

		public override string ValuesToString()
		{
			return Value.x.ToString() + ", " + Value.y.ToString() + ", " + Value.z.ToString();
		}

		private string Name;
		private Float3 Value;
	}

	public class Float4Property : Property
	{
		public Float4Property() { Value = new Float4(); }
		public Float4Property(Float4 val) { Value = val; }
		public Float4Property(string name, Float4 val) { Name = name; Value = val; }

		public void SetValue(Float4 value) { Value = value; }

		public Float4 GetValue() { return Value; }

		public override Types GetType()
		{
			return Types.FLOAT2;
		}

		public override string GetName()
		{
			return Name;
		}

		public override void SetName(string name)
		{
			Name = name;
		}

		public override string ValuesToString()
		{
			return Value.x.ToString() + ", " + Value.y.ToString() + ", " + Value.z.ToString() + ", " + Value.w.ToString();
		}

		private string Name;
		private Float4 Value;
	}

	public class IntProperty : Property
	{
		public IntProperty() { Value = 0; }
		public IntProperty(int val) { Value = val; }
		public IntProperty(string name, int val) { Name = name; Value = val; }

		public void SetValue(int value) { Value = value; }
		public int GetValue() { return Value; }

		public override Types GetType()
		{
			return Types.INT;
		}

		public override string GetName()
		{
			return Name;
		}

		public override void SetName(string name)
		{
			Name = name;
		}

		public override string ValuesToString()
		{
			return Value.ToString();
		}

		private string Name;
		private int Value;
	}

	public class Int2Property : Property
	{
		public Int2Property() { Value = new Int2(); }
		public Int2Property(Int2 val) { Value = val; }
		public Int2Property(string name, Int2 val) { Name = name; Value = val; }

		public void SetValue(Int2 value) { Value = value; }

		public Int2 GetValue() { return Value; }

		public override Types GetType()
		{
			return Types.INT2;
		}

		public override string GetName()
		{
			return Name;
		}

		public override void SetName(string name)
		{
			Name = name;
		}

		public override string ValuesToString()
		{
			return Value.x.ToString() + ", " + Value.y.ToString();
		}

		private string Name;
		private Int2 Value;
	}

	public class Int3Property : Property
	{
		public Int3Property() { Value = new Int3(); }
		public Int3Property(Int3 val) { Value = val; }
		public Int3Property(string name, Int3 val) { Name = name; Value = val; }

		public void SetValue(Int3 value) { Value = value; }

		public Int3 GetValue() { return Value; }

		public override Types GetType()
		{
			return Types.INT3;
		}

		public override string GetName()
		{
			return Name;
		}

		public override void SetName(string name)
		{
			Name = name;
		}

		public override string ValuesToString()
		{
			return Value.x.ToString() + ", " + Value.y.ToString() + ", " + Value.z.ToString();
		}

		private string Name;
		private Int3 Value;
	}

	public class Int4Property : Property
	{
		public Int4Property() { Value = new Int4(); }
		public Int4Property(Int4 val) { Value = val; }
		public Int4Property(string name, Int4 val) { Name = name; Value = val; }

		public void SetValue(Int4 value) { Value = value; }

		public Int4 GetValue() { return Value; }

		public override Types GetType()
		{
			return Types.INT4;
		}

		public override string GetName()
		{
			return Name;
		}

		public override void SetName(string name)
		{
			Name = name;
		}

		public override string ValuesToString()
		{
			return Value.x.ToString() + ", " + Value.y.ToString() + ", " + Value.z.ToString() + ", " + Value.w.ToString();
		}

		private string Name;
		private Int4 Value;
	}

	public class BoolProperty : Property
	{
		public BoolProperty() { Value = false; }
		public BoolProperty(bool val) { Value = val; }
		public BoolProperty(string name, bool val) { Name = name; Value = val; }

		public void SetValue(bool value) { Value = value; }
		public bool GetValue() { return Value; }

		public override Types GetType()
		{
			return Types.BOOL;
		}

		public override string GetName()
		{
			return Name;
		}

		public override void SetName(string name)
		{
			Name = name;
		}

		public override string ValuesToString()
		{
			return Value.ToString();
		}

		private string Name;
		private bool Value;
	}

	public class Bool2Property : Property
	{
		public Bool2Property() { Value = new Bool2(); }
		public Bool2Property(Bool2 val) { Value = val; }
		public Bool2Property(string name, Bool2 val) { Name = name; Value = val; }

		public void SetValue(Bool2 value) { Value = value; }

		public Bool2 GetValue() { return Value; }

		public override Types GetType()
		{
			return Types.BOOL2;
		}

		public override string GetName()
		{
			return Name;
		}

		public override void SetName(string name)
		{
			Name = name;
		}

		public override string ValuesToString()
		{
			return Value.x.ToString() + ", " + Value.y.ToString();
		}

		private string Name;
		private Bool2 Value;
	}

	public class Bool3Property : Property
	{
		public Bool3Property() { Value = new Bool3(); }
		public Bool3Property(Bool3 val) { Value = val; }
		public Bool3Property(string name, Bool3 val) { Name = name; Value = val; }

		public void SetValue(Bool3 value) { Value = value; }

		public Bool3 GetValue() { return Value; }

		public override Types GetType()
		{
			return Types.BOOL3;
		}

		public override string GetName()
		{
			return Name;
		}

		public override void SetName(string name)
		{
			Name = name;
		}

		public override string ValuesToString()
		{
			return Value.x.ToString() + ", " + Value.y.ToString() + ", " + Value.z.ToString();
		}

		private string Name;
		private Bool3 Value;
	}

	public class Bool4Property : Property
	{
		public Bool4Property() { Value = new Bool4(); }
		public Bool4Property(Bool4 val) { Value = val; }
		public Bool4Property(string name, Bool4 val) { Name = name; Value = val; }

		public void SetValue(Bool4 value) { Value = value; }

		public Bool4 GetValue() { return Value; }

		public override Types GetType()
		{
			return Types.BOOL4;
		}

		public override string GetName()
		{
			return Name;
		}

		public override void SetName(string name)
		{
			Name = name;
		}

		public override string ValuesToString()
		{
			return Value.x.ToString() + ", " + Value.y.ToString() + ", " + Value.z.ToString() + ", " + Value.w.ToString();
		}

		private string Name;
		private Bool4 Value;
	}
}