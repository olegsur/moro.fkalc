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
			border.BorderColor = new Color (155, 155, 155);
			
			Screen.QueueDraw ();
		}
		
		private void HandleMouseLeaveEvent (object sender, EventArgs e)
		{
			border.BorderColor = new Color (0, 0, 0);
			
			Screen.QueueDraw ();
		}

		private void HandleKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
		{
			ProcessKey (args.Event.Key);
		}
		
		private void ProcessKey (Gdk.Key key)
		{			
			new InsertCharacterCommand (key, this).Execute ();
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