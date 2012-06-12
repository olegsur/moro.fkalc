using System.Linq;
using System.Collections.Generic;
using Gtk;

namespace GMathCad.UI.Framework
{
	public class KeyboardDevice
	{
		public Visual FocusedElement { get; set; }
		public event KeyPressEventHandler PreviewKeyPressEvent;		
		public event KeyPressEventHandler KeyPressEvent;	
		
		private List<IKeyboardInputProvider> providers = new List<IKeyboardInputProvider>();
		
		public KeyboardDevice ()
		{
		}
		
		public void RegisterKeyboardInputProvider (IKeyboardInputProvider provider)
		{
			provider.KeyPressEvent += HandleProviderKeyPressEvent;
			providers.Add(provider);
		}		
		
		public void UnregisterKeyboardInputProvider (IKeyboardInputProvider provider)
		{
			provider.KeyPressEvent -= HandleProviderKeyPressEvent;
			providers.Remove(provider);			
		}
		
		private void HandleProviderKeyPressEvent (object o, KeyPressEventArgs args)
		{
			if (FocusedElement == null) return;
			
			RaisePreviewKeyPressEvent(args);
			RaiseKeyPressEvent(args);			
		}	
		
		private void RaisePreviewKeyPressEvent(KeyPressEventArgs args)
		{
			if (PreviewKeyPressEvent == null) return;
			
			foreach(var element in new EventRouteFactory().Build(FocusedElement))
			{
				var del = PreviewKeyPressEvent.GetInvocationList().FirstOrDefault(d => d.Target == element);
				
				if (del != null)
				{
					del.DynamicInvoke (this, args);
				}	
			}
		}
		
		private void RaiseKeyPressEvent(KeyPressEventArgs args)
		{
			if (KeyPressEvent == null) return;
			
			foreach(var element in new EventRouteFactory().Build(FocusedElement).Reverse())
			{
				var del = KeyPressEvent.GetInvocationList().FirstOrDefault(d => d.Target == element);
				
				if (del != null)
				{
					del.DynamicInvoke (this, args);
				}	
			}
		}
	}
}