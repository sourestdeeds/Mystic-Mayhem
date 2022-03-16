using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class KomodoDragon : BaseCreature
	{
		[Constructable]
		public KomodoDragon () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a komodo dragon";
			Body = 134;
			Hue = Utility.RandomList( 2005, 2016, 2109, 2130 );
			BaseSoundID = 362;

			SetStr( 796, 825 );
			SetDex( 86, 135 );
			SetInt( 466, 486 );

			SetHits( 800, 1050 );

			SetDamage( 14, 19 );

			SetDamageType( ResistanceType.Physical, 100 );
			SetDamageType( ResistanceType.Fire, 50 );
			SetDamageType( ResistanceType.Cold, 50 );
			SetDamageType( ResistanceType.Poison, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 80 );
			SetResistance( ResistanceType.Fire, 80 );
			SetResistance( ResistanceType.Cold, 80 );
			SetResistance( ResistanceType.Poison, 80 );
			SetResistance( ResistanceType.Energy, 80 );

			//SetSkill( SkillName.Magery, 80.1, 90.0 );
			//SetSkill( SkillName.Meditation, 80.1, 90.0 );
			//SetSkill( SkillName.EvalInt, 80.1, 90.0 );
			SetSkill( SkillName.MagicResist, 85.1, 100.0 );
			SetSkill( SkillName.Tactics, 79.3, 100.0 );
			SetSkill( SkillName.Wrestling, 79.3, 110.0 );
			SetSkill( SkillName.Anatomy, 69.3, 94.0 );
			SetSkill( SkillName.Poisoning, 60, 90 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 70;

			PackGold( 800, 1200 );
			PackMagicItems( 9, 9, 0.95, 0.95 );
			PackMagicItems( 7, 7, 0.80, 0.65 );
			//PackMagicItems( 4, 4, 0.80, 0.65 );

			if ( Utility.RandomDouble() <= 0.45 )
			{
				int amount = Utility.RandomMinMax( 5, 15 );
				PackItem( new DragonDust(amount) );
			}
			int count = Utility.RandomMinMax( 1, 8 );  //aa
			if ( count < 3 )
				PackItem( new MessageInABottle( Map.Felucca ) );
		}
		public override void OnDeath( Container c )
		{
		/*	int amt = Utility.RandomMinMax( 1, 3 );
			switch ( Utility.Random ( 200 ) )
			{
				case 0: c.DropItem( new RareForgedMetalShard1() ); break;
				case 1: c.DropItem( new RareForgedMetalShard2() ); break;
				case 2: c.DropItem( new MondainsStaffHalf1() ); break;
				case 3: c.DropItem( new MondainsStaffHalf2() ); break;
				case 4: c.DropItem( new MercenaryElixer( amt ) ); break;
				case 5: c.DropItem( new MercenaryElixer( amt ) ); break;
				case 6: c.DropItem( new MercenaryElixer( amt ) ); break;
				case 7: c.DropItem( new MercenaryElixer( amt ) ); break;
			} */

			base.OnDeath( c );
		}
		public override int TreasureMapLevel{ get{ return 6; } }
		public override int Meat{ get{ return 5; } }
	//	public override int Hides{ get{ return 10; } } 
      	//	public override HideType HideType{ get{ return HideType.Vulcon; } }

		public override bool AutoDispel{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override Poison HitPoison{ get{ return Poison.Lethal; } }

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
		    try{
			if ( 0.3 < Utility.RandomDouble() )
				return;

			switch ( Utility.Random( 3 ) )
			{
				case 0:
				{
					from.SendLocalizedMessage( 1004014 ); // You have been stunned!
					from.Freeze( TimeSpan.FromSeconds( 3.0 ) );
					break;
				}
				case 1:
				{
					from.SendAsciiMessage( "You have been hit by a paralyzing blow!" );
					from.Freeze( TimeSpan.FromSeconds( 3.0 ) );
					break;
				}
				case 2:
				{
					AOS.Damage( from, this, Utility.Random( 20, 10 ), 100, 0, 0, 0, 0 );
					from.SendAsciiMessage( "You have been hit with a massive strike!" );
					break;
				}
			}
		    }
		    catch{}
		}

		public KomodoDragon( Serial serial ) : base( serial )
		{
		}

		private DateTime m_NextBreathe;

		public override void OnActionCombat()
		{
			Mobile combatant = Combatant;

			if ( combatant == null || combatant.Deleted || combatant.Map != Map || !InRange( combatant, 12 ) || !CanBeHarmful( combatant ) || !InLOS( combatant ) )
				return;

			if ( DateTime.Now >= m_NextBreathe )
			{
				Breathe( combatant );

				m_NextBreathe = DateTime.Now + TimeSpan.FromSeconds( 5.0 + (3.0 * Utility.RandomDouble()) ); // 12-15 seconds
			}
		}

		public void Breathe( Mobile m )
		{
			DoHarmful( m );

			new BreatheTimer( m, this ).Start();

			this.Frozen = true;

			//this.MovingParticles( m, 0x1FBD, 1, 0, false, true, ( this.Hue - 1 ), 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0 );
			this.MovingParticles( m, 0x36D4, 1, 0, false, true, 2004, 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0 );
		}

		private class BreatheTimer : Timer
		{
			private KomodoDragon d;
			private Mobile m_Mobile;

			public BreatheTimer( Mobile m, KomodoDragon owner ) : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.0 ) )
			{
				d = owner;
				m_Mobile = m;
			}

			protected override void OnTick()
			{
				int damagemin = d.Hits / 20;
				int damagemax = d.Hits / 25;
				d.Frozen = false;

				m_Mobile.PlaySound( 0x11D );
				AOS.Damage( m_Mobile, Utility.RandomMinMax( damagemin, damagemax ), 0, 100, 0, 0, 0 );
				Stop();
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}