using System;
using Gtk;

namespace GMathCad.UI.Framework
{
	public class ElementHost
	{
		public Widget Host { get; private set; }
		public Visual Element { get; private set; }
		
		public ElementHost (Widget widget, Visual element)
		{
			Host = widget;
			Element = element;
		}
	}
}

