using UnityEngine;
using System.Collections;

namespace Passives
{
	public class Relocation : Passive //TODO uncompleted
	{
		/* Constructors */
		public Relocation() : base() { }
		public Relocation(Entity subject) : base(subject)
		{
			
		}

		protected override void setValues ()
		{
			passive_id = 2;

			name = "Relocation";
			desc = "Increased <color=#ffff00ff>Movement Speed</color>\nDecreased <color=#ff0000ff>Health</color>\nAbility: Relocate => Teleport instantly to a marker.";
			icon = Resources.Load<Sprite>("Sprites/UI/Passive Icons/impact-point");
		}

		public override Passive Copy ()
		{
			return new Relocation (subject);
		}
	}
}
