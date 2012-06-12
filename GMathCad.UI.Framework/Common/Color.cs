using System;
namespace GMathCad.UI.Framework
{
	public struct Color
	{
		public int R { get; set; }
		public int G { get; set; }
		public int B { get; set; }
		
		public Color (int r, int g, int b) : this()
		{
			R = r;			
			G = g;
			B = b;
		}		
		
	}
}

