using System;
using Gtk;
using GMathCad.UI;
using GMathCad.UI.Framework;

public partial class MainWindow: Gtk.Window
{	
	private DocumentView rootElement = new DocumentView();
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		
		var elementHost = new ElementHost(this, rootElement);
		
		Keyboard.Device.RegisterKeyboardInputProvider(new WidgetKeyboardInputProvider(elementHost));
		Mouse.Device.RegistedMouseInputProvider(new WidgetMouseInputProvider(elementHost));
		
		GMathCad.UI.Framework.Screen.Current = this;
	}	
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnExposeEvent (object o, Gtk.ExposeEventArgs args)
	{
		using (var cr = Gdk.CairoHelper.Create(this.GdkWindow)) {				
			
			var width = 0;
			var height = 0;
			
			this.GetSize (out width, out height);
			
			var size = new Size (width, height);
			
			rootElement.Measure (size, cr);
			rootElement.Arrange (size);
			rootElement.Render (cr);
		}		
	}
}
