// 
// Decorator.cs
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
	public class Decorator : FrameworkElement
	{
		private UIElement child;		
		
		public Decorator ()
		{
		}
		
		public UIElement Child { 
			get { return child; }
			set {
				if (child == value)
					return;
				
				if (child != null)
					RemoveVisualChild (child);
				
				if (value != null)
					AddVisualChild (value);
				
				child = value;					
			}
		}
		
		protected override Size MeasureOverride (Size availableSize)
		{
			if (Child == null)
				return new Size (0, 0);
			
			Child.Measure (availableSize);			
					
			return Child.DesiredSize;
		}
		
		protected override void ArrangeOverride (Size finalSize)
		{
			if (Child == null)
				return;
				
			Child.Arrange (new Rect (Child.DesiredSize));
		}
		
		protected override void OnRender (DrawingContext dc)
		{
			if (Child == null)
				return;
			
			Child.Render (dc);
		}

		public override int VisualChildrenCount {
			get {
				return Child == null ? 0 : 1;
			}
		}

		public override Visual GetVisualChild (int index)
		{
			return Child;
		}
	}
}

