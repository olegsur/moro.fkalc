// 
// UIElement.cs
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

namespace GMathCad.UI.Framework
{
	public class UIElement : Visual
	{		
		public event ButtonPressEventHandler ButtonPressEvent;
		public event MotionNotifyEventHandler PreviewMotionNotifyEvent;
		public event MotionNotifyEventHandler MotionNotifyEvent;
		public event EventHandler MouseEnterEvent;
		public event EventHandler MouseLeaveEvent;
		public event KeyPressEventHandler PreviewKeyPressEvent;
		public event KeyPressEventHandler KeyPressEvent;
		
		public Size DesiredSize { get; set; }

		public bool IsFocused { get; set; }
		
		public Visibility Visibility { get; set; }
		public bool IsVisible { get { return Visibility == Visibility.Visible;} }
		
		public UIElement ()
		{
			Mouse.PreviewButtonPressEvent += HandlePreviewButtonPressEvent;
			Mouse.ButtonPressEvent += OnButtonPressEvent;
			
			Mouse.PreviewMotionNotifyEvent += OnPreviewMotionNotifyEvent;
			Mouse.MotionNotifyEvent += OnMotionNotifyEvent;
			
			Mouse.MouseEnterEvent += OnMouseEnterEvent;
			Mouse.MouseLeaveEvent += OnMouseLeaveEvent;
			
			Keyboard.PreviewKeyPressEvent += OnPreviewKeyPressEvent;
			Keyboard.KeyPressEvent += OnKeyPressEvent;
		}
		
		public void Measure (Size availableSize, Cairo.Context cr)
		{	
			DesiredSize = MeasureCore (availableSize, cr);
		}
		
		public void Arrange (Size finalSize)
		{
			ArrangeCore (finalSize);
		}
		
		protected virtual Size MeasureCore (Size availableSize, Cairo.Context cr)
		{		
			return Size.Empty;
		}
		
		protected virtual void ArrangeCore (Size finalSize)
		{			
		}
		
		protected virtual void OnRender (Cairo.Context cr)
		{			
		}
		
		public void Render (Cairo.Context cr)
		{
			OnRender (cr);
		}
		
		private void HandlePreviewButtonPressEvent (object o, ButtonPressEventArgs args)
		{
			//if (IsFocused) return;
			
			if (Mouse.Device.TargetElement != this)
				return;
			
			IsFocused = true;
			
			Keyboard.FocusedElement = this;
		}
				
		protected virtual void OnButtonPressEvent (object o, ButtonPressEventArgs args)
		{
			RaiseButtonPressEvent (args);
		}
		
		protected virtual void OnPreviewKeyPressEvent (object o, KeyPressEventArgs args)
		{
			RaisePreviewKeyPressEvent (args);
		}
		
		protected virtual void OnKeyPressEvent (object o, KeyPressEventArgs args)
		{
			RaiseKeyPressEvent (args);
		}
				
		protected virtual void OnPreviewMotionNotifyEvent (object o, MotionNotifyEventArgs args)
		{
			RaisePreviewMotionNotifyEvent (args);
		}
		
		protected virtual void OnMotionNotifyEvent (object o, MotionNotifyEventArgs args)
		{
			RaiseMotionNotifyEvent (args);
		}
				
		protected virtual void OnMouseEnterEvent (object sender, EventArgs args)
		{
			RaiseMouseEnterEvent (args);
		}
				
		protected virtual void OnMouseLeaveEvent (object sender, EventArgs args)
		{
			RaiseMouseLeaveEvent (args);
		}
		
		private void RaiseButtonPressEvent (ButtonPressEventArgs args)
		{
			if (ButtonPressEvent != null) {
				ButtonPressEvent (this, args);
			}
		}
		
		private void RaisePreviewMotionNotifyEvent (MotionNotifyEventArgs args)
		{
			if (PreviewMotionNotifyEvent != null) {
				PreviewMotionNotifyEvent (this, args);
			}
		}
		
		private void RaiseMotionNotifyEvent (MotionNotifyEventArgs args)
		{
			if (MotionNotifyEvent != null) {
				MotionNotifyEvent (this, args);
			}
		}
		
		private void RaiseMouseEnterEvent (EventArgs args)
		{
			if (MouseEnterEvent != null) {
				MouseEnterEvent (this, args);
			}
		}
		
		private void RaiseMouseLeaveEvent (EventArgs args)
		{
			if (MouseLeaveEvent != null) {
				MouseLeaveEvent (this, args);
			}
		}
		
		private void RaisePreviewKeyPressEvent (KeyPressEventArgs args)
		{
			if (PreviewKeyPressEvent != null) {
				PreviewKeyPressEvent (this, args);
			}
		}
				
		private void RaiseKeyPressEvent (KeyPressEventArgs args)
		{
			if (KeyPressEvent != null) {
				KeyPressEvent (this, args);
			}
		}
	}
}

