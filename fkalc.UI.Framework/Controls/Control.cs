using System;
namespace fkalc.UI.Framework
{
	public class Control : FrameworkElement
	{
		public Brush Background { get; set; }
		public Color BorderColor { get; set; }
		public double BorderThickness { get; set; }
		
		public Control ()
		{
			Background = Brushes.Transparent;
			BorderThickness = 1;
		}
		
		protected override void OnRender (DrawingContext dc)
		{
			base.OnRender (dc);
			
			if (!IsVisible)
				return;
		
			dc.DrawRectangle (Background, new Pen (BorderColor, BorderThickness), new Rect (0, 0, Width, Height));
		}
	}
}

