// 
// KeyboardDevice.cs
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
using System.Linq;

using Gtk;

namespace fkalc.UI.Framework
{
	public class KeyboardDevice
	{
		public event KeyPressEventHandler PreviewKeyPressEvent;		
		public event KeyPressEventHandler KeyPressEvent;	
		public event EventHandler GotKeyboardFocusEvent;
		public event EventHandler LostKeyboardFocusEvent;

		public Visual FocusedElement { get; private set; }
		
		private List<IKeyboardInputProvider> providers = new List<IKeyboardInputProvider> ();
		
		public KeyboardDevice ()
		{
		}
		
		public void RegisterKeyboardInputProvider (IKeyboardInputProvider provider)
		{
			provider.KeyPressEvent += HandleProviderKeyPressEvent;
			providers.Add (provider);
		}		
		
		public void UnregisterKeyboardInputProvider (IKeyboardInputProvider provider)
		{
			provider.KeyPressEvent -= HandleProviderKeyPressEvent;
			providers.Remove (provider);			
		}

		public void Focus (Visual visual)
		{
			if (FocusedElement == visual)
				return;

			if (FocusedElement != null)
				RaiseLostKeyboardFocusEvent ();

			FocusedElement = visual;

			if (visual != null)
				RaiseGotKeyboardFocusEvent ();
		}
		
		private void HandleProviderKeyPressEvent (object o, KeyPressEventArgs args)
		{
			if (FocusedElement == null)
				return;
			
			RaisePreviewKeyPressEvent (args);
			RaiseKeyPressEvent (args);			
		}	
		
		private void RaisePreviewKeyPressEvent (KeyPressEventArgs args)
		{
			if (PreviewKeyPressEvent == null)
				return;
			
			foreach (var element in new EventRouteFactory().Build(FocusedElement)) {
				var del = PreviewKeyPressEvent.GetInvocationList ().FirstOrDefault (d => d.Target == element);
				
				if (del != null) {
					del.DynamicInvoke (this, args);
				}	
			}
		}
		
		private void RaiseKeyPressEvent (KeyPressEventArgs args)
		{
			if (KeyPressEvent == null)
				return;
			
			foreach (var element in new EventRouteFactory().Build(FocusedElement).Reverse()) {
				var del = KeyPressEvent.GetInvocationList ().FirstOrDefault (d => d.Target == element);
				
				if (del != null) {
					del.DynamicInvoke (this, args);
				}	
			}
		}

		private void RaiseGotKeyboardFocusEvent ()
		{
			if (GotKeyboardFocusEvent == null)
				return;
			
			foreach (var element in new EventRouteFactory().Build(FocusedElement).Reverse()) {
				var del = GotKeyboardFocusEvent.GetInvocationList ().FirstOrDefault (d => d.Target == element);
				
				if (del != null) {
					del.DynamicInvoke (this, EventArgs.Empty);
				}	
			}
		}

		private void RaiseLostKeyboardFocusEvent ()
		{
			if (LostKeyboardFocusEvent == null)
				return;
			
			foreach (var element in new EventRouteFactory().Build(FocusedElement).Reverse()) {
				var del = LostKeyboardFocusEvent.GetInvocationList ().FirstOrDefault (d => d.Target == element);
				
				if (del != null) {
					del.DynamicInvoke (this, EventArgs.Empty);
				}	
			}
		}
	}
}