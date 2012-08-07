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
			Expect (TokenType.StartBlock);

			while (enumerator.Current.Type != TokenType.EndBlock) {
				try {
					result.Add (ParseExpressionStatement ());
				} catch {
					if (enumerator.Current.Location != null) {
						enumerator.Current.Location.Region.HasError = true;
					}

					while (enumerator.Current.Type != TokenType.Semicolon) {
						enumerator.MoveNext ();
					}

					enumerator.MoveNext ();
				}
			}

			Expect (TokenType.EndBlock);

			return result;
		}

		private Statement ParseExpressionStatement ()
		{
			Statement result = null;

			var expression = ParseExpression ();

			if (enumerator.Current.Type == TokenType.Assignment) {
				if (expression is Variable) {
					enumerator.MoveNext ();
					var right = ParseExpression ();

					var variable = expression as Variable;

					result = new AssignmentVariable (variable.Id, right) { Location = expression.Location };
				} else if (expression is Function) {
					enumerator.MoveNext ();
					var right = ParseExpression ();

					var function = expression as Function;

					var args = function.Args.Cast<Variable> ().Select (v => v.Id).ToArray ();

					result = new AssignmentFunction (function.Id, args, right) { Location = expression.Location };
				}
			} else {
				result = new ExpressionStatement (expression) { Location = expression.Location };
			}

			Expect (TokenType.Semicolon);

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

				Expression right;

				switch (opt.Type) {
				case TokenType.Plus:
					enumerator.MoveNext ();
					right = ParseMultiplicativeExpression ();

					left = new Addition (left, right) { Location = opt.Location };
					continue;
				case TokenType.Minus:
					enumerator.MoveNext ();
					right = ParseMultiplicativeExpression ();

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

				Expression right;

				switch (opt.Type) {
				case TokenType.Multiplication:
					enumerator.MoveNext ();
					right = ParseUnaryExpression ();

					left = new Multiplication (left, right) { Location = opt.Location };
					continue;
				case TokenType.Division:
					enumerator.MoveNext ();
					right = ParseUnaryExpression ();

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
			Expression result = null;

			if (enumerator.Current is NumberCoreToken) {
				var token = enumerator.Current as NumberCoreToken;
				result = new Const (token.Value) { Location = token.Location };
				enumerator.MoveNext ();
			} else if (enumerator.Current.Type == TokenType.OpenParentheses)
				result = ParseTuple ();
			else {
				if (enumerator.Current is IdentifierCoreToken) {
					var token = enumerator.Current as IdentifierCoreToken;
					var id = token.Id;

					enumerator.MoveNext ();

					if (enumerator.Current.Type == TokenType.OpenParentheses) {	
						result = ParseFunctionCall (id);
						result.Location = token.Location;
					} else					
						result = new Variable (id) { Location = token.Location };
				}
			}

			return result;
		}


		private Expression ParseFunctionCall (string id)
		{
			Expect (TokenType.OpenParentheses);

			var arg_list = ParseArgumentList ();

			Expect (TokenType.CloseParentheses);

			return new Function (id, arg_list.ToArray ());
		}

		private IEnumerable<Expression> ParseArgumentList ()
		{
			var result = new List<Expression> ();

			result.Add (ParseExpression ());

			return result;
		}

		private Expression ParseTuple ()
		{
			Expect (TokenType.OpenParentheses);

			var result = ParseExpression ();

			Expect (TokenType.CloseParentheses);

			return result;
		}

		private void Expect (TokenType type)
		{
			if (enumerator.Current.Type == type) {
				enumerator.MoveNext ();
				return;
			}

			throw new Exception ("Error");
		}
	}
}
