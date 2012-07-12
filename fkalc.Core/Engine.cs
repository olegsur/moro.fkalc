//
// Engine.cs
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

namespace fkalc.Core
{
	public class Engine
	{
		public Engine ()
		{
		}

		public void Evaluate (IEnumerable<MathRegionToken> tokens)
		{
			var coreTokens = new Scaner (tokens).Scan ();

			var tree = new Parser (coreTokens).ParseAssinmentStatement ();

			var result = Evaluate (tree);

			//resultToken.Child = new TextToken () {Text = result.ToString()};
		}

		public double Evaluate (Statement statement)
		{
			if (statement is StatementExpression)
				return Evaluate ((statement as StatementExpression).Expression);

			return 0;
		}

		private double Evaluate (Expression expression)
		{
			if (expression is Const)
				return (expression as Const).Value;
			if (expression is Addition)
				return Evaluate ((expression as Addition).Left) + Evaluate ((expression as Addition).Right);
			if (expression is Subtraction)
				return Evaluate ((expression as Subtraction).Left) - Evaluate ((expression as Subtraction).Right);
			if (expression is Multiplication)
				return Evaluate ((expression as Multiplication).Left) * Evaluate ((expression as Multiplication).Right);
			if (expression is Division)
				return Evaluate ((expression as Division).Left) / Evaluate ((expression as Division).Right);

			return 0;
		}
	}
}

