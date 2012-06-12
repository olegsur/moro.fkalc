using System;
using System.Collections.Generic;
using GMathCad.UI.Framework;

namespace GMathCad.UI
{
	public class DocumentView : UserControl
	{
		//private List<MathRegion> regions = new List<MathRegion>();
		
		private DocumentCursor documentCursor = new DocumentCursor();
		private Canvas canvas = new Canvas();
		
		public DocumentView ()
		{		
			canvas.PreviewKeyPressEvent += HandlePreviewKeyPressEvent;
			canvas.ButtonPressEvent += HandleButtonPressEvent;
			
			Keyboard.FocusedElement = this;
			
			Content = canvas;
			
			canvas.Children.Add (new CanvasChildContainer (documentCursor));
		}

		private void HandleButtonPressEvent (object o, Gtk.ButtonPressEventArgs args)
		{		
			if (Mouse.Device.TargetElement != canvas)
			{
				documentCursor.IsVisible = false;
				Screen.QueueDraw ();
				return;
			}
			
			canvas.SetLeft (args.Event.X, documentCursor);
			canvas.SetTop (args.Event.Y, documentCursor);
						
			documentCursor.IsVisible = true;
    
			Screen.QueueDraw ();			
		}
		
		private void HandlePreviewKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
		{
			ProcessKey(args.Event);
		}
		
		private void ProcessKey (Gdk.EventKey evnt)
		{
			if (Keyboard.FocusedElement == canvas)
		    {
				documentCursor.IsVisible = false;
		
				var region = new MathRegion();
				
				canvas.Children.Add (new CanvasChildContainer(region));                				
				canvas.SetLeft (canvas.GetLeft (documentCursor), region);
				canvas.SetTop (canvas.GetTop (documentCursor), region);
				
				Keyboard.FocusedElement = region;
		    }		    
		
		    Screen.QueueDraw ();
		}
	}
}

