using System;

namespace GMathCad.UI.Framework
{
	public class FrameworkElement : UIElement
	{
		public double Height { get; set; }
		public double Width { get; set; }		
		
		public FrameworkElement ()
		{			
		}
		
		protected override Size MeasureCore (Size availableSize, Cairo.Context cr)
		{
			return MeasureOverride (availableSize, cr);
		}
		
		protected override void ArrangeCore (Size finalSize)
		{
			if (finalSize.IsEmpty)
				return;
			
			Height = finalSize.Height;
			Width = finalSize.Width;
			
			ArrangeOverride (finalSize);
		}
		
		protected virtual Size MeasureOverride (Size availableSize, Cairo.Context cr)
		{		
			return Size.Empty;
		}
		
		protected virtual void ArrangeOverride (Size finalSize)
		{	
		}
	}
}