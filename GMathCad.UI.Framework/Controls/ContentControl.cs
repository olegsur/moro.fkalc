using System;

namespace GMathCad.UI.Framework
{
	public class ContentControl : FrameworkElement
	{
		private UIElement content;		
		
		public ContentControl ()
		{			
		}
		
		public UIElement Content 
		{ 
			get { return content; }
			set
			{
				if (content == value) return;
				
				if (content != null) RemoveVisualChild (content);
				
				if (value != null) AddVisualChild (value);
				
				content = value;					
			}
		}
		
		protected override Size MeasureOverride (Size availableSize, Cairo.Context cr)
		{
			if (Content == null) return Size.Empty;
			
			Content.Measure (availableSize, cr);
			
			return Content.DesiredSize;
		}
		
		protected override void ArrangeOverride (Size finalSize)
		{
			if (Content == null)
				return;
			
			Content.Arrange (!Content.DesiredSize.IsEmpty ? new Size (Width, Height) : finalSize);
		}
		
		protected override void OnRender (Cairo.Context cr)
		{
			if (Content == null) return;
			
			Content.Render (cr);
		}
		
		public override Visual HitTest (double x, double y)
		{
			if (Content == null) return null;
			
			return Content.HitTest (x, y);
		}
	}
}