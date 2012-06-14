// 
// MouseDevice.cs
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
using System.Collections.Generic;
using System.Linq;

namespace GMathCad.UI.Framework
{
	public class MouseDevice
	{
		public event ButtonPressEventHandler PreviewButtonPressEvent;
		public event ButtonPressEventHandler ButtonPressEvent;
		
		public event MotionNotifyEventHandler PreviewMotionNotifyEvent;
		public event MotionNotifyEventHandler MotionNotifyEvent;
		
		public event EventHandler MouseEnterEvent;
		public event EventHandler MouseLeaveEvent;
		
		private Visual targetElement;
		
		private List<IMouseInputProvider> providers = new List<IMouseInputProvider>();
		
		public MouseDevice ()
		{
		}
		
		public Visual TargetElement 
		{ 
			get { return targetElement; }
			private set
			{
				if (targetElement == value) return;
				
				RaiseMouseLeaveEvent ();
				
				targetElement = value;
				
				RaiseMouseEnterEvent ();				
			}			
		}
		
		
		public void RegistedMouseInputProvider (IMouseInputProvider provider)
		{
			provider.ButtonPressEvent += HandleProviderButtonPressEvent;
			provider.MotionNotifyEvent += HandleMotionNotifyEvent;
			providers.Add(provider);
		}		
		
		public void UnregisterMouseInputProvider (IMouseInputProvider provider)
		{
			provider.ButtonPressEvent -= HandleProviderButtonPressEvent;
			provider.MotionNotifyEvent -= HandleMotionNotifyEvent;
			providers.Remove(provider);			
		}

		private void HandleProviderButtonPressEvent (object o, ButtonPressEventArgs args)
		{
			RaisePreviewButtonPressEvent(args);
			RaiseButtonPressEvent(args);
		}
		
		private void HandleMotionNotifyEvent (object o, MotionNotifyEventArgs args)
		{
			var provider = providers.FirstOrDefault(p => p == o);
			
			if (provider == null) return;
			
			TargetElement = provider.RootElement.HitTest (args.Event.X, args.Event.Y);
			
			RaisePreviewMotionNotifyEvent (args);
			RaiseMotionNotifyEvent (args);
		}
		
		private void RaisePreviewButtonPressEvent (ButtonPressEventArgs args)
		{
			if (PreviewButtonPressEvent == null || TargetElement == null) return;
			
			foreach(var element in new EventRouteFactory().Build(TargetElement))
			{
				var del = PreviewButtonPressEvent.GetInvocationList().FirstOrDefault(d => d.Target == element);
				
				if (del != null)
				{
					del.DynamicInvoke (this, args);
				}	
			}
		}	
		
		private void RaiseButtonPressEvent (ButtonPressEventArgs args)
		{
			if (ButtonPressEvent == null || TargetElement == null) return;
			
			foreach(var element in new EventRouteFactory().Build(TargetElement))
			{
				var del = ButtonPressEvent.GetInvocationList().FirstOrDefault(d => d.Target == element);
				
				if (del != null)
				{
					del.DynamicInvoke (this, args);
				}	
			}
		}
		
		private void RaisePreviewMotionNotifyEvent (MotionNotifyEventArgs args)
		{
			if (PreviewMotionNotifyEvent == null || TargetElement == null) return;
			
			foreach(var element in new EventRouteFactory().Build(TargetElement))
			{
				var del = PreviewMotionNotifyEvent.GetInvocationList().FirstOrDefault(d => d.Target == element);
				
				if (del != null)
				{
					del.DynamicInvoke (this, args);
				}	
			}
		}	
		
		private void RaiseMotionNotifyEvent (MotionNotifyEventArgs args)
		{
			if (MotionNotifyEvent == null || TargetElement == null) return;
			
			foreach(var element in new EventRouteFactory().Build(TargetElement).Reverse())
			{
				var del = MotionNotifyEvent.GetInvocationList().FirstOrDefault(d => d.Target == element);
				
				if (del != null)
				{
					del.DynamicInvoke (this, args);
				}	
			}
		}
		
		private void RaiseMouseEnterEvent ()
		{
			if (MouseEnterEvent == null || TargetElement == null) return;
			
			var del = MouseEnterEvent.GetInvocationList().FirstOrDefault(d => d.Target == TargetElement);
				
			if (del != null)
			{
				del.DynamicInvoke (this, EventArgs.Empty);
			}
		}
		
		private void RaiseMouseLeaveEvent ()
		{
			if (MouseLeaveEvent == null || TargetElement == null) return;
			
			var del = MouseLeaveEvent.GetInvocationList().FirstOrDefault(d => d.Target == TargetElement);
				
			if (del != null)
			{
				del.DynamicInvoke (this, EventArgs.Empty);
			}
		}
	}
}

