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

			Margin = new Thickness (3, 0, 0, 0);
		}

		private UIElement OpenParentheses ()
		{
			var figure = new PathFigure ();
			figure.StartPoint = new Point (5, 1);


			figure.Segments.Add (new ArcSegment () { Point = new Point (5, 30), Size = new Size (14, 20), SweepDirection = SweepDirection.Counterclockwise });
				
			var geometry = new PathGeometry ();
			geometry.Figures.Add (figure);

			var path = new Path ();
			path.Data = geometry;
			path.StrokeThickness = 2;
			path.WidthRequest = 6;
			path.HeightRequest = 30;

			return path;
		}

		private UIElement CloseParentheses ()
		{
			var figure = new PathFigure ();
			figure.StartPoint = new Point (2, 1);

			figure.Segments.Add (new ArcSegment () { Point = new Point (2, 30), Size = new Size (14, 20), SweepDirection = SweepDirection.Clockwise });
				
			var geometry = new PathGeometry ();
			geometry.Figures.Add (figure);

			var path = new Path ();
			path.Data = geometry;
			path.StrokeThickness = 2;
			path.HeightRequest = 30;
			path.WidthRequest = 5;

			BindingOperations.SetBinding (this, "DataContext.ShowCloseParentheses", path.GetProperty ("Visibility"), new BooleanToVisibilityConverter ());

			return path;
		}
	}
}

