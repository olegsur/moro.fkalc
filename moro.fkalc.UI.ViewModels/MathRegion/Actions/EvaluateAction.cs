//
// EvaluateAction.cs
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
using moro.fkalc.Core;

namespace moro.fkalc.UI.ViewModels.MathRegion.Actions
{
	public class EvaluateAction : IAction
	{
		private MathRegionViewModel Region { get; set; }
		
		public EvaluateAction (MathRegionViewModel region)
		{
			if (region == null)
				throw new ArgumentNullException ("region");

			Region = region;
		}

		public void Do ()
		{
			if (!Region.Document.Regions.Any (r => r.NeedToEvaluate))
				return;

			var regions = Region.Document.Regions.GroupBy (r => r.Y).OrderBy (g => g.Key)
				.SelectMany (g => g.GroupBy (r => r.X).OrderBy (o => o.Key).SelectMany (o => o.ToList ()));

			foreach (var region in regions)
				region.SetNeedToEvaluate (false);

			new Engine ().Evaluate (regions);
		}
	}
}

