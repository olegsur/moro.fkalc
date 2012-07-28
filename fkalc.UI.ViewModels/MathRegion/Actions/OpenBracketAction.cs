//
// OpenBracketAction.cs
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
using fkalc.UI.ViewModels.MathRegion.Tokens;
using System.Text.RegularExpressions;

namespace fkalc.UI.ViewModels.MathRegion.Actions
{
	public class OpenBracketAction : IAction
	{
		private MathRegionViewModel Region { get; set; }

		public OpenBracketAction (MathRegionViewModel region)
		{
			Region = region;
		}

		public void Do ()
		{
			Region.SetNeedToEvaluate (true);
									
			if (Region.Selection.SelectedToken is TextToken == false) {
				var operation = new MultiplicationToken ();			
				
				new InsertHBinaryOperation (Region, operation).Do ();
			}

			var textToken = Region.Selection.SelectedToken as TextToken;

			if (!string.IsNullOrEmpty (textToken.Text) && Regex.IsMatch (textToken.Text, @"\d+") && Region.Selection.Position != 0) {
				var operation = new MultiplicationToken ();			
				
				new InsertHBinaryOperation (Region, operation).Do ();
			}

			textToken = Region.Selection.SelectedToken as TextToken;

			var parent = GetHBoxToken (textToken);

			var index = parent.Tokens.IndexOf (textToken);

			parent.Tokens [index] = new OpenBracketToken ();
			parent.Tokens.Insert (index + 1, textToken);

			Region.Selection.SelectedToken = textToken;
		}

		private HBoxToken GetHBoxToken (Token token)
		{
			if (token.Parent is HBoxToken == false) {
				var parent = token.Parent;
				var container = new HBoxToken ();

				parent.Replace (token, container);
				container.Add (token);
			}

			return token.Parent as HBoxToken;
		}
	}
}

