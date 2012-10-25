// 
// InsertCharacterAction.cs
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
using System.Text.RegularExpressions;
using moro.fkalc.UI.ViewModels.MathRegion.Tokens;

namespace moro.fkalc.UI.ViewModels.MathRegion.Actions
{
	public class InsertCharacterAction : IAction
	{
		private uint Key { get; set; }

		private MathRegionViewModel Region { get; set; }

		public InsertCharacterAction (uint key, MathRegionViewModel region)
		{
			Key = key;
			Region = region;
		}
		
		public void Do ()
		{
			Region.SetNeedToEvaluate (true);

			var name = Gdk.Keyval.Name (Key);

			if (Region.Selection.SelectedToken is TextToken == false) {
				var operation = new MultiplicationToken ();			
				
				new InsertHBinaryOperation (Region, operation).Do ();
			}

			var textToken = Region.Selection.SelectedToken as TextToken;

			if (!string.IsNullOrEmpty (textToken.Text) && Regex.IsMatch (textToken.Text, @"^\d") && Regex.IsMatch (name, @"\D")) {
				var operation = new MultiplicationToken ();			
				
				new InsertHBinaryOperation (Region, operation).Do ();
			}

			textToken = Region.Selection.SelectedToken as TextToken;
			
			if (string.IsNullOrEmpty (textToken.Text))
				textToken.Text += name [0];
			else 
				textToken.Text = textToken.Text.Insert (Region.Selection.Position, name [0].ToString ());

			Region.Selection.Position++;
		}
	}
}