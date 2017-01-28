using UnityEngine;
using System.Collections;

public abstract class Ability
{
	/* Instance Vars */
	protected int ability_id;

	protected string name;
	protected string desc;
	protected Sprite icon;

	protected float coolMax;
	protected float coolCurr;
	protected float coolPerc;

	protected int chargesMax;
	protected int chargesCurr;

	protected Entity subject;

	//protected Animation preAnim;
	//protected Animation channelAnim;
	//protected Animation postAnim;

	/* Accessors */
	public int AbilityID
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
	public float CooldownPercentage
	{
		get{ return coolPerc; }
		set{ coolPerc = value; }
	}
	public float EffectiveCooldown
	{
		get{ return coolMax * coolPerc; }
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
	public Ability()
	{
		ability_id = -1; //unlisted ability

		name = "NULL_NAME";
		desc = "NULL ABILITY";
		icon = null;

		coolMax = 0f;
		coolPerc = 1f;
		coolCurr = EffectiveCooldown;

		chargesMax = 0;
		chargesCurr = chargesMax;

		this.subject = null;
	}
	public Ability(Entity subject)
	{
		ability_id = -1; //unlisted ability

		name = "DEFUALT_NAME";
		desc = "UNINITIALIZED ABILITY";
		icon = null;

		coolMax = 0f;
		coolPerc = 1f;
		coolCurr = EffectiveCooldown;

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
			coolCurr = EffectiveCooldown;
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
				coolCurr = EffectiveCooldown;
		}
	}

	// Create a string representation of this ability
	public override string ToString ()
	{
		return name + "\n" + desc + "\nCooldown: " + EffectiveCooldown + " seconds\nMax Charges: " + chargesMax;
	}
}
