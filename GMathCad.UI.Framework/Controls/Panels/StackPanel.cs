// 
// StackPanel.cs
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
using System.Linq;
using System.Collections.Generic;

namespace GMathCad.UI.Framework
{
	public class StackPanel : Panel<StackPanelChild>
	{	
		private List<StackPanelChild> children = new List<StackPanelChild> ();
		public override IEnumerable<StackPanelChild> Children { get { return children; } }
		
		public Orientation Orientation { get; set; }
		
		public StackPanel ()
		{
			Orientation = Orientation.Vertical;
		}		
		
		public override void AddChild (UIElement uielement)
		{
			var child = new StackPanelChild (uielement);
			
			AddVisualChild (child);			
			children.Add (child);
		}
		
		public override void RemoveChild (UIElement uielement)
		{
			var child = children.FirstOrDefault (c => c.Content == uielement);	
			
			if (child == null)
				return;
			
			RemoveVisualChild (child);			
			children.Remove (child);
		}
		
		public int GetPosition (UIElement uielement)
		{
			return children.FindIndex (c => c.Content == uielement);
		}
		
		public void SetPosition (int position, UIElement uielement)
		{
			var oldIndex = children.FindIndex (c => c.Content == uielement);
			
			if (oldIndex == -1)
				return;
			
			var child = children.ElementAt (oldIndex);
			children.RemoveAt (oldIndex);
			children.Insert (position, child);			
		}
		
		public void SetMargin (Thickness margin, UIElement element)
		{
			var el = children.FirstOrDefault (c => c.Content == element);
			
			if (el == null)
				return;
			
			el.Margin = margin;
		}
		
		public void SetHorizontalAlignment (HorizontalAlignment alignment, UIElement element)
		{
			var el = children.FirstOrDefault (c => c.Content == element);
			
			if (el == null)
				return;
			
			el.HorizontalAlignment = alignment;
		}
						
		protected override Size MeasureOverride (Size availableSize)
		{
			foreach (var child in Children.Where(c => c.Visibility != Visibility.Collapsed)) {
				child.Measure (availableSize);
			}
			
			var width = 0d;
			var height = 0d;			
						
			if (Orientation == Orientation.Horizontal) {
				width = children.Sum (c => c.DesiredSize.Width + c.Margin.Left + c.Margin.Right);
				height = children.Max (c => c.DesiredSize.Height + c.Margin.Top + c.Margin.Bottom);			
			} else {
				width = children.Max (c => c.DesiredSize.Width + c.Margin.Left + c.Margin.Right);
				height = children.Sum (c => c.DesiredSize.Height + c.Margin.Top + c.Margin.Bottom);			
			}
		
			return new Size (width, height);
		}
		
		protected override void ArrangeOverride (Size finalSize)
		{
			var x = 0d;
			var y = 0d;
			
			foreach (var child in children.Where(c => c.Visibility != Visibility.Collapsed)) {	
				var width = child.DesiredSize.Width;
				var height = child.DesiredSize.Height;
				
				if (Orientation == Orientation.Vertical && child.HorizontalAlignment == HorizontalAlignment.Stretch) {
					width = finalSize.Width;
				}
				
				child.Arrange (new Size (width, height));
				
				if (Orientation == Orientation.Horizontal) {
					x += child.Margin.Left;	
					y = (Height - child.Height) / 2;
				} else {
					x = (Width - child.Width) / 2;
					y += child.Margin.Top;
				}
				
				child.X = x;
				child.Y = y;
				
				if (Orientation == Orientation.Horizontal)				
					x += child.Width + child.Margin.Right;
				else
					y += child.Height + child.Margin.Bottom;
			}
		}
		
		protected override void OnRender (DrawingContext dc)
		{
			foreach (var child in children.Where(c => c.IsVisible)) {
				dc.Save ();
				
				dc.Translate (child.X, child.Y);
				
				child.Render (dc);
				
				dc.Restore ();
			}
		}
	}
}