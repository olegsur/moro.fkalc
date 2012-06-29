// 
// FrameworkElement.cs
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
	public class FrameworkElement : UIElement
	{
		public double Height { get; protected set; }
		public double Width { get; protected set; }	
		
		public double? HeightRequest { get; set; }
		public double? WidthRequest { get; set; }

		private readonly DependencyProperty<object> dataContext;

		public object DataContext { 
			get { return dataContext.Value;} 
			set { dataContext.Value = value; }
		}
		
		public FrameworkElement ()
		{	
			dataContext = BuildProperty<object> ("DataContext");
		}
		
		protected override Size MeasureCore (Size availableSize)
		{
			var size = MeasureOverride (availableSize);
			
			
			var height = HeightRequest ?? size.Height;
			var width = WidthRequest ?? size.Width;
			
			return new Size (width, height);
		}
		
		protected override void ArrangeCore (Rect finalRect)
		{	
			base.ArrangeCore (finalRect);

			Height = finalRect.Height;
			Width = finalRect.Width;
			
			ArrangeOverride (finalRect.Size);
		}
		
		protected virtual Size MeasureOverride (Size availableSize)
		{		
			return new Size (0, 0);
		}
		
		protected virtual void ArrangeOverride (Size finalSize)
		{	
		}
	}
}