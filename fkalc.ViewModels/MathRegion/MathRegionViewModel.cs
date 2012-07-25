//
// MathRegionViewModel.cs
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
using fkalc.ViewModels.MathRegion.Tokens;
using fkalc.UI.Framework;
using fkalc.ViewModels.MathRegion.Actions;

namespace fkalc.ViewModels.MathRegion
{
	public class MathRegionViewModel : DependencyObject
	{	
		private readonly DependencyProperty<double> x;
		private readonly DependencyProperty<double> y;
		private readonly DependencyProperty<HBoxToken> root;
		private readonly DependencyProperty<Selection> selection;

		private readonly DependencyProperty<ICommand> resultCommand;

		public double X { 
			get { return x.Value; }
			set { x.Value = value; }
		}

		public double Y { 
			get { return y.Value; }
			set { y.Value = value; }
		}

		public HBoxToken Root { 
			get { return root.Value; }
			private set { root.Value = value; } 
		}

		public Selection Selection { 
			get { return selection.Value; }
			private set { selection.Value = value; }
		}

		public MathRegionViewModel ()
		{
			x = BuildProperty<double> ("X");
			y = BuildProperty<double> ("Y");
			root = BuildProperty<HBoxToken> ("Root");
			selection = BuildProperty<Selection> ("Selection");

			resultCommand = BuildProperty<ICommand> ("ResultCommand");
			resultCommand.Value = new DelegateCommand (() => new ResultAction (this).Do ());

			Root = new HBoxToken ();
			Selection = new Selection ();

			var token = new TextToken ();
			Root.Add (token);

			Selection.SelectedToken = token;
		

			                                     
		}

	}
}