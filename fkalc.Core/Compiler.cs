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
		private Scope CurrentScope = new Scope ();
		private List<FunctionSignature> functions = new List<FunctionSignature> ();

		public Compiler ()
		{
			functions.Add (new FunctionSignature () { Id = "^pow", Parameters = new[] {"x","y"}, Body = () => Math.Pow(CurrentScope.GetVariable("x"), CurrentScope.GetVariable("y"))});

			functions.Add (new FunctionSignature () { Id = "^sqrt", Parameters = new[] {"x"}, Body = () => Math.Sqrt(CurrentScope.GetVariable("x"))});
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

			if (statement is AssignmentVariable) {
				var s = statement as AssignmentVariable;

				var result = Compile (s.Expression);

				return () =>
				{
					CurrentScope.SetVariable (s.Id, result ());
				};
			}

			if (statement is AssignmentFunction) {
				var s = statement as AssignmentFunction;

				var result = Compile (s.Body);

				return () =>
				{
					var index = functions.FindIndex (f => f.Id == s.Id);

					var function = new FunctionSignature () {Id = s.Id, Parameters = s.Parameters, Body = result };

					if (index != -1)
						functions [index] = function;
					else
						functions.Add (function);
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
					return CurrentScope.GetVariable (variable.Id);
				};
			}
			if (expression is Function) {
				var function = expression as Function;

				var arg_list = function.Args.Select (arg => Compile (arg)).ToList ();

				return () =>
				{
					var func = functions.FirstOrDefault (f => f.Id == function.Id && f.Parameters.Count () == arg_list.Count);
			
					if (func == null)
						return 0;

					var parentScope = CurrentScope;
					CurrentScope = new Scope () { Parent = parentScope};

					var i = 0;
					foreach (var p in func.Parameters) {
						var value = arg_list [i] ();
						CurrentScope.SetVariable (p, value);
						i++;
					}

					var result = func.Body ();

					CurrentScope.Parent = null;
					CurrentScope = parentScope;

					return result;
				};
			}


			return null;
		}
	}

	public class Scope
	{
		public Scope Parent { get; set; }

		private Dictionary<string, double> variables = new Dictionary<string, double> ();

		public Scope ()
		{
		}

		public void SetVariable (string id, double value)
		{
			variables [id] = value;
		}

		public double GetVariable (string id)
		{
			var result = 0d;

			if (variables.TryGetValue (id, out result)) {
				return result;
			}

			return Parent == null ? 0 : Parent.GetVariable (id);
		}

	}

	public class FunctionSignature
	{
		public string Id { get; set; }
		public string[] Parameters { get; set; }
		public Func<double> Body { get; set; }
	}
}

