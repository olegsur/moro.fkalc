//
// CairoContext.cs
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

namespace fkalc.UI.Framework
{
	public class CairoContext : DrawingContext
	{
		private Cairo.Context cr;

		public CairoContext (Cairo.Context cr)
		{
			this.cr = cr;
		}

		public override void DrawLine (Pen pen, Point point0, Point point2)
		{
			cr.Color = new Cairo.Color (pen.Color.R, pen.Color.G, pen.Color.B, pen.Color.Alfa);
			cr.LineWidth = pen.Thickness;

			cr.MoveTo (point0.X, point0.Y);
			cr.LineTo (point2.X, point2.Y);

			cr.Stroke ();
		}

		public override void DrawEllipse (Brush brush, Pen pen, Point center, double radiusX, double radiusY)
		{
			cr.Save ();		

			cr.Translate (center.X, center.Y);
			cr.Scale (1, radiusY / radiusX);

			if (brush != null) {
				if (brush is SolidColorBrush) {
					var b = brush as SolidColorBrush;
					cr.Color = new Cairo.Color (b.Color.R, b.Color.G, b.Color.B, b.Color.Alfa);	
					cr.Arc (0, 0, radiusX - pen.Thickness / 2, 0, 2 * Math.PI);			
			
					cr.Fill ();
				}
			}

			if (pen != null) {
				cr.Color = new Cairo.Color (pen.Color.R, pen.Color.G, pen.Color.B, pen.Color.Alfa);	
				cr.LineWidth = pen.Thickness;
				cr.Arc (0, 0, radiusX - pen.Thickness / 2, 0, 2 * Math.PI);				
			
				cr.Stroke ();
			}
									
			cr.Stroke ();
			
			cr.Restore ();
		}

		public override void DrawText (FormattedText formattedText, Point origin)
		{
			cr.Color = new Cairo.Color (0, 0, 0);	
			cr.MoveTo (origin.X, origin.Y);
			
			cr.SelectFontFace (formattedText.FontFamily, Cairo.FontSlant.Normal, Cairo.FontWeight.Normal);
			cr.SetFontSize (formattedText.FontSize);
			
			cr.ShowText (formattedText.Text);
		}

		public override void DrawRectangle (Brush brush, Pen pen, Rect rectangle)
		{
			if (brush != null) {
				if (brush is SolidColorBrush) {
					var b = brush as SolidColorBrush;
					cr.Color = new Cairo.Color (b.Color.R, b.Color.G, b.Color.B, b.Color.Alfa);	
					cr.Rectangle (rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);			
			
					cr.Fill ();
				}
			}

			if (pen != null) {
				cr.Color = new Cairo.Color (pen.Color.R, pen.Color.G, pen.Color.B, pen.Color.Alfa);	
				cr.Rectangle (rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);			
			
				cr.Stroke ();
			}			
		}

		public override void PushTransform (Transform transform)
		{
			cr.Save ();

			var m = transform.Value;
			cr.Transform (new Cairo.Matrix (m.M11, m.M12, m.M21, m.M22, m.OffsetX, m.OffsetY));
		}

		public override void Pop ()
		{
			cr.Restore ();
		}

		public override Antialias Antialias {
			get {
				switch (cr.Antialias) {
				case Cairo.Antialias.Default:
					return Antialias.Default;
				case Cairo.Antialias.None:
					return Antialias.None;
				case Cairo.Antialias.Gray:
					return Antialias.Gray;
				case Cairo.Antialias.Subpixel:
					return Antialias.Subpixel;
				default:
					return Antialias.Default;
				}

			}
			set {
				switch (value) {
				case Antialias.Default:
					cr.Antialias = Cairo.Antialias.Default;
					break;
				case Antialias.None:
					cr.Antialias = Cairo.Antialias.None;
					break;
				case Antialias.Gray:
					cr.Antialias = Cairo.Antialias.Gray;
					break;
				case Antialias.Subpixel:
					cr.Antialias = Cairo.Antialias.Subpixel;
					break;
				default:
					cr.Antialias = Cairo.Antialias.Default;
					break;
				}
			}
		}


	}
}

