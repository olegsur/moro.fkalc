//
// CommaAction.cs
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
using moro.fkalc.UI.ViewModels.MathRegion.Tokens;

namespace moro.fkalc.UI.ViewModels.MathRegion.Actions
{
	public class CommaAction: IAction
	{
		private MathRegionViewModel Region { get; set; }

		public CommaAction (MathRegionViewModel region)
		{
			Region = region;
		}

		public void Do ()
		{
			var selectedToken = Region.Selection.SelectedToken;

			if (selectedToken.Parent == null || selectedToken.Parent is HBoxToken == false || selectedToken.Parent.Parent is ParenthesesToken == false)
				return;

			//var parentheses = selectedToken.Parent.Parent as ParenthesesToken;

			Region.SetNeedToEvaluate (true);

			var operation = new CommaToken ();
			new InsertHBinaryOperation (Region, operation).Do ();
		}
	}
}

