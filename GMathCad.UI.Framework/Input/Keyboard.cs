using System;
using Gtk;

namespace GMathCad.UI.Framework
{
	public static class Keyboard
	{
		public static KeyboardDevice Device {get; private set;}
		
		static Keyboard ()
		{
			Device = new KeyboardDevice ();
		}
		
		public static Visual FocusedElement 
		{
			get	{ return Device.FocusedElement;	}
			set { Device.FocusedElement = value; }
		}
		
		public static event KeyPressEventHandler PreviewKeyPressEvent
		{
			add { Device.PreviewKeyPressEvent += value; }
			remove {Device.PreviewKeyPressEvent -= value; }
		}	
		
		public static event KeyPressEventHandler KeyPressEvent
		{
			add { Device.KeyPressEvent += value; }
			remove {Device.KeyPressEvent -= value; }
		}
	}
}