using System;
using System.Collections.Generic;
using Cairo;
using System.Linq;

namespace GMathCad.UI.Framework
{
	public class Canvas : Panel<CanvasChildContainer>
	{
		public Canvas ()
		{
		}
	
		protected override void OnRender (Cairo.Context cr)
		{
			foreach (var element in Children) {
				cr.Save ();				
				
				var matrix = new Cairo.Matrix (1, 0, 0, 1, element.X, element.Y);				
				cr.Transform (matrix);
				
				element.Render (cr);
				
				cr.Restore ();
			}
		}
		
		public double GetLeft (UIElement element)
		{
			var el = Children.Cast<CanvasChildContainer>().FirstOrDefault (c => c.Content == element);
			
			if (el == null)
				return 0;
			
			return el.X;
		}
		
		public void SetLeft (double left, UIElement element)
		{
			var el = Children.Cast<CanvasChildContainer> ().FirstOrDefault (c => c.Content == element);
			
			if (el == null)
				return;
			
			el.X = left;
		}
		
		public double GetTop (UIElement element)
		{
			var el = Children.Cast<CanvasChildContainer> ().FirstOrDefault (c => c.Content == element);
			
			if (el == null)
				return 0;
			
			return el.Y;
		}
		
		public void SetTop (double top, UIElement element)
		{
			var el = Children.Cast<CanvasChildContainer> ().FirstOrDefault (c => c.Content == element);
			
			if (el == null)
				return;
			
			el.Y = top;
		}
				
		protected override Size MeasureOverride (Size availableSize, Cairo.Context cr)
		{
			Children.ToList ().ForEach (r => r.Measure (availableSize, cr));
			
			return availableSize;
		}
		
		protected override void ArrangeOverride (Size finalSize)
		{
			foreach (var container in Children) {				
				container.Arrange (!container.DesiredSize.IsEmpty ? container.DesiredSize : finalSize);
			}
		}
		
		public override Visual HitTest (double x, double y)
		{
			var hitTest = Children.Cast<CanvasChildContainer> ().FirstOrDefault (r => r.HitTest (x, y) != null);
			if (hitTest != null) {
				return hitTest.Content;
			}
			return this;
		}	
		
		
	}
}