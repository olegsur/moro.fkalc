// 
// Mouse.cs
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

namespace fKalc.UI.Framework
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

