using System;

namespace GMathCad.UI.Framework
{
	public class Visual
	{
		public Visual VisaulParent { get; private set; }
		
		public Visual ()
		{
		}
		
		public virtual Visual HitTest (double x, double y)
		{			
			return null;
		}
		
		protected internal void AddVisualChild (Visual visual)
		{
			visual.VisaulParent = this;
		}
		
		protected internal void RemoveVisualChild (Visual visual)
		{
			visual.VisaulParent = null;
		}
	}
}

