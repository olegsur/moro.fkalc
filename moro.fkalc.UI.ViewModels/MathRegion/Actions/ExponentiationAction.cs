//
// ExponentiationAction.cs
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
	public class ExponentiationAction: IAction
	{
		private MathRegionViewModel Region { get; set; }
		
		public ExponentiationAction (MathRegionViewModel region)
		{
			Region = region;
		}
		
		public void Do ()
		{
			Region.SetNeedToEvaluate (true);

			var parent = Region.Selection.SelectedToken.Parent;

			Token _base;
			Token power;

			if (Region.Selection.SelectedToken is TextToken) {
				var textToken = Region.Selection.SelectedToken as TextToken;

				_base = new TextToken ()
				{
					Text = GetLeftString (textToken.Text)
				};

				power = new TextToken ()
				{
					Text = GetRightString (textToken.Text)
				};
			} else {
				if (Region.Selection.SelectedToken is FractionToken) {
					var fraction = Region.Selection.SelectedToken;

					var parentheses = new ParenthesesToken ();
					parentheses.ShowCloseParentheses = true;

					parent.Replace (fraction, parentheses);

					var container = new HBoxToken ();
					parentheses.Child = container;

					container.Add (fraction);

					_base = parentheses;
					Region.Selection.SelectedToken = parentheses;
				} else
					_base = Region.Selection.SelectedToken;

				power = new TextToken ();
			}

			var exponentiation = new ExponentiationToken ();			

			parent.Replace (Region.Selection.SelectedToken, exponentiation);
			exponentiation.Base = _base;
			exponentiation.Power = power;
				
			Region.Selection.SelectedToken = power;
		}

		private string GetLeftString (string text)
		{
			if (string.IsNullOrEmpty (text))
				return null;

			return text.Substring (0, Region.Selection.Position);
		}

		private string GetRightString (string text)
		{
			if (string.IsNullOrEmpty (text))
				return null;

			return text.Substring (Region.Selection.Position, text.Length - Region.Selection.Position);
		}
	}
}

	