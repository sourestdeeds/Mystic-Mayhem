using System;
using System.Collections;
using Server;
using Server.Misc;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
namespace Server.Mobiles

{
	public class MysteriousImage2 : BaseCreature
	{
		public override bool CanTeach { get { return false; } }

		private Timer m_Timer;
		private Timer delaytimer;
		
		public static TimeSpan TalkDelay = TimeSpan.FromSeconds( 5.0 );
     	public DateTime m_NextTalk;
		
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( DateTime.Now >= m_NextTalk && InRange( m, 2 ) && !InRange( oldLocation, 2 ) && InLOS( m ) ) // check if its time to talk + Player in range.
			{
				m_NextTalk = DateTime.Now + TalkDelay;
				switch ( Utility.Random( 1 ))		   
				{
					case 0: Say("I shall return to exact my revenge.. I promise you.."); break;
	
				};
		
			}
		}

		[Constructable] public MysteriousImage2 ():base (AIType.AI_Mage, FightMode.None, 10, 1, 0.8, 0.8)
		{
			InitStats (1000, 1000, 1000);
			Blessed = true;
			this.Body = 970;
			this.Hue = 0;
			this.Name = "The True Aura Of Minax";
			
			this.Direction = (Direction) Utility.Random (8);
			this.AccessLevel = AccessLevel.GameMaster;
			m_Timer = new InternalTimer (this);
			m_Timer.Start ();
		}

		public override bool ClickTitle { get { return false; } }

		public MysteriousImage2 (Serial serial):base (serial)
		{
		}

		public override void OnDelete ()
		{
			if (delaytimer != null)
				delaytimer.Stop ();
			if (m_Timer != null)
				m_Timer.Stop ();
			base.OnDelete ();
		}

		public override void Serialize (GenericWriter writer)
		{
			base.Serialize (writer);
			writer.Write ((int) 0);
		}

		public override void Deserialize (GenericReader reader)
		{
			base.Deserialize (reader);
			int version = reader.ReadInt ();
		}

		private class DelayTimer:Timer
		{
			private Mobile m_Owner;
			DateTime waitfor;
			public DelayTimer (Mobile owner):base (TimeSpan.FromSeconds (5), TimeSpan.FromSeconds (5))
			{
				m_Owner = owner;
				waitfor = DateTime.Now;
			}

			protected override void OnTick ()
			{
				if (!m_Owner.Deleted)
				{
					Effects.SendLocationParticles (EffectItem.Create (m_Owner.Location, m_Owner.Map, EffectItem.DefaultDuration), 0x1fcb, 10, 14, 2023);
					m_Owner.PlaySound (0x293);
					m_Owner.Delete ();
				}
			}
		}

		public void Relocate ()
		{
			delaytimer = new DelayTimer (this);
			delaytimer.Start ();
		}
		private class InternalTimer:Timer
		{
			private MysteriousImage2 m_Owner;
			public InternalTimer (MysteriousImage2 owner):base (TimeSpan.FromSeconds (5), TimeSpan.FromSeconds (5))
			{
				m_Owner = owner;
			}
			protected override void OnTick ()
			{
				if (!m_Owner.Deleted)
				{
					m_Owner.Direction = (Direction) Utility.Random (8);
					m_Owner.Relocate ();
				}
				Stop ();
			}
		}
	}
}
