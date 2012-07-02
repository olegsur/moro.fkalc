// 
// Keyboard.cs
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
using Gtk;

namespace fkalc.UI.Framework
{
	public static class Keyboard
	{
		public static KeyboardDevice Device { get; private set; }
		
		static Keyboard ()
		{
			Device = new KeyboardDevice ();
		}
		
		public static Visual FocusedElement {
			get	{ return Device.FocusedElement;	}
		}
		
		public static event KeyPressEventHandler PreviewKeyPressEvent {
			add { Device.PreviewKeyPressEvent += value; }
			remove { Device.PreviewKeyPressEvent -= value; }
		}	
		
		public static event KeyPressEventHandler KeyPressEvent {
			add { Device.KeyPressEvent += value; }
			remove { Device.KeyPressEvent -= value; }
		}

		public static event EventHandler GotKeyboardFocusEvent {
			add { Device.GotKeyboardFocusEvent += value; }
			remove { Device.GotKeyboardFocusEvent -= value; }
		}

		public static event EventHandler LostKeyboardFocusEvent {
			add { Device.LostKeyboardFocusEvent += value; }
			remove { Device.LostKeyboardFocusEvent -= value; }
		}

		public static void Focus (Visual visual)
		{
			Device.Focus (visual);
		}
	}
}