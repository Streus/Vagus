using UnityEngine;
using System.Collections;

namespace EntityStats
{
	public abstract class Ability
	{
		/* Instance Vars */
		protected int ability_id;

		protected string name;
		protected string desc;
		protected Sprite icon;

		protected float coolMax;
		protected float coolCurr;

		protected int chargesMax;
		protected int chargesCurr;

		protected Entity subject;

		//protected Animation preAnim;
		//protected Animation channelAnim;
		//protected Animation postAnim;

		/* Accessors */
		public string AbilityID
		{
			get{ return ability_id; }
		}

		public string Name
		{
			get{ return name; }
		}
		public string Desc
		{
			get{ return desc; }
		}
		public Sprite Icon
		{
			get{ return icon; }
		}

		public float CooldownMax
		{
			get{ return coolMax; }
			set{ coolMax = value; }
		}
		public float CooldownCurr
		{
			get{ return coolCurr; }
			set{ coolCurr = value; }
		}

		public int ChargesMax
		{
			get{ return chargesMax; }
			set{ chargesMax = value; }
		}
		public int ChargesCurr
		{
			get{ return chargesCurr; }
			set{ chargesCurr = value; }
		}

		public Entity Subject
		{
			get{ return subject; }
		}

		//public Animation PreAnimation
		//{
		//	get{ return preAnim; }
		//}
		//public Animation ChannelAnimation
		//{
		//	get{ return channelAnim; }
		//}
		//public Animation PostAnimation
		//{
		//	get{ return postAnim; }
		//}

		/* Constructors */
		public Ability(Entity subject)
		{
			ability_id = -1; //unlisted ability

			name = "DEFUALT_NAME";
			desc = "UNINITIALIZED ABILITY";
			icon = null;

			coolMax = 0f;
			coolCurr = coolMax;

			chargesMax = 0;
			chargesCurr = chargesMax;

			this.subject = subject;
		}

		/* Instance Methods */

		// Is this ability useable?
		public bool ready()
		{
			return (chargesCurr >= 1 || coolCurr <= 0f);
		}

		// Invoke the ability
		public void use()
		{
			if (!ready ())
				return;

			//TODO ability animations?
			useEffect ();
			
			if (chargesCurr >= 1)
				chargesCurr--;

			if (chargesCurr < chargesMax || chargesMax == 0)
				coolCurr = coolMax;
		}

		// Custom ability use behavior goes here
		protected abstract void useEffect();

		// Called by the subject Entity during its update
		// Arg: the deltaTime of this update
		public void OnUpdate(float dec)
		{
			coolCurr -= Mathf.Min (coolCurr, dec);
			if (coolCurr == 0f && chargesCurr < chargesMax)
			{
				chargesCurr++;
				if (chargesCurr != chargesMax)
					coolCurr = coolMax;
			}
		}

		// Create a string representation of this ability
		public override string ToString ()
		{
			return name + "\n" + desc + "\nCooldown: " + coolMax + " seconds\nMax Charges: " + chargesMax;
		}
	}
}
