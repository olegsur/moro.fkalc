// 
// Ellipse.cs
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
	public class Ellipse : Shape
	{
		public Ellipse ()
		{
		}
		
		protected override void OnRender (Cairo.Context cr)
		{
			cr.Save ();			
			
			var anialias = cr.Antialias;
			
			cr.Antialias = SnapsToDevicePixels ? Cairo.Antialias.None : anialias;
						
			cr.LineWidth = StrokeThickness;
			cr.Color = new Cairo.Color (Stroke.R, Stroke.G, Stroke.B);
			
			cr.Scale (1, Height / Width);
			
			cr.Arc (Width / 2, Width / 2, Width / 2 - StrokeThickness / 2, 0, 2 * Math.PI);			
						
			cr.Stroke ();
			
			cr.Restore ();
			cr.Antialias = anialias;
		}
	}
}

