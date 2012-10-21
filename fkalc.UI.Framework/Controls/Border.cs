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

namespace fkalc.UI.Framework
{
	public class Border : Decorator
	{ 
		private DependencyProperty<Thickness> padding;		
		private DependencyProperty<Brush> background;
		private DependencyProperty<Color> borderColor;
		private DependencyProperty<double> borderThickness;	
		
		public Thickness Padding { 
			get { return padding.Value;}
			set { padding.Value = value;}
		}
		
		public Brush Background { 
			get { return background.Value;}
			set { background.Value = value;}
		}
		
		public Color BorderColor { 
			get { return borderColor.Value;}
			set { borderColor.Value = value;}
		}
		
		public double BorderThickness { 
			get { return borderThickness.Value;}
			set { borderThickness.Value = value;}
		}
		
		public Border ()
		{
			padding = BuildProperty<Thickness> ("Padding");
			background = BuildProperty<Brush> ("Background");
			borderColor = BuildProperty<Color> ("BorderColor");
			borderThickness = BuildProperty<double> ("BorderThickness");
					
			BorderColor = Colors.Black;
			BorderThickness = 1;
			Padding = new Thickness (5);
		}

		protected override Size MeasureOverride (Size availableSize)
		{
			if (Child == null)
				return new Size (0, 0);
			
			Child.Measure (availableSize);
			
			var width = Child.DesiredSize.Width + Padding.Left + Padding.Right + BorderThickness;
			var height = Child.DesiredSize.Height + Padding.Top + Padding.Bottom + BorderThickness;
			
			return new Size (width, height);
		}

		protected override void ArrangeOverride (Size finalSize)
		{
			if (Child == null)
				return;

			Child.Arrange (new Rect (Padding.Left + BorderThickness / 2, 
			                         Padding.Top + BorderThickness / 2,
			                         finalSize.Width - Padding.Left - Padding.Right - BorderThickness / 2, 
			                         finalSize.Height - Padding.Top - Padding.Bottom - BorderThickness / 2));
		}

		protected override void OnRender (DrawingContext dc)
		{		
			if (Child == null)
				return;

			dc.DrawRectangle (Background, new Pen (BorderColor, BorderThickness), new Rect (0, 0, Width, Height));
		}
	}
}

