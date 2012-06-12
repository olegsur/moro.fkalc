using Gtk;

namespace GMathCad.UI.Framework
{
	public class WidgetKeyboardInputProvider : IKeyboardInputProvider
	{
		public event KeyPressEventHandler KeyPressEvent;
		
		public WidgetKeyboardInputProvider (ElementHost elementHost)
		{
			elementHost.Host.KeyPressEvent += (o, args) => RaiseKeyPressEventHandler (args);
		}
		
		private void RaiseKeyPressEventHandler (KeyPressEventArgs args)
		{
			if (KeyPressEvent != null)
			{
				KeyPressEvent (this, args);
			}
		}
	}
}

