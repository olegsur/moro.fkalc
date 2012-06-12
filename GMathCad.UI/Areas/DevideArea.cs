using System;
using GMathCad.UI.Framework;

namespace GMathCad.UI
{
	public class DevideArea : Area
	{
		private StackPanel panel = new StackPanel() { Orientation = Orientation.Vertical };
		
		//public Area Dividend { get; private set; }
		//public Area Divisor { get; private set; }
		
		public DevideArea (Area dividend, Area divisor) : base ()
		{
			Content = panel;
			
			var line = new Line ()
			{
				Height = 1
			};			
			
			panel.Children.Add (new StackPanelChildContainer(dividend));
			panel.Children.Add (new StackPanelChildContainer(line));			
			panel.Children.Add (new StackPanelChildContainer(divisor));
			
			panel.SetMargin (new Thickness (5), dividend);
			panel.SetMargin (new Thickness (5), divisor);
			
			panel.SetHorizontalAlignment (HorizontalAlignment.Stretch, line);
			
			dividend.Parent = this;
			divisor.Parent = this;
		}
	}
}

