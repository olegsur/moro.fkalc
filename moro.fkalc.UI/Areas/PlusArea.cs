// 
// PlusArea.cs
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

namespace moro.fkalc.UI
{
	public class PlusArea : Area
	{
		public PlusArea ()
		{
			HeightRequest = 12;
			WidthRequest = 12;
			
			var canvas = new Canvas ();
			
			var line1 = new Line ()
			{
				WidthRequest = 12,
				HeightRequest = 2,
				StrokeThickness = 2,
				Stroke = Colors.Black,
				SnapsToDevicePixels = true
			};
			
			var line2 = new Line ()
			{
				WidthRequest = 2,
				HeightRequest = 12,
				StrokeThickness = 2,
				Stroke = Colors.Black,
				SnapsToDevicePixels = true
			};
			
			canvas.Children.Add (line1);
			canvas.Children.Add (line2);
			
			canvas.SetTop (5, line1);
			canvas.SetLeft (5, line2);
			
			Content = canvas;
			Margin = new Thickness (2);
		}
	}
}

