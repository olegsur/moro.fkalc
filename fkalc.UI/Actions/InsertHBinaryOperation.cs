//
// InsertHBinaryOperation.cs
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
using fkalc.Tokens;

namespace fkalc.UI
{
	public class InsertHBinaryOperation
	{
		private MathRegion Region { get; set; }
		private Token Operation { get; set; }

		public InsertHBinaryOperation (MathRegion region, Token operation)
		{
			Region = region;
			Operation = operation;
		}

		public void Execute ()
		{
			var right = new TextToken ();

			if (Region.Selection.SelectedToken is TextToken) {
				var textToken = Region.Selection.SelectedToken as TextToken;

				var parent = GetHBoxToken (textToken);

				var left = new TextToken ()
				{
					Text =  GetLeftString (textToken.Text)
				};

				right.Text = GetRightString (textToken.Text);

				var index = parent.Tokens.IndexOf (textToken);

				parent.Tokens [index] = left;
				parent.Tokens.Insert (index + 1, Operation);	
				parent.Tokens.Insert (index + 2, right);
				 
			} else {
				var parent = GetHBoxToken (Region.Selection.SelectedToken);

				var index = parent.Tokens.IndexOf (Region.Selection.SelectedToken);

				if (Region.Selection.Type == SelectionType.Left) {
					parent.Tokens.Insert (index, Operation);	
					parent.Tokens.Insert (index, right);
				} else {
					parent.Tokens.Insert (index + 1, Operation);	
					parent.Tokens.Insert (index + 2, right);
				}
			}
				
			Region.Selection.SelectedToken = right;
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

