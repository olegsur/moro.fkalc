using System;
namespace GMathCad.UI.Framework
{
	public class Control : FrameworkElement
	{
		public Color Background { get; set; }
		public Color BorderColor { get; set; }
		
		public Control ()
		{
			Background = new Color (255, 255, 255, 0);
		}
		
		protected override void OnRender (Cairo.Context cr)
		{
			base.OnRender (cr);
			
			if (DesiredSize.IsEmpty || !IsVisible)
				return;
			
			cr.Save ();
			
			cr.Color = new Cairo.Color (Background.R, Background.G, Background.B, Background.Alfa);
			
			cr.Rectangle (0, 0, Width, Height);			
			cr.Fill ();
			
			cr.Color = new Cairo.Color (BorderColor.R, BorderColor.G, BorderColor.B, BorderColor.Alfa);
			
			cr.Rectangle (0, 0, Width, Height);			
			cr.Stroke ();
			
			cr.Restore ();
		}
	}
}

