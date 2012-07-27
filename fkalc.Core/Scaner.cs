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
using fkalc.UI.Common.MathRegion;
using fkalc.UI.Common.MathRegion.Tokens;

namespace fkalc.Core
{
	public class Scaner
	{
		private IEnumerable<IMathRegionViewModel> Tokens { get; set; }

		public Scaner (IEnumerable<IMathRegionViewModel> tokens)
		{
			Tokens = tokens;
		}

		public IEnumerable<CoreToken> Scan ()
		{
			yield return new StartBlockCoreToken ();

			foreach (var token in Scan (Tokens)) {
				yield return token;
			}

			yield return new EndBlockCoreToken ();
		}

		private  IEnumerable<CoreToken> Scan (IEnumerable<IMathRegionViewModel> tokens)
		{
			foreach (var mathToken in tokens) {
				foreach (var coreToken in Scan(mathToken.Root)) {
					yield return coreToken;
				}
				yield return new SemicolonCoreToken ();
			}
		}

		private IEnumerable<CoreToken> Scan (IToken token)
		{
			if (token is ITextToken) {
				var t = token as ITextToken;
				double value;
				double.TryParse (t.Text, out value);

				yield return new NumberCoreToken (value);
			}

			if (token is IPlusToken)
				yield return new PlusCoreToken ();

			if (token is IMinusToken)
				yield return new MinusCoreToken ();

			if (token is IMultiplicationToken)
				yield return new MultiplicationCoreToken ();

			if (token is IFractionToken) {
				var fraction = token as IFractionToken;

				yield return new OpenBracketCoreToken ();
				foreach (var coreToken in Scan(fraction.Dividend))
					yield return coreToken;
				yield return new CloseBracketCoreToken ();

				yield return new DivisionCoreToken ();

				yield return new OpenBracketCoreToken ();
				foreach (var t in Scan(fraction.Divisor))
					yield return t;
				yield return new CloseBracketCoreToken ();
			}

			if (token is IHBoxToken) {
				var box = token as IHBoxToken;
				foreach (var t in box.Tokens)
					foreach (var coreToken in Scan(t))
						yield return coreToken;
			}

			if (token is IAssignmentToken) {
				yield return new AssignmentCoreToken ();
			}
		}
	}
}

