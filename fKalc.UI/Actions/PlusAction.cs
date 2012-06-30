// 
// PlusAction.cs
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

namespace fKalc.UI
{
	public class PlusAction
	{
		private MathRegion Region { get; set; }
		
		public PlusAction (MathRegion region)
		{
			Region = region;
		}
		
		public void Execute ()
		{
			var operation = new PlusToken ();			
				
			var right = new TextToken ();
				
			if (Region.ActiveToken.Parent is HBoxToken) {				
				var parent = Region.ActiveToken.Parent as HBoxToken;	

				parent.Add (operation);	
				parent.Add (right);
			} else {				
				var parent = Region.ActiveToken.Parent;
				
				var container = new HBoxToken ();				
				var token = Region.ActiveToken;
				
				parent.Replace (token, container);
				
				container.Add (token);
				container.Add (operation);
				container.Add (right);
			}
				
			Region.ActiveToken = right;
		}
	}
}