//
// Parser.cs
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
using System.Collections.Generic;

namespace fkalc.Core
{
	public class Parser
	{
		private IEnumerator<CoreToken> enumerator;

		public Parser (IEnumerable<CoreToken> tokens)
		{
			enumerator = tokens.GetEnumerator ();
			enumerator.MoveNext ();
		}

		public StatementBlock ParseStatementBlock ()
		{
			var result = new StatementBlock ();
			Expect<StartBlockCoreToken> ();

			while (enumerator.Current is EndBlockCoreToken == false) {
				result.Add (ParseExpressionStatement ());
			}

			Expect<EndBlockCoreToken> ();

			return result;
		}

		private Statement ParseExpressionStatement ()
		{
			Statement result = null;

			var expression = ParseExpression ();

			if (enumerator.Current is AssignmentCoreToken) {
				if (expression is Variable) {
					enumerator.MoveNext ();
					var right = ParseExpression ();

					var variable = expression as Variable;

					result = new Assignment (variable.Id, right) { Location = expression.Location };
				}
			} else {
				result = new ExpressionStatement (expression) { Location = expression.Location };
			}

			Expect<SemicolonCoreToken> ();

			return result;
		}

		private Expression ParseExpression ()
		{
			var left = ParseAdditiveExpression ();
			return left;
		}

		private Expression ParseAdditiveExpression ()
		{
			var left = ParseMultiplicativeExpression ();

			while (true) {			
				var opt = enumerator.Current;

				if (opt is PlusCoreToken) {
					enumerator.MoveNext ();
					var right = ParseMultiplicativeExpression ();

					left = new Addition (left, right) { Location = opt.Location };
					continue;
				}

				if (opt is MinusCoreToken) {
					enumerator.MoveNext ();
					var right = ParseMultiplicativeExpression ();

					left = new Subtraction (left, right) { Location = opt.Location };
					continue;
				}

				break;
			}

			return left;
		}

		private Expression ParseMultiplicativeExpression ()
		{
			var left = ParseUnaryExpression ();

			while (true) {			
				var opt = enumerator.Current;

				if (opt is MultiplicationCoreToken) {
					enumerator.MoveNext ();
					var right = ParseUnaryExpression ();

					left = new Multiplication (left, right) { Location = opt.Location };
					continue;
				}

				if (opt is DivisionCoreToken) {
					enumerator.MoveNext ();
					var right = ParseUnaryExpression ();

					left = new Division (left, right) { Location = opt.Location };
					continue;
				}

				break;
			}
			return left;
		}	

		private Expression ParseUnaryExpression ()
		{
			return ParsePrimaryExpression ();
		}

		private Expression ParsePrimaryExpression ()
		{
			if (enumerator.Current is NumberCoreToken) {
				var token = enumerator.Current as NumberCoreToken;
				var result = new Const (token.Value) { Location = token.Location };
				enumerator.MoveNext ();
				return result;
			}

			if (enumerator.Current is OpenBracketCoreToken)
				return ParseTuple ();

			return ParseIdentifier ();
		}

		private Expression ParseIdentifier ()
		{
			if (enumerator.Current is IdentifierCoreToken) {
				var token = enumerator.Current as IdentifierCoreToken;
				enumerator.MoveNext ();
				return new Variable (token.Id) { Location = token.Location };
			}

			return null;
		}

		private Expression ParseTuple ()
		{
			Expect<OpenBracketCoreToken> ();

			var result = ParseExpression ();

			Expect<CloseBracketCoreToken> ();

			return result;
		}

		private void Expect<T> () where T: CoreToken
		{
			if (enumerator.Current is T) {
				enumerator.MoveNext ();
				return;
			}

			throw new Exception ("Error");
		}				                       
	}
}
