using System.Collections;

namespace LNG
{
	public struct Float2
	{
		public Float2(float ParamX, float ParamY) { x = ParamX; y = ParamY; }
		public float x, y;
	};

	public struct Float3
	{
		public Float3(float ParamX, float ParamY, float ParamZ) { x = ParamX; y = ParamY; z = ParamZ; }
		public float x, y, z;
	};

	public struct Float4
	{
		public Float4(float ParamX, float ParamY, float ParamZ, float ParamW) { x = ParamX; y = ParamY; z = ParamZ; w = ParamW; }
		public float x, y, z, w;
	};

	public struct Int2
	{
		public Int2(int ParamX, int ParamY) { x = ParamX; y = ParamY; }
		public int x, y;
	};

	public struct Int3
	{
		public Int3(int ParamX, int ParamY, int ParamZ) { x = ParamX; y = ParamY; z = ParamZ; }
		public int x, y, z;
	};

	public struct Int4
	{
		public Int4(int ParamX, int ParamY, int ParamZ, int ParamW) { x = ParamX; y = ParamY; z = ParamZ; w = ParamW; }
		public int x, y, z, w;
	};

	public struct Bool2
	{
		public Bool2(bool ParamX, bool ParamY) { x = ParamX; y = ParamY; }
		public bool x, y;
	};

	public struct Bool3
	{
		public Bool3(bool ParamX, bool ParamY, bool ParamZ) { x = ParamX; y = ParamY; z = ParamZ; }
		public bool x, y, z;
	};

	public struct Bool4
	{
		public Bool4(bool ParamX, bool ParamY, bool ParamZ, bool ParamW) { x = ParamX; y = ParamY; z = ParamZ; w = ParamW; }
		public bool x, y, z, w;
	};
}
