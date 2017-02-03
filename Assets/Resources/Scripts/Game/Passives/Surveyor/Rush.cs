using UnityEngine;
using System.Collections;

namespace Passives
{
	public class Rush : Passive //TODO uncompleted
	{
		/* Constructors */
		public Rush() : base() { }
		public Rush(Entity subject) : base(subject)
		{
			
		}

		protected override void setValues ()
		{
			passive_id = 4;

			name = "Rush";
			desc = "Max Marker Count +2\nDecreased <color=#ff0000ff>Health</color>\nIncreased <color=ff00ffff>Shield Recharge Delay</color>\n" +
				"Ability: Rush => Gain a temporary 20% X <color=#ff7700ff># of markers</color> speed boost.";
			icon = Resources.Load<Sprite> ("Sprites/UI/Passive Icons/speedometer");
		}

		public override Passive Copy ()
		{
			return new Rush (subject);
		}
	}
}