using System;
using System.Linq;
using System.Collections.Generic;

namespace GMathCad.UI.Framework
{
	public class StackPanel : Panel<StackPanelChildContainer>
	{	
		public Orientation Orientation { get; set; }
		
		public StackPanel ()
		{
			Orientation = Orientation.Vertical;
		}		
		
		public void SetMargin (Thickness margin, UIElement element)
		{
			var el = Children.FirstOrDefault (c => c.Content == element);
			
			if (el == null)
				return;
			
			el.Margin = margin;
		}
		
		public void SetHorizontalAlignment (HorizontalAlignment alignment, UIElement element)
		{
			var el = Children.FirstOrDefault (c => c.Content == element);
			
			if (el == null)
				return;
			
			el.HorizontalAlignment = alignment;
		}
						
		protected override Size MeasureOverride (Size availableSize, Cairo.Context cr)
		{
			foreach (var container in Children) {
				container.Measure (availableSize, cr);
			}
			
			var width = 0d;
			var height = 0d;			
						
			if (Orientation == Orientation.Horizontal) {
				width = Children.Where (c => !c.DesiredSize.IsEmpty).Sum (c => c.DesiredSize.Width + c.Margin.Left + c.Margin.Right);
				height = Children.Where (c => !c.DesiredSize.IsEmpty).Max (c => c.DesiredSize.Height + c.Margin.Top + c.Margin.Bottom);			
			} else {
				width = Children.Where (c => !c.DesiredSize.IsEmpty).Max (c => c.DesiredSize.Width + c.Margin.Left + c.Margin.Right);
				height = Children.Where (c => !c.DesiredSize.IsEmpty).Sum (c => c.DesiredSize.Height + c.Margin.Top + c.Margin.Bottom);			
			}
		
			return new Size (width, height);
		}
		
		protected override void ArrangeOverride (Size finalSize)
		{
			foreach (var container in Children) {	
				var width = !container.DesiredSize.IsEmpty ? container.DesiredSize.Width : finalSize.Width;
				var height = !container.DesiredSize.IsEmpty ? container.DesiredSize.Height : finalSize.Height;
				
				if (Orientation == Orientation.Vertical && container.HorizontalAlignment == HorizontalAlignment.Stretch) {
					width = finalSize.Width;
				}
				
				container.Arrange (new Size (width, height));
			}
		}
		
		protected override void OnRender (Cairo.Context cr)
		{
			var x = 0d;
			var y = 0d;
			
			foreach (var child in Children) {
				cr.Save ();
				
				if (Orientation == Orientation.Horizontal)
				{
					x += child.Margin.Left;	
					y = (Height - child.Height) / 2;
				}
				else
				{
					x = (Width - child.Width) / 2;
					y += child.Margin.Top;
				}
				
				
				var matrix = new Cairo.Matrix (1, 0, 0, 1, x, y);				
				cr.Transform (matrix);
				
				child.Render (cr);
				
				cr.Restore ();
				
				if (Orientation == Orientation.Horizontal)				
					x += child.Width + child.Margin.Right;				
				else
					y += child.Height + child.Margin.Bottom;
			}
		}
		
		
	}
}