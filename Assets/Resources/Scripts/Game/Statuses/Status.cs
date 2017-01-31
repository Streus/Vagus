using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Status : Passive
{
	/* Instance Vars */
	protected int status_id;

	protected float durationRem;
	protected float durationMax;

	protected int stacksCurr;
	protected int stacksMax;

	private DecayType stacktype;

	private List<Status> subjectStatuses;

	/* Accessors */
	public int StatusID
	{
		get{ return status_id; }
	}

	public float DurationRem
	{
		get{ return durationRem; }
	}
	public float DurationMax
	{
		get{ return durationMax; }
	}

	public int StacksCurrent
	{
		get{ return stacksCurr; }
		set
		{
			if (stacktype == DecayType.none)
			{
				durationRem = durationMax;
				return;
			}
			revert ();
			stacksCurr = Mathf.Clamp (value, 1, stacksMax);
			apply ();
		}
	}
	public int StacksMax
	{
		get{ return stacksMax; }
	}

	public DecayType Stacktype
	{
		get{ return stacktype; }
	}

	/* Constructors */
	public Status(float dur, Entity subject, DecayType dt = DecayType.none) : base(subject)
	{
		passive_id = 0; //passive child
		status_id = -1; //unlisted status

		name = "NULL_NAME";
		desc = "NULL STATUS";
		icon = null;

		this.subject = subject;
		subject.getStatusList (out subjectStatuses);

		durationMax = dur;
		durationRem = durationMax;

		stacktype = dt;
	}

	/* Instance Methods */

	// Update durationRem and handle stack decay / status removal
	public override void OnUpdate (float dec)
	{
		durationRem -= dec;
		if (durationRem <= 0)
		{
			switch (stacktype)
			{
			case DecayType.none:
				subjectStatuses.Remove (this);
				break;

			case DecayType.communal:
				subjectStatuses.Remove (this);
				break;

			case DecayType.serial:
				stacksCurr--;
				if (stacksCurr <= 0)
					subjectStatuses.Remove (this);
				else {
					StacksCurrent = stacksCurr;
				}
				break;
			}
		}
	}

	public override bool Equals (object obj)
	{
		return this.status_id == ((Status)obj).status_id;
	}

}

public enum DecayType
{
	//status duration is refresed instead of stacking
	none,

	//All stacks are removed on duration end
	communal,

	//Stacks remove one at a time.
	serial
}
	