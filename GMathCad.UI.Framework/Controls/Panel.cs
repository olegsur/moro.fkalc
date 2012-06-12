using System;
using System.Collections.Generic;
using System.Linq;

namespace GMathCad.UI.Framework
{
	public abstract class Panel<TChild> : FrameworkElement where TChild : UIElement
	{
		public PanelChildrenCollection<TChild> Children { get; private set; }		
		
		public Panel ()
		{		
			Children = new PanelChildrenCollection<TChild> (this);
		}		
	}
}

