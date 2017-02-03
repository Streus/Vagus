using UnityEngine;
using System.Collections;

namespace Passives
{
	public class Markers : Passive //TODO uncompleted
	{
		/* Constructors */
		public Markers() : base() { }
		public Markers(Entity subject) : base(subject)
		{
			
		}

		protected override void setValues ()
		{
			passive_id = 1;

			name = "Markers";
			desc = "Leave a marker at your position once every five seconds.  A maximum of 3 markers can be active at a time.";
			icon = Resources.Load<Sprite>("Sprites/UI/Passive Icons/position-marker");
		}

		public override Passive Copy ()
		{
			return new Markers (subject);
		}
	}
}
