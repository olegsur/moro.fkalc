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

