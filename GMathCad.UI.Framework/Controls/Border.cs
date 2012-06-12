using System;

namespace GMathCad.UI.Framework
{
	public class Border : Decorator
	{ 
		public Thickness Padding  { get; set; }
		public Color BorderColor { get; set; }
		
		public Border ()
		{
			BorderColor = new Color (0, 0, 0);
			Padding = new Thickness (5);
		}
		
		protected override void OnRender (Cairo.Context cr)
		{		
			if (Child == null) return;
			
			cr.NewSubPath();
			
			cr.Save ();
			
			cr.Color = new Cairo.Color (BorderColor.R, BorderColor.G, BorderColor.B);	
			cr.Rectangle (0, 0, Width, Height);			
			
			cr.Stroke ();
			
			cr.Restore ();
			
			cr.NewSubPath ();
			
			cr.Save ();
			
			Cairo.Matrix m = new Cairo.Matrix(1, 0, 0, 1, Padding.Left, Padding.Top);
			cr.Transform (m);			
			
			Child.Render (cr);
			
			cr.Restore ();
			
			cr.Stroke ();
		}
		
		protected override Size MeasureOverride (Size availableSize, Cairo.Context cr)
		{
			if (Child == null)
				return Size.Empty;
			
			Child.Measure (availableSize, cr);
			
			var width = Child.DesiredSize.Width + Padding.Left + Padding.Right;
			var height = Child.DesiredSize.Height + Padding.Top + Padding.Bottom;
			
			return new Size(width, height);
		}
		
		protected override void ArrangeOverride (Size finalSize)
		{
			if (Child == null)
				return;
				
			Child.Arrange (!Child.DesiredSize.IsEmpty ? Child.DesiredSize : finalSize);
		}
	}
}

