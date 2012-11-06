//
// MainWindow.cs
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
using moro.fkalc.UI.ViewModels;

namespace moro.fkalc.UI
{
	public class MainWindow : Window
	{
		public MainWindow ()
		{
			WidthRequest = 997;
			HeightRequest = 679;
			Title = "fkalc";

			var menuItems = new ObservableCollection<string> () { "File", "Edit", "View", "Help" };

			var menu = new Menu ()
			{
				HorizontalAlignment = HorizontalAlignment.Stretch,
				VerticalAlignment = VerticalAlignment.Stretch,
				ItemsSource = menuItems,
			};

			var document = new DocumentView ()
			{
				DataContext = new DocumentViewModel ()
			};

			var grid = new Grid ()
			{
				HorizontalAlignment = HorizontalAlignment.Stretch,
				VerticalAlignment = VerticalAlignment.Stretch
			};

			grid.RowDefinitions.Add (new RowDefinition () { Height = GridLength.Auto });
			grid.RowDefinitions.Add (new RowDefinition ());
			grid.ColumnDefinitions.Add (new ColumnDefinition ());

			grid.Children.Add (menu);
			grid.Children.Add (document);		

			grid.SetRow (0, menu);
			grid.SetColumn (0, menu);

			grid.SetRow (1, document);
			grid.SetColumn (0, document);

			Content = grid;
		}
	}
}

