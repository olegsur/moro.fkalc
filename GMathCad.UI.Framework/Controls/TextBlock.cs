using System;

namespace GMathCad.UI.Framework
{
	public class TextBlock : FrameworkElement
	{
		public string Text { get; set; }
		
		public TextBlock ()
		{
		}	
		
		protected override void OnRender (Cairo.Context cr)
		{	
			cr.MoveTo(0, DesiredSize.Height);
			
			cr.SelectFontFace("Georgia",Cairo.FontSlant.Normal, Cairo.FontWeight.Normal);
			cr.SetFontSize(20);
			
		    cr.ShowText(Text);
		}
		
		protected override Size MeasureOverride (Size availableSize, Cairo.Context cr)
		{
			cr.SelectFontFace("Georgia",Cairo.FontSlant.Normal, Cairo.FontWeight.Normal);
			cr.SetFontSize(20);
			
			var textExtents = cr.TextExtents(Text);
			
			return new Size (textExtents.Width, textExtents.Height);
		}		
	}
}

