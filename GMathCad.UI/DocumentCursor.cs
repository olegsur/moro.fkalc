using System;
using Cairo;
using GMathCad.UI.Framework;

namespace GMathCad.UI
{
	public class DocumentCursor : UIElement
	{
        public bool IsVisible { get; set; }
		
		public DocumentCursor ()
		{
			IsVisible = true;
		}	
		
	
		protected override void OnRender (Context cr)
		{
			if (!IsVisible) return;
			
			cr.NewSubPath ();

        	cr.MoveTo (5, 0);
        	cr.LineTo (5, 10);
        	cr.MoveTo (0, 5);
        	cr.LineTo (10, 5);
			
			cr.Stroke ();
		}
	}
}