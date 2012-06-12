using System;

namespace GMathCad.UI.Framework
{
	public class Decorator : FrameworkElement
	{
		private UIElement child;		
		
		public Decorator ()
		{
		}
		
		public UIElement Child 
		{ 
			get { return child; }
			set
			{
				if (child == value) return;
				
				if (child != null) RemoveVisualChild (child);
				
				if (value != null) AddVisualChild (value);
				
				child = value;					
			}
		}
	}
}

