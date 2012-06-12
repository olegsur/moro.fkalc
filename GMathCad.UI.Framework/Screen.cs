using System;
using Gtk;

namespace GMathCad.UI.Framework
{
	public static class Screen
	{
		public static Widget Current { get; set; }	
		
		public static void QueueDraw ()
		{
			if (Current == null) return;
			
			Current.QueueDraw ();
		}
	}
}

