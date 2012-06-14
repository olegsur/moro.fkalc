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

namespace GMathCad.UI.Framework
{
	public class ContentControl : FrameworkElement
	{
		private UIElement content;		
		
		public ContentControl ()
		{			
		}
		
		public UIElement Content 
		{ 
			get { return content; }
			set
			{
				if (content == value) return;
				
				if (content != null) RemoveVisualChild (content);
				
				if (value != null) AddVisualChild (value);
				
				content = value;					
			}
		}
		
		protected override Size MeasureOverride (Size availableSize, Cairo.Context cr)
		{
			if (Content == null) return Size.Empty;
			
			Content.Measure (availableSize, cr);
			
			return Content.DesiredSize;
		}
		
		protected override void ArrangeOverride (Size finalSize)
		{
			if (Content == null)
				return;
			
			Content.Arrange (!Content.DesiredSize.IsEmpty ? new Size (Width, Height) : finalSize);
		}
		
		protected override void OnRender (Cairo.Context cr)
		{
			if (Content == null) return;
			
			Content.Render (cr);
		}
		
		public override Visual HitTest (double x, double y)
		{
			if (Content == null) return null;
			
			return Content.HitTest (x, y);
		}
	}
}