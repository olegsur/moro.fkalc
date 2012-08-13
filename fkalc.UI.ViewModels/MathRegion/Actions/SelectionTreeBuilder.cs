//
// SelectionTreeBuilder.cs
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
using System.Linq;
using fkalc.UI.ViewModels.MathRegion.Tokens;
using fkalc.UI.ViewModels.MathRegion;

namespace fkalc.UI.ViewModels.MathRegion.Actions
{
	public class SelectionTreeBuilder
	{
		private SelectionType Type { get; set; }

		public SelectionTreeBuilder (SelectionType type)
		{
			Type = type;
		}

		public IEnumerable<SelectionToken> Build (Token token)
		{
			if (token is HBoxToken) {
				var hbox = token as HBoxToken;

				foreach (var child in hbox.Tokens.Where(c => !IsOperator(c))) {
					foreach (var node in Build(child))
						yield return node;
				}
			}

			if (token is FractionToken) {
				var fraction = token as FractionToken;

				yield return new SelectionToken (SelectionType.Left, fraction);

				foreach (var node in Build(fraction.Dividend))
					yield return node;
				foreach (var node in Build(fraction.Divisor))
					yield return node;

				yield return new SelectionToken (SelectionType.Right, fraction);
			}

			if (token is TextToken)
				yield return new SelectionToken (Type, token);

			if (token is ParenthesesToken) {
				var parentheses = token as ParenthesesToken;

				yield return new SelectionToken (SelectionType.Left, parentheses);

				foreach (var node in Build(parentheses.Child))
					yield return node;

				yield return new SelectionToken (SelectionType.Right, parentheses);
			}

			if (token is ExponentiationToken) {
				var exponentiation = token as ExponentiationToken;

				yield return new SelectionToken (SelectionType.Left, exponentiation);

				foreach (var node in Build(exponentiation.Base))
					yield return node;
				foreach (var node in Build(exponentiation.Power))
					yield return node;

				yield return new SelectionToken (SelectionType.Right, exponentiation);
			}

			if (token is SquareRootToken) {
				var squareRoot = token as SquareRootToken;

				yield return new SelectionToken (SelectionType.Left, squareRoot);

				foreach (var node in Build(squareRoot.Child))
					yield return node;

				yield return new SelectionToken (SelectionType.Right, squareRoot);
			}

			if (token is AbsoluteToken) {
				var absolute = token as AbsoluteToken;

				yield return new SelectionToken (SelectionType.Left, absolute);

				foreach (var node in Build(absolute.Child))
					yield return node;

				yield return new SelectionToken (SelectionType.Right, absolute);
			}
		}

		private bool IsOperator (Token token)
		{
			return token is PlusToken || token is MinusToken || token is MultiplicationToken;
		}

		public class SelectionToken
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

