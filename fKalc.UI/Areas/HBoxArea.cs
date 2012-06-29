// 
// HBoxArea.cs
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
using GMathCad.UI.Framework;
using System.Linq;
using System.Collections.Generic;

namespace GMathCad.UI
{
	public class HBoxArea : Area
	{
		private HStackPanel panel = new HStackPanel ();  
		private ItemsControl itemsControl;

		public HBoxArea ()
		{
			itemsControl = new ItemsControl ()
			{
				ItemsPanel = panel
			};

			Content = itemsControl;

			itemsControl.ItemTemplate = new DataTemplate (Factory);

			GetProperty ("DataContext").DependencyPropertyValueChanged += DataContextChanged;
		}

		private void DataContextChanged (object sender, DPropertyValueChangedEventArgs e)
		{
			if (e.NewValue is DependencyObject == false)
				return;

			var source = e.NewValue as DependencyObject;

			BindingOperations.SetBinding (source.GetProperty ("Tokens"), itemsControl.GetProperty ("ItemsSource"));
		}

		private UIElement Factory (object token)
		{
			return new TokenAreaConverter ().Convert (token) as UIElement;
		}

		private class HStackPanel : StackPanel
		{
			public HStackPanel ()
			{
				Orientation = Orientation.Horizontal;
			}
			
			protected override Size MeasureOverride (Size availableSize)
			{
				foreach (var child in InternalChildren.Where(c => c.Visibility != Visibility.Collapsed)) {
					child.Measure (availableSize);
				}
				
				var width = InternalChildren.Any () ? InternalChildren.Sum (c => c.DesiredSize.Width + c.Margin.Left + c.Margin.Right) : 0;
				var heightTop = InternalChildren.Any () ? InternalChildren.Max (c => BaseLineCalculator.GetDesiredBaseLine (GetArea (c)) + c.Margin.Top) : 0;			
				var heightBottom = InternalChildren.Any () ? InternalChildren.Max (c => c.DesiredSize.Height - BaseLineCalculator.GetDesiredBaseLine (GetArea (c)) + c.Margin.Bottom) : 0; 
						
				return new Size (width, heightTop + heightBottom);
			}
			
			protected override void ArrangeOverride (Size finalSize)
			{
				base.ArrangeOverride (finalSize);

				if (!InternalChildren.Where (c => c.Visibility != Visibility.Collapsed).Any ())
					return;
				
				var baseLine = InternalChildren.Where (c => c.Visibility != Visibility.Collapsed)
					.Max (c => BaseLineCalculator.GetDesiredBaseLine (GetArea (c)) + c.Margin.Top);
								
				foreach (var child in InternalChildren.Where(c => c.Visibility != Visibility.Collapsed)) {
					child.Y = baseLine - BaseLineCalculator.GetDesiredBaseLine (GetArea (child));

					//rearrange child
					child.Arrange (new Rect (new Point (child.X, child.Y), new Size (child.Width, child.Height)));										
				}	
			}

			private Area GetArea (UIElement uielement)
			{
				return AreaHelper.GetArea (uielement);
			}
		}
	}
}