// 
// DocumentView.cs
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
using moro.Framework;
using moro.fkalc.UI.ViewModels.MathRegion;
using moro.fkalc.UI.ViewModels;
using moro.Framework.Data;

namespace moro.fkalc.UI
{
	public class DocumentView : UserControl
	{
		private readonly DependencyProperty<ICommand> newRegionCommand;

		public ICommand NewRegionCommand { get { return newRegionCommand.Value; } }

		private DocumentCursor documentCursor = new DocumentCursor ();
		private Canvas canvas = new Canvas ();
		private ItemsControl itemsControl = new ItemsControl () {HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch};
		
		public DocumentView ()
		{	
			HorizontalAlignment = HorizontalAlignment.Stretch;
			VerticalAlignment = VerticalAlignment.Stretch;

			newRegionCommand = BuildProperty<ICommand> ("NewRegionCommand");

			Focusable = true;

			itemsControl.ItemsPanel = canvas;
			itemsControl.ItemTemplate = new DataTemplate (Factory);
			
			var adornerDecorator = new AdornerDecorator () { Child = itemsControl };
			
			var grid = new Grid () {HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch};
			grid.RowDefinitions.Add (new RowDefinition ());
			grid.RowDefinitions.Add (new RowDefinition () {Height = GridLength.Auto});
			grid.ColumnDefinitions.Add (new ColumnDefinition ());
			grid.ColumnDefinitions.Add (new ColumnDefinition () {Width = GridLength.Auto});			
			
			var textBlock = new TextBlock (){Text ="+"};
						
			var button = new Button () { Content = textBlock};			
			
			grid.Children.Add (adornerDecorator);
			grid.Children.Add (button);
			
			grid.SetRow (1, button);			
			
			Content = grid;

			PreviewKeyPressEvent += HandlePreviewKeyPressEvent;
			ButtonPressEvent += HandleButtonPressEvent;

			AdornerLayer.GetAdornerLayer (itemsControl).Add (documentCursor);
			
			Background = Brushes.White;

			Keyboard.Focus (this);

			BindingOperations.SetBinding (this, "DataContext.DocumentCursor", documentCursor.GetProperty ("DataContext"));
			BindingOperations.SetBinding (this, "DataContext.Regions", itemsControl.GetProperty ("ItemsSource"));
			BindingOperations.SetBinding (this, "DataContext.NewRegionCommand", GetProperty ("NewRegionCommand"));
		}		

		private void HandleButtonPressEvent (object o, MouseButtonEventArgs args)
		{		
			if (Mouse.Device.TargetElement != canvas) {
				documentCursor.Visibility = Visibility.Collapsed;
				return;
			}

			var point = Mouse.GetPosition (this);
			documentCursor.X = Math.Round (point.X);
			documentCursor.Y = Math.Round (point.Y);
									
			documentCursor.Visibility = Visibility.Visible;
		}
		
		private void HandlePreviewKeyPressEvent (object o, KeyEventArgs args)
		{
			ProcessKey ();
		}
		
		private void ProcessKey ()
		{
			if (Keyboard.FocusedElement == this) {
				documentCursor.Visibility = Visibility.Collapsed;

				if (NewRegionCommand != null)
					NewRegionCommand.Execute (null);
			}		    
		}

		private UIElement Factory (object viewModel)
		{
			var region = new MathRegion ();
			region.DataContext = viewModel;

			BindingOperations.SetBinding ((viewModel as DependencyObject).GetProperty ("X"), canvas, region, "X");
			BindingOperations.SetBinding ((viewModel as DependencyObject).GetProperty ("Y"), canvas, region, "Y");

			Keyboard.Focus (region);

			return region;
		}
	}
}

