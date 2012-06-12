using System;
namespace GMathCad.UI.Framework
{
	public class Line : Shape
	{
		public Line ()
		{
		}
		
		protected override Size MeasureOverride (Size availableSize, Cairo.Context cr)
		{
			return new Size (Width, Height);
		}
		
		protected override void ArrangeCore (Size finalSize)
		{			
			Height = finalSize.Height;
			Width = finalSize.Width;
			
			DesiredSize = new Size (Width, Height);
		}		
		
		protected override void OnRender (Cairo.Context cr)
		{
			cr.MoveTo (0, 0);
			cr.LineTo (Width, 0);			
			cr.Stroke ();
		}
	}
}

