using System;

namespace GMathCad.UI.Framework
{
	public struct Size
	{
		private double height;		
		private double width; 
		private bool isEmpty;
					
		public Size (double width, double height) : this (width, height, false)
		{			
		}
		
		private Size (double width, double height, bool isEmpty)
		{
			this.width = width;
			this.height = height;
			this.isEmpty = isEmpty;
		}
		
		public double Height {
			get {
				return this.height;
			}
		}

		public double Width {
			get {
				return this.width;
			}
		}	
		
		public bool IsEmpty {
			get {
				return isEmpty;
			}
		}
		
		public static Size Empty = new Size (double.NegativeInfinity, double.NegativeInfinity, true);
	}
}

