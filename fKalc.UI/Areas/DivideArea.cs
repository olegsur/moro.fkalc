// 
// DivideArea.cs
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
using fKalc.UI.Framework;
using System.Linq;

namespace fKalc.UI
{
	public class DivideArea : Area
	{
		private StackPanel panel = new StackPanel () { Orientation = Orientation.Vertical };
		
		public ContentControl Dividend { get; private set; }
		public ContentControl Divisor { get; private set; }
		
		public DivideArea ()
		{
			Content = panel;		
			
			Dividend = new ContentControl () {Content = new TextArea()};
			Divisor = new ContentControl () {Content = new TextArea()};
			
			var line = new Line ()
			{
				HeightRequest = 2,
				StrokeThickness = 2
			};
			
			panel.Children.Add (Dividend);
			panel.Children.Add (line);			
			panel.Children.Add (Divisor);
			
			panel.SetMargin (new Thickness (5, 0, 5, 5), Dividend);
			panel.SetMargin (new Thickness (5, 5, 5, 0), Divisor);
			
			panel.SetHorizontalAlignment (HorizontalAlignment.Stretch, line);

			GetProperty ("DataContext").DependencyPropertyValueChanged += DataContextChanged;
		}

		private void DataContextChanged (object sender, DPropertyValueChangedEventArgs e)
		{
			if (e.NewValue is DependencyObject == false)
				return;

			var source = e.NewValue as DependencyObject;

			BindingOperations.SetBinding (source.GetProperty ("Dividend"), Dividend.GetProperty ("Content"), new TokenAreaConverter ());
			BindingOperations.SetBinding (source.GetProperty ("Divisor"), Divisor.GetProperty ("Content"), new TokenAreaConverter ());
		}	
	}
}