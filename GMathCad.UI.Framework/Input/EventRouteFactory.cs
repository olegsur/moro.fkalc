using System;
using System.Collections.Generic;

namespace GMathCad.UI.Framework
{
	public class EventRouteFactory
	{
		public EventRouteFactory ()
		{
		}
		
		public IEnumerable<Visual> Build(Visual visual)
		{
			yield return visual;
			
			if (visual.VisaulParent != null)
			{
				foreach(var parent in Build(visual.VisaulParent))
				{
					yield return parent;
				}
			}
		}
	}
}

