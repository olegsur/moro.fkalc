using System;
using GMathCad.UI.Framework;
using System.Linq;

namespace GMathCad.UI
{
	public class HBoxArea : Area
	{
		private StackPanel panel = new StackPanel () { Orientation = Orientation.Horizontal };  
		
		public HBoxArea ()
		{
			Content = panel;
		}
		
		public void AddArea (Area area)
		{
			panel.Children.Add (new StackPanelChildContainer (area));			
			
			area.Parent = this;	
			
			panel.SetMargin (new Thickness (5), area);
		}
		
		public void Replace (Area oldArea, Area newArea)
		{
			var oldContainer = panel.Children.First (c => c.Content == oldArea);
			
			var index = panel.Children.IndexOf (oldContainer);
			panel.Children.Remove (oldContainer);
			panel.Children.Insert (index, new StackPanelChildContainer (newArea));		
		}
	}
}

