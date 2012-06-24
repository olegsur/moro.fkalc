using System;
namespace GMathCad.UI.Framework
{
	public class Control : FrameworkElement
	{
		public Brush Background { get; set; }
		public Color BorderColor { get; set; }
		
		public Control ()
		{
			Background = Brushes.Transparent;
		}
		
		protected override void OnRender (DrawingContext dc)
		{
			base.OnRender (dc);
			
			if (!IsVisible)
				return;
		
			dc.DrawRectangle (Background, new Pen (BorderColor, 1), new Rect (0, 0, Width, Height));
		}
	}
}

