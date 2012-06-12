using System;
using Gtk;

namespace GMathCad.UI.Framework
{
	public static class Mouse
	{
		public static MouseDevice Device {get; private set;}
		
		static Mouse ()
		{
			Device = new MouseDevice ();
		}
		
		public static event ButtonPressEventHandler PreviewButtonPressEvent
		{
			add { Device.PreviewButtonPressEvent += value; }
			remove {Device.PreviewButtonPressEvent -= value; }
		}
		
		public static event ButtonPressEventHandler ButtonPressEvent
		{
			add { Device.ButtonPressEvent += value; }
			remove {Device.ButtonPressEvent -= value; }
		}
		
		public static event MotionNotifyEventHandler PreviewMotionNotifyEvent
		{
			add { Device.PreviewMotionNotifyEvent += value; }
			remove {Device.PreviewMotionNotifyEvent -= value; }
		}
		
		public static event MotionNotifyEventHandler MotionNotifyEvent
		{
			add { Device.MotionNotifyEvent += value; }
			remove {Device.MotionNotifyEvent -= value; }
		}
		
		public static event EventHandler MouseEnterEvent
		{
			add { Device.MouseEnterEvent += value; }
			remove {Device.MouseEnterEvent -= value; }
		}
		
		public static event EventHandler MouseLeaveEvent
		{
			add { Device.MouseLeaveEvent += value; }
			remove {Device.MouseLeaveEvent -= value; }
		}
	}
}

