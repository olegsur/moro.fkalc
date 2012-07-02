//
// RightAction.cs
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
using System.Linq;
using fkalc.Tokens;
using System.Collections.Generic;

namespace fkalc.UI
{
	public class RightAction
	{
		private MathRegion Region { get; set; }

		public RightAction (MathRegion region)
		{
			Region = region;
		}
		
		public void Execute ()
		{
			if (Region.Selection.SelectedToken is TextToken) {
				var textToken = Region.Selection.SelectedToken as TextToken;
				var lenght = string.IsNullOrEmpty (textToken.Text) ? 0 : textToken.Text.Length;

				if (Region.Selection.Position < lenght) {
					Region.Selection.Position++;
					return;
				}

				Region.Selection.Type = SelectionType.Left;
			}		

			var next = GetSelectionTree (Region.Root)
				.SkipWhile (t => !(t.Token == Region.Selection.SelectedToken && t.Type == Region.Selection.Type))
					.Skip (1).FirstOrDefault ();

			if (next != null) {
				Region.Selection.SelectedToken = next.Token;
				Region.Selection.Type = next.Type;
			}
		}

		private IEnumerable<SelectionToken> GetSelectionTree (Token token)
		{
			if (token is HBoxToken) {
				var hbox = token as HBoxToken;

				foreach (var child in hbox.Tokens.Where(c => !IsOperator(c))) {
					foreach (var node in GetSelectionTree(child))
						yield return node;
				}
			}

			if (token is FractionToken) {
				var fraction = token as FractionToken;

				yield return new SelectionToken (SelectionType.Left, fraction);

				foreach (var node in GetSelectionTree(fraction.Dividend))
					yield return node;
				foreach (var node in GetSelectionTree(fraction.Divisor))
					yield return node;

				yield return new SelectionToken (SelectionType.Right, fraction);
			}

			if (token is TextToken)
				yield return new SelectionToken (SelectionType.Left, token);

		}

		private bool IsOperator (Token token)
		{
			return token is PlusToken || token is MinusToken || token is MultiplicationToken;
		}

		private class SelectionToken
		{
			public SelectionType Type { get; set; }
			public Token Token { get; set; }

			public SelectionToken (SelectionType type, Token token)
			{
				Type = type;
				Token = token;
			}
		}

	}
}

