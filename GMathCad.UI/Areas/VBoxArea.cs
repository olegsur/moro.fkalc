using System;
using GMathCad.UI.Framework;

namespace GMathCad.UI
{
	public class VBoxArea: Area
	{
		private StackPanel panel = new StackPanel() { Orientation = Orientation.Vertical };  
		
		public VBoxArea ()
		{
			Content = panel;
		}
		
		public void AddArea (Area area)
		{
			panel.Children.Add (new StackPanelChildContainer(area));			
			
			area.Parent = this;	
			
			panel.SetMargin (new Thickness (5), area);
		}
	}
}

