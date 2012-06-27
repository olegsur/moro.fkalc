//
// ItemsControl.cs
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
using System.Collections;
using System.Linq;
using System.Collections.Specialized;

namespace GMathCad.UI.Framework
{
	public class ItemsControl : Control
	{
		public StackPanel itemsPanel;

		private readonly DependencyProperty<IEnumerable> itemsSource;
		public IEnumerable ItemsSource { 
			get { return itemsSource.Value;} 
			set { itemsSource.Value = value; }
		}

		public ItemsControl ()
		{
			ItemsPanel = new StackPanel ();
			itemsSource = BuildProperty<IEnumerable> ("ItemsSource");

			itemsSource.DependencyPropertyValueChanged += ItemsSourceChanged;
		}

		public StackPanel ItemsPanel { 
			get { return itemsPanel; }
			set { 
				if (itemsPanel == value)
					return;

				if (value == null)
					throw new ArgumentNullException ();

				if (itemsPanel != null)
					RemoveVisualChild (itemsPanel);

				itemsPanel = value;

				AddVisualChild (itemsPanel);
			}
		}

		private void ItemsSourceChanged (object sender, DPropertyValueChangedEventArgs<IEnumerable> e)
		{
			itemsPanel.Children.Clear ();

			foreach (var o in e.NewValue) {
				var child = o is UIElement ? o as UIElement : new TextBlock () {Text = o.ToString ()};
				itemsPanel.Children.Add (child);
			}

			if (e.NewValue is INotifyCollectionChanged) {
				var collection = e.NewValue as INotifyCollectionChanged;

				collection.CollectionChanged += HandleCollectionChanged;
			}
		}

		private void HandleCollectionChanged (object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action) {
			case NotifyCollectionChangedAction.Add:
				var index = e.NewStartingIndex;
				foreach (var child in e.NewItems.OfType<UIElement>()) {
					itemsPanel.Children.Insert (index, child);
					index++;
				}
				break;
			case NotifyCollectionChangedAction.Remove:
				foreach (var child in e.OldItems.OfType<UIElement>()) {
					itemsPanel.Children.Remove (child);
				}
				break;
			case NotifyCollectionChangedAction.Reset:
				itemsPanel.Children.Clear ();
				break;
			default:
				break;
			}

		}

		protected override Size MeasureOverride (Size availableSize)
		{
			ItemsPanel.Measure (availableSize);
			return ItemsPanel.DesiredSize;
		}

		protected override void ArrangeOverride (Size finalSize)
		{
			ItemsPanel.Arrange (new Rect (finalSize));
		}

		protected override void OnRender (DrawingContext dc)
		{
			itemsPanel.Render (dc);
		}
	}
}

