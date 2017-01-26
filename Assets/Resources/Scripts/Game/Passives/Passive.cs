using UnityEngine;
using System.Collections;
using System;

namespace EntityStats
{
	public abstract class Passive
	{
		/* Instance Vars */
		protected int passive_id;

		protected string name;
		protected string desc;
		protected Sprite icon;

		protected Entity subject;

		private bool applied;

		/* Accessors */
		public string PassiveID
		{
			get{ return passive_id; }
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

		public Entity Subject
		{
			get{ return subject; }
		}

		/* Constructors */
		public Passive(Entity subject)
		{
			passive_id = -1; //unlisted passive

			name = "DEFUALT_NAME";
			desc = "UNINITIALIZED PASSIVE";
			icon = null;

			this.subject = subject;

			applied = false;
		}

		/* Deconstructors */
		~Passive()
		{
			if (applied && subject != null)
				revert ();
		}

		/* Instance Methods */

		// Apply some change to the subject
		public virtual void apply()
		{
			applied = true;
			return;
		}

		// Revert the change applied by this Passive
		public virtual void revert()
		{
			applied = false;
			return;
		}

		// Called by Entity for constant behaviors
		// Arg: the deltaTime from subject Entity
		public virtual void OnUpdate(float dec){ }

		// Called by Entity for bullet hit behaviors
		// Arg: the bullet that hit subject Entity
		public virtual void OnBulletHit(Bullet bullet){ }

		//TODO more Passive hooks?

		// Create another instance of this Passive with the same subject
		public abstract Passive Copy (Entity e);

		// Compare this Passive to another
		public override bool Equals (object obj)
		{
			return this.passive_id == ((Passive)obj).passive_id;
		}

		// Create a string for displaying information on this Passive
		public override string ToString ()
		{
			return name + "\n" + desc;
		}
	}
}