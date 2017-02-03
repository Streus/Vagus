using UnityEngine;
using System.Collections;

namespace Passives
{
	public class Redistribution : Passive //TODO uncompleted
	{
		/* Constructors */
		public Redistribution() : base() { }
		public Redistribution(Entity subject) : base(subject)
		{
			
		}

		protected override void setValues ()
		{
			passive_id = 3;

			name = "Redistribution";
			desc = "Increased <color=#ffff00ff>Movement Speed</color>\nIncreased Afterburner Duration\nAbility: Redistribute => " +
				"Destroy all current markers and place 3 new ones in a triangle around you.";
			icon = Resources.Load<Sprite>("Sprites/UI/Passive Icons/triorb");
		}

		public override Passive Copy ()
		{
			return new Redistribution (subject);
		}
	}
}
