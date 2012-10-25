//
// Scaner.cs
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
using moro.fkalc.UI.Common.MathRegion;
using moro.fkalc.UI.Common.MathRegion.Tokens;

namespace moro.fkalc.Core
{
	public class Scaner
	{
		private IEnumerable<IMathRegionViewModel> Regions { get; set; }

		public Scaner (IEnumerable<IMathRegionViewModel> regions)
		{
			Regions = regions;
		}

		public IEnumerable<CoreToken> Scan ()
		{
			yield return new CoreToken (null, TokenType.StartBlock);

			foreach (var token in Scan (Regions)) {
				yield return token;
			}

			yield return new CoreToken (null, TokenType.EndBlock);
		}

		private  IEnumerable<CoreToken> Scan (IEnumerable<IMathRegionViewModel> regions)
		{
			foreach (var region in regions) {
				foreach (var coreToken in Scan(region.Root, region)) {
					yield return coreToken;
				}
				yield return new CoreToken (new Location (region), TokenType.Semicolon);
			}
		}

		private IEnumerable<CoreToken> Scan (IToken token, IMathRegionViewModel region)
		{
			if (token is ITextToken) {
				var t = token as ITextToken;
				double value;
				if (double.TryParse (t.Text, out value))
					yield return new NumberCoreToken (new Location (region), value);
				else
					yield return new IdentifierCoreToken (new Location (region), t.Text);
			}

			if (token is IPlusToken)
				yield return new CoreToken (new Location (region), TokenType.Plus);

			if (token is IMinusToken)
				yield return new CoreToken (new Location (region), TokenType.Minus);

			if (token is IMultiplicationToken)
				yield return new CoreToken (new Location (region), TokenType.Multiplication);

			if (token is IFractionToken) {
				var fraction = token as IFractionToken;

				yield return new CoreToken (new Location (region), TokenType.OpenParentheses);
				foreach (var coreToken in Scan(fraction.Dividend, region))
					yield return coreToken;
				yield return new CoreToken (new Location (region), TokenType.CloseParentheses);

				yield return new CoreToken (new Location (region), TokenType.Division);

				yield return new CoreToken (new Location (region), TokenType.OpenParentheses);
				foreach (var t in Scan(fraction.Divisor, region))
					yield return t;
				yield return new CoreToken (new Location (region), TokenType.CloseParentheses); 
			}

			if (token is IHBoxToken) {
				var box = token as IHBoxToken;
				foreach (var t in box.Tokens)
					foreach (var coreToken in Scan(t, region))
						yield return coreToken;
			}

			if (token is IAssignmentToken) {
				yield return new CoreToken (new Location (region), TokenType.Assignment);
			}

			if (token is IParenthesesToken) {
				var parentheses = token as IParenthesesToken;

				yield return new CoreToken (new Location (region), TokenType.OpenParentheses);
				foreach (var coreToken in Scan(parentheses.Child, region))
					yield return coreToken;

				if (parentheses.ShowCloseParentheses)
					yield return new CoreToken (new Location (region), TokenType.CloseParentheses);
			}

			if (token is ICommaToken)
				yield return new CoreToken (new Location (region), TokenType.Comma);

			if (token is IExponentiationToken) {
				var exponentiation = token as IExponentiationToken;

				yield return new IdentifierCoreToken (new Location (region), "^pow");
				yield return new CoreToken (new Location (region), TokenType.OpenParentheses);
				foreach (var coreToken in Scan(exponentiation.Base, region))
					yield return coreToken;

				yield return new CoreToken (new Location (region), TokenType.Comma);

				foreach (var coreToken in Scan(exponentiation.Power, region))
					yield return coreToken;
				yield return new CoreToken (new Location (region), TokenType.CloseParentheses); 
			}

			if (token is ISquareRootToken) {
				var squareRoot = token as ISquareRootToken;

				yield return new IdentifierCoreToken (new Location (region), "^sqrt");
				yield return new CoreToken (new Location (region), TokenType.OpenParentheses);
				foreach (var coreToken in Scan(squareRoot.Child, region))
					yield return coreToken;			
				yield return new CoreToken (new Location (region), TokenType.CloseParentheses); 
			}

			if (token is IAbsoluteToken) {
				var absolute = token as IAbsoluteToken;

				yield return new IdentifierCoreToken (new Location (region), "^abs");
				yield return new CoreToken (new Location (region), TokenType.OpenParentheses);
				foreach (var coreToken in Scan(absolute.Child, region))
					yield return coreToken;			
				yield return new CoreToken (new Location (region), TokenType.CloseParentheses); 
			}
		}
	}
}

