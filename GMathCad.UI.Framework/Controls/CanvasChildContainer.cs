using System;

namespace GMathCad.UI.Framework
{
	public class CanvasChildContainer : ContentControl
	{
		public double X { get; set; }

		public double Y { get; set; }

		public CanvasChildContainer (UIElement child)
		{
			Content = child;
		}
			
		public override Visual HitTest (double x, double y)
		{
			return Content.HitTest (x - X, y - Y);
		}
	}
}

