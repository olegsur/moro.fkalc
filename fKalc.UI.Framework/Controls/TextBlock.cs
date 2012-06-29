// 
// TextBlock.cs
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

namespace GMathCad.UI.Framework
{
	public class TextBlock : FrameworkElement
	{
		private readonly DependencyProperty<string> text;

		public string Text { 
			get { return text.Value;} 
			set { text.Value = value; }
		}
		
		public TextBlock ()
		{
			text = BuildProperty<string> ("Text");
		}	
		
		protected override void OnRender (DrawingContext dc)
		{	
			dc.DrawText (new FormattedText (Text) {FontFamily = "Georgia", FontSize = 20}, new Point (0, Height));
		}
		
		protected override Size MeasureOverride (Size availableSize)
		{
			var surface = new Cairo.ImageSurface (Cairo.Format.A1, 1, 1);
			
			using (Cairo.Context cr = new Cairo.Context(surface)) {			
				cr.SelectFontFace ("Georgia", Cairo.FontSlant.Normal, Cairo.FontWeight.Normal);
				cr.SetFontSize (20);
			
				var textExtents = cr.TextExtents (Text);
			
				return new Size (textExtents.Width, textExtents.Height);
			}
		}		
	}
}

