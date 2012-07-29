//
// ParenthesesArea.cs
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
using fkalc.UI.Framework;

namespace fkalc.UI
{
	public class ParenthesesArea : Area
	{
		public ContentControl Child { get; private set; }

		public ParenthesesArea ()
		{
			Child = new ContentControl ()
			{
				Content = new TextArea ()
			};

			var stackPanel = new StackPanel ()
			{
				Orientation = Orientation.Horizontal
			};

			stackPanel.Children.Add (OpenParentheses ());
			stackPanel.Children.Add (Child);
			stackPanel.Children.Add (CloseParentheses ());

			Content = stackPanel;

			BindingOperations.SetBinding (this, "DataContext.Child", Child.GetProperty ("Content"), new TokenAreaConverter ());
		}

		private Canvas OpenParentheses ()
		{
			var canvas = new Canvas ();
			
			var line1 = new Line ()
			{
				WidthRequest = 7,
				HeightRequest = 2,
				StrokeThickness = 2,
				Stroke = Colors.Black,
				SnapsToDevicePixels = true
			};

			var line2 = new Line ()
			{
				WidthRequest = 2,
				HeightRequest = 20,
				StrokeThickness = 2,
				Stroke = Colors.Black,
				SnapsToDevicePixels = true
			};

			var line3 = new Line ()
			{
				WidthRequest = 7,
				HeightRequest = 2,
				StrokeThickness = 2,
				Stroke = Colors.Black,
				SnapsToDevicePixels = true
			};
						
			canvas.Children.Add (line1);			
			canvas.Children.Add (line2);
			canvas.Children.Add (line3);
			canvas.SetTop (1, line1);
			canvas.SetTop (1, line2);
			canvas.SetLeft (1, line2);
			canvas.SetTop (19, line3);

			canvas.HeightRequest = 20;
			canvas.WidthRequest = 7;

			return canvas;
		}

		private Canvas CloseParentheses ()
		{
			var canvas = new Canvas ();
			
			var line1 = new Line ()
			{
				WidthRequest = 7,
				HeightRequest = 2,
				StrokeThickness = 2,
				Stroke = Colors.Black,
				SnapsToDevicePixels = true
			};

			var line2 = new Line ()
			{
				WidthRequest = 2,
				HeightRequest = 20,
				StrokeThickness = 2,
				Stroke = Colors.Black,
				SnapsToDevicePixels = true
			};

			var line3 = new Line ()
			{
				WidthRequest = 7,
				HeightRequest = 2,
				StrokeThickness = 2,
				Stroke = Colors.Black,
				SnapsToDevicePixels = true
			};
						
			canvas.Children.Add (line1);			
			canvas.Children.Add (line2);
			canvas.Children.Add (line3);
			canvas.SetTop (1, line1);
			canvas.SetTop (1, line2);
			canvas.SetLeft (4, line2);
			canvas.SetTop (19, line3);

			canvas.HeightRequest = 20;
			canvas.WidthRequest = 7;

			BindingOperations.SetBinding (this, "DataContext.ShowCloseParentheses", canvas.GetProperty ("Visibility"), new BooleanToVisibilityConverter ());

			return canvas;
		}
	}
}

