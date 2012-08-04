//
// Compiler.cs
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

namespace fkalc.Core
{
	public class Compiler
	{
		private Dictionary<string, double> variables = new Dictionary<string, double> ();

		public Compiler ()
		{
		}

		public Action Compile (Statement statement)
		{
			if (statement is ExpressionStatement) {
				var s = statement as ExpressionStatement;
				var result = Compile (s.Expression);

				return () =>
				{
					if (s.Location != null && s.Location.Region != null)
						s.Location.Region.SetResult (result ().ToString ());
				};
			}

			if (statement is StatementBlock) {
				var block = statement as StatementBlock;

				var actions = block.Children.Select (s => Compile (s)).ToList ();

				return () =>
				{
					foreach (var action in actions) {
						action ();
					}
				};
			}

			if (statement is Assignment) {
				var s = statement as Assignment;

				var result = Compile (s.Expression);

				return () =>
				{
					variables [s.Id] = result ();
				};
			}

			return null;
		}

		private Func<double> Compile (Expression expression)
		{
			if (expression is Const)
				return () => (expression as Const).Value;
			if (expression is Addition) {
				var addition = expression as Addition;
				var left = Compile (addition.Left);
				var right = Compile (addition.Right);
				return () => left () + right ();
			}
			if (expression is Subtraction) {
				var subtraction = expression as Subtraction;
				var left = Compile (subtraction.Left);
				var right = Compile (subtraction.Right);
				return () => left () - right ();
			}
			if (expression is Multiplication) {
				var multiplication = expression as Multiplication;
				var left = Compile (multiplication.Left);
				var right = Compile (multiplication.Right);
				return () => left () * right ();
			}
			if (expression is Division) {
				var division = expression as Division;
				var left = Compile (division.Left);
				var right = Compile (division.Right);
				return () => left () / right ();
			}
			if (expression is Variable) {
				var variable = expression as Variable;

				return () =>
				{
					var value = 0d;
					variables.TryGetValue (variable.Id, out value);

					return value;
				};
			}

			return null;
		}
	}
}

