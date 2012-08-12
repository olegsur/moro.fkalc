//
// Grid.cs
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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace fkalc.UI.Framework
{
	public class Grid : Panel
	{
		private List<GridChild> children = new List<GridChild> ();

		public Grid ()
		{
			Children.CollectionChanged += HandleCollectionChanged;
		}

		public void SetRow (uint row, UIElement element)
		{
			var child = children.FirstOrDefault (c => c.Content == element);
			
			if (child == null)
				return;
			
			child.Row = row;
		}

		public void SetColumn (uint column, UIElement element)
		{
			var child = children.FirstOrDefault (c => c.Content == element);
			
			if (child == null)
				return;
			
			child.Column = column;
		}

		private void HandleCollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add) {
				var index = e.NewStartingIndex;

				foreach (var uielement in e.NewItems.Cast<UIElement>()) {
					var child = new GridChild (uielement);
			
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
					var child = new GridChild (uielement);
			
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
			var col = children.Where (c => c.Visibility != Visibility.Collapsed);

			foreach (var child in children.Where(c => c.Visibility != Visibility.Collapsed)) {
				child.Measure (availableSize);
			}

			var height = col.Any () ? col.GroupBy (c => c.Row).Sum (row => row.Max (c => c.DesiredSize.Height + c.Margin.Top + c.Margin.Bottom)) : 0;
			var width = col.Any () ? col.GroupBy (c => c.Column).Sum (column => column.Max (c => c.DesiredSize.Width + c.Margin.Left + c.Margin.Right)) : 0;
			
			return new Size (width, height);
		}

		protected override void ArrangeOverride (Size finalSize)
		{
			var x = 0d;
			var y = 0d;

			var col = children.Where (c => c.Visibility != Visibility.Collapsed);

			var heights = col.GroupBy (c => c.Row).OrderBy (g => g.Key)
				.ToDictionary (row => row.Key, row => row.Max (c => c.DesiredSize.Height + c.Margin.Top + c.Margin.Bottom)); 

			var widths = col.GroupBy (c => c.Column).OrderBy (g => g.Key)
				.ToDictionary (column => column.Key, column => column.Max (c => c.DesiredSize.Width + c.Margin.Left + c.Margin.Right)); 

			foreach (var height in heights) {
				x = 0;
				foreach (var width in widths) {
					var child = col.FirstOrDefault (c => c.Row == height.Key && c.Column == width.Key);

					if (child != null)
						child.Arrange (new Rect (new Point (x + child.Margin.Left, y + child.Margin.Top), child.DesiredSize));

					x += width.Value;
				}
				y += height.Value;
			}
		}

		public override Visual GetVisualChild (int index)
		{
			return children [index];
		}
	}
}

