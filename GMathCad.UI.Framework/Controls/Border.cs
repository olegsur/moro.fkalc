// 
// Border.cs
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
	public class Border : Decorator
	{ 
		public Thickness Padding  { get; set; }
		public Color BorderColor { get; set; }
		
		public Border ()
		{
			BorderColor = new Color (0, 0, 0);
			Padding = new Thickness (5);
		}
		
		protected override void OnRender (Cairo.Context cr)
		{		
			if (Child == null) return;
			
			cr.NewSubPath();
			
			cr.Save ();
			
			cr.Color = new Cairo.Color (BorderColor.R, BorderColor.G, BorderColor.B);	
			cr.Rectangle (0, 0, Width, Height);			
			
			cr.Stroke ();
			
			cr.Restore ();
			
			cr.NewSubPath ();
			
			cr.Save ();
			
			Cairo.Matrix m = new Cairo.Matrix(1, 0, 0, 1, Padding.Left, Padding.Top);
			cr.Transform (m);			
			
			Child.Render (cr);
			
			cr.Restore ();
			
			cr.Stroke ();
		}
		
		protected override Size MeasureOverride (Size availableSize, Cairo.Context cr)
		{
			if (Child == null)
				return Size.Empty;
			
			Child.Measure (availableSize, cr);
			
			var width = Child.DesiredSize.Width + Padding.Left + Padding.Right;
			var height = Child.DesiredSize.Height + Padding.Top + Padding.Bottom;
			
			return new Size(width, height);
		}
		
		protected override void ArrangeOverride (Size finalSize)
		{
			if (Child == null)
				return;
				
			Child.Arrange (!Child.DesiredSize.IsEmpty ? Child.DesiredSize : finalSize);
		}
	}
}

