using System;

namespace GMathCad.UI.Framework
{
	public struct Thickness
	{		
		public double bottom;
		public double left;
		public double right;
		public double top;			
		
		public Thickness (double uniformLength)
		{
			bottom = left = right = top = uniformLength;
		}
		
		public double Bottom { get { return bottom;	} }
		public double Left { get { return left;	} }
		public double Right { get {	return right; } }
		public double Top { get { return top; } }
			
	}
}

