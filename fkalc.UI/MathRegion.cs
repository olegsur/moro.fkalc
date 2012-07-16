// 
// MathRegion.cs
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
using fkalc.ViewModels.MathRegion.Tokens;

namespace fkalc.UI
{
	public class MathRegion : UserControl
	{
		private Border border;

		private HBoxToken root = new HBoxToken ();
		public Selection Selection { get; private set; }

		public HBoxToken Root {
			get {
				return root;
			}
		}
		
		public MathRegion ()
		{
			Focusable = true;

			var activeToken = new TextToken ();
			var rootVisual = new HBoxArea ();
			rootVisual.DataContext = root;

			root.Add (activeToken);

			border = new Border ()
			{
				Child = rootVisual,
				BorderColor = Colors.Bisque
			};	
									
			Content = new AdornerDecorator () {Child = border };

			AdornerLayer.GetAdornerLayer (rootVisual).Add (new CursorLines (this));
			
			MouseEnterEvent += HandleMouseEnterEvent;
			MouseLeaveEvent += HandleMouseLeaveEvent;
			
			new InsertCharacterProcessor (this);
			new DivideProcessor (this);
			new PlusProcessor (this);
			new MinusProcessor (this);
			new MultiplicationProcessor (this);
			new ResultProcessor (this);
			new LeftProcessor (this);
			new RightProcessor (this);
			new AssignmentProcessor (this);

			Selection = new Selection ();
			Selection.SelectedToken = activeToken;
		}

		private void HandleMouseEnterEvent (object sender, EventArgs e)
		{
			border.BorderColor = Colors.Red;
			
			Screen.QueueDraw ();
		}
		
		private void HandleMouseLeaveEvent (object sender, EventArgs e)
		{
			border.BorderColor = Colors.Bisque;
			
			Screen.QueueDraw ();
		}	
	}
}