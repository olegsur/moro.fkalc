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
using System.Collections.Specialized;

namespace fKalc.UI.Framework
{
	public class StackPanel : Panel
	{	
		private List<StackPanelChild> children = new List<StackPanelChild> ();
		protected IEnumerable<StackPanelChild> InternalChildren { get { return children; } }
		
		public Orientation Orientation { get; set; }
		
		public StackPanel ()
		{
			base.Children.CollectionChanged += HandleCollectionChanged;

			Orientation = Orientation.Vertical;
		}		

		public Thickness GetMargin (UIElement element)
		{
			return children.First (c => c.Content == element).Margin; 		
		}

		public void SetMargin (Thickness margin, UIElement element)
		{
			children.First (c => c.Content == element).Margin = margin;
		}

		public HorizontalAlignment GetHorizontalAlignment (UIElement element)
		{
			return children.First (c => c.Content == element).HorizontalAlignment;
		}

		public void SetHorizontalAlignment (HorizontalAlignment alignment, UIElement element)
		{
			children.First (c => c.Content == element).HorizontalAlignment = alignment;
		}

		private void HandleCollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add) {
				var index = e.NewStartingIndex;

				foreach (var uielement in e.NewItems.Cast<UIElement>()) {
					var child = new StackPanelChild (uielement);
			
					AddVisualChild (child);
					children.Insert (index, child);

					index++;
				}
			}

			if (e.Action == NotifyCollectionChangedAction.Remove) {
				var index = e.OldStartingIndex;
				foreach (var uielement in e.OldItems.Cast<UIElement>()) {
					var child = children [index];
			
					RemoveVisualChild (child);			
					children.Remove (child);

					index++;
				}
			}

			if (e.Action == NotifyCollectionChangedAction.Replace) {
				var index = e.NewStartingIndex;

				foreach (var uielement in e.NewItems.Cast<UIElement>()) {
					var child = new StackPanelChild (uielement);
			
					RemoveVisualChild (children [index]);

					AddVisualChild (child);
					children [index] = child;

					index++;
				}
			}

			if (e.Action == NotifyCollectionChangedAction.Reset) {
				children.Clear ();
			}
		}
						
		protected override Size MeasureOverride (Size availableSize)
		{
			foreach (var child in children.Where(c => c.Visibility != Visibility.Collapsed)) {
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

				if (Orientation == Orientation.Horizontal) {
					x += child.Margin.Left;	
					y = (Height - height) / 2;
				} else {
					x = (Width - width) / 2;
					y += child.Margin.Top;
				}

				child.Arrange (new Rect (new Point (x, y), new Size (width, height)));
				
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
				dc.PushTransform (child.VisualTransform);
				
				child.Render (dc);
				
				dc.Pop ();
			}
		}
	}
}