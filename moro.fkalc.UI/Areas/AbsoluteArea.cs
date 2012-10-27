//
// AbsoluteArea.cs
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
using moro.Framework;
using moro.Framework.Data;

namespace moro.fkalc.UI
{
	public class AbsoluteArea : Area
	{
		public ContentControl Child { get; private set; }

		public AbsoluteArea ()
		{
			Child = new ContentControl ()
			{
				Content = new TextArea (),
				VerticalAlignment = VerticalAlignment.Center,
				Margin = new Thickness(3)
			};

			var stackPanel = new StackPanel ()
			{
				Orientation = Orientation.Horizontal
			};

			stackPanel.Children.Add (new Line () { WidthRequest = 2, VerticalAlignment = VerticalAlignment.Stretch, StrokeThickness = 2, });
			stackPanel.Children.Add (Child);
			stackPanel.Children.Add (new Line () { WidthRequest = 2, VerticalAlignment = VerticalAlignment.Stretch, StrokeThickness = 2, });

			Content = stackPanel;

			BindingOperations.SetBinding (this, "DataContext.Child", Child.GetProperty ("Content"), new TokenAreaConverter ());	
		}
	}
}

