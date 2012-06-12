using Gtk;

namespace GMathCad.UI.Framework
{
	public interface IMouseInputProvider
	{
		event ButtonPressEventHandler ButtonPressEvent;
		event MotionNotifyEventHandler MotionNotifyEvent;
		
		Visual RootElement { get; }
	}
}