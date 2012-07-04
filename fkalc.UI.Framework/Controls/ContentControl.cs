// 
// ContentControl.cs
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
	public class ContentControl : Control
	{
		private readonly DependencyProperty<UIElement> content;
		public UIElement Content { 
			get { return content.Value;} 
			set { content.Value = value; }
		}
		
		public ContentControl ()
		{		
			content = BuildProperty<UIElement> ("Content");
			content.DependencyPropertyValueChanged += HandleContentChanged;
		}

		private void HandleContentChanged (object sender, DPropertyValueChangedEventArgs<UIElement> e)
		{				
			if (e.OldValue != null)
				RemoveVisualChild (e.OldValue);
				
			if (e.NewValue != null)
				AddVisualChild (e.NewValue);
		}		

		
		protected override Size MeasureOverride (Size availableSize)
		{
			if (Content == null || Content.Visibility == Visibility.Collapsed)
				return new Size (0, 0);
			
			Content.Measure (availableSize);
			
			return Content.DesiredSize;
		}
		
		protected override void ArrangeOverride (Size finalSize)
		{
			if (Content == null || Content.Visibility == Visibility.Collapsed)
				return;
			
			Content.Arrange (new Rect (new Size (Width, Height)));
		}
		
		public override int VisualChildrenCount {
			get {
				return Content == null ? 0 : 1;
			}
		}

		public override Visual GetVisualChild (int index)
		{
			return Content;
		}
		
		public override Visual HitTest (double x, double y)
		{
			if (Content == null || !Content.IsVisible)
				return null;
			
			return Content.HitTest (x, y);
		}
	}
}