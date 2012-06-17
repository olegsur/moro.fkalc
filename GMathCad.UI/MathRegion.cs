// 
// MathRegion.cs
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
using GMathCad.UI.Framework;

namespace GMathCad.UI
{
	public class MathRegion : UserControl
	{
		private HBoxArea root = new HBoxArea ();
		private TextArea activeArea = new TextArea ();
		private Border border;
		
		public TextArea ActiveArea 
		{ 
			get { return activeArea; } 
			set { activeArea = value; }
		}
		
		public MathRegion ()
		{
			root.AddArea (activeArea);
			
			border = new Border ()
			{
				Child = root
			};	
			
			Content = border;
			
			KeyPressEvent += HandleKeyPressEvent;
			
			MouseEnterEvent += HandleMouseEnterEvent;
			MouseLeaveEvent += HandleMouseLeaveEvent;
		}

		private void HandleMouseEnterEvent (object sender, EventArgs e)
		{
			border.BorderColor = new Color (255, 0, 0);
			
			Screen.QueueDraw ();
		}
		
		private void HandleMouseLeaveEvent (object sender, EventArgs e)
		{
			border.BorderColor = new Color (0, 0, 0);
			
			Screen.QueueDraw ();
		}

		private void HandleKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
		{
			ProcessKey (args.Event.KeyValue);
		}
		
		private void ProcessKey (uint key)
		{	
			var factory = new InsertCharacterCommandFactory ();
			if (factory.IsSupported (key)) {
				factory.Build (key, this).Execute ();
				return;
			}
			
			var plusFactory = new PlusCommandFactory ();
			if (plusFactory.IsSupported (key)) {
				plusFactory.Build (this).Execute ();
				return;
			}
			
			var devideFactory = new DivideCommandFactory ();
			if (devideFactory.IsSupported (key)) {
				devideFactory.Build (this).Execute ();
				return;
			}
		}
		
		public override Visual HitTest (double x, double y)
		{
			if (x >= 0 && x <= Width &&
				y >= 0 && y <= Height) {
				return this;
			}
			
			return null;
		}
		
	}
}