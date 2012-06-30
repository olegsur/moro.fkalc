// 
// DocumentView.cs
//  
// Author:
//       Oleg Sur <oleg.sur@gmail.com>
// 
// Copyright (c) 2012 Oleg Sur
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using fkalc.UI.Framework;

namespace fkalc.UI
{
	public class DocumentView : UserControl
	{
		//private List<MathRegion> regions = new List<MathRegion>();
		
		private DocumentCursor documentCursor = new DocumentCursor ();
		private Canvas canvas = new Canvas ();
		
		public DocumentView ()
		{		
			canvas.PreviewKeyPressEvent += HandlePreviewKeyPressEvent;
			canvas.ButtonPressEvent += HandleButtonPressEvent;
			
			Keyboard.FocusedElement = this;
			
			Content = canvas;
			
			canvas.Children.Add (documentCursor);
			
			Background = Brushes.White;
		}

		private void HandleButtonPressEvent (object o, Gtk.ButtonPressEventArgs args)
		{		
			if (Mouse.Device.TargetElement != canvas) {
				documentCursor.Visibility = Visibility.Collapsed;
				Screen.QueueDraw ();
				return;
			}
			
			canvas.SetLeft (args.Event.X, documentCursor);
			canvas.SetTop (args.Event.Y, documentCursor);
						
			documentCursor.Visibility = Visibility.Visible;
    
			Screen.QueueDraw ();			
		}
		
		private void HandlePreviewKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
		{
			ProcessKey (args.Event);
		}
		
		private void ProcessKey (Gdk.EventKey evnt)
		{
			if (Keyboard.FocusedElement == canvas) {
				documentCursor.Visibility = Visibility.Collapsed;
		
				var region = new MathRegion ();
				
				canvas.Children.Add (region);                				
				canvas.SetLeft (canvas.GetLeft (documentCursor), region);
				canvas.SetTop (canvas.GetTop (documentCursor), region);
				
				Keyboard.FocusedElement = region;
			}		    
		
			Screen.QueueDraw ();
		}
	}
}

