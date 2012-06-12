using System;
using Gtk;

namespace GMathCad.UI.Framework
{
	public class WidgetMouseInputProvider : IMouseInputProvider
	{
		public event ButtonPressEventHandler ButtonPressEvent;
		public event MotionNotifyEventHandler MotionNotifyEvent;
		
		public Visual RootElement { get; private set; }
		
		public WidgetMouseInputProvider (ElementHost elementHost)
		{
			elementHost.Host.ButtonPressEvent += HandleWidgetButtonPressEvent;	
			elementHost.Host.MotionNotifyEvent += HandleWidgetMotionNotifyEvent;
			
			RootElement = elementHost.Element;
		}		

		private void HandleWidgetButtonPressEvent (object o, ButtonPressEventArgs args)
		{
			if (ButtonPressEvent != null)
			{
				ButtonPressEvent(this, args);				
			}
		}
		
		private void HandleWidgetMotionNotifyEvent (object o, MotionNotifyEventArgs args)
		{
			if (MotionNotifyEvent != null)
			{
				MotionNotifyEvent(this, args);
			}			
		}
	}
}