using System;
namespace GMathCad.UI.Framework
{
	public class StackPanelChildContainer : ContentControl
	{
		public Thickness Margin { get; set; }

		public HorizontalAlignment HorizontalAlignment { get; set; }
			
		public StackPanelChildContainer (UIElement child)
		{
			Content = child;
			
			HorizontalAlignment = HorizontalAlignment.Center;
		}
	}
}

