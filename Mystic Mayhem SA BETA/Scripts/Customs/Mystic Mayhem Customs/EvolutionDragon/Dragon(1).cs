    //////////////////////////////////
   //			           //
  //      Scripted by Raelis      //
 //		             	 //
//////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class Dragon1 : BaseCreature
	{
		[Constructable]
		public Dragon1 () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a dragon";
			Body = 59;
			Hue = Utility.RandomList( 2419, 2406, 2413, 2418, 2213, 2425, 2207, 2219, 2220, 2117, 2129, 1150, 1153, 1161, 1259, 1175 );
			BaseSoundID = 362;

			SetStr( 796, 825 );
			SetDex( 86, 135 );
			SetInt( 466, 486 );

			SetHits( 800, 1050 );

			SetDamage( 19, 25 );

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

			SetSkill( SkillName.Magery, 80.1, 90.0 );
			SetSkill( SkillName.Meditation, 80.1, 90.0 );
			SetSkill( SkillName.EvalInt, 80.1, 90.0 );
			SetSkill( SkillName.MagicResist, 85.1, 90.0 );
			SetSkill( SkillName.Tactics, 59.3, 74.0 );
			SetSkill( SkillName.Wrestling, 59.3, 74.0 );
			SetSkill( SkillName.Anatomy, 59.3, 74.0 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 70;

			PackGold( 800, 1200 );
			PackMagicItems( 4, 4, 0.95, 0.95 );
			PackMagicItems( 4, 4, 0.80, 0.65 );
			PackMagicItems( 4, 4, 0.80, 0.65 );

			if ( Utility.RandomDouble() <= 0.45 )
			{
				int amount = Utility.RandomMinMax( 1, 5 );

				PackItem( new DragonDust(amount) );
			}
		}
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 5; } }
		public override int Hides{ get{ return 12; } } 
      	public override HideType HideType{ get{ return HideType.Spined; } }

		public override bool AutoDispel{ get{ return true; } }

		public Dragon1( Serial serial ) : base( serial )
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

				m_NextBreathe = DateTime.Now + TimeSpan.FromSeconds( 12.0 + (3.0 * Utility.RandomDouble()) ); // 12-15 seconds
			}
		}

		public void Breathe( Mobile m )
		{
			DoHarmful( m );

			new BreatheTimer( m, this ).Start();

			this.Frozen = true;

			this.MovingParticles( m, 0x1FBD, 1, 0, false, true, ( this.Hue - 1 ), 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0 );
		}

		private class BreatheTimer : Timer
		{
			private Dragon1 d;
			private Mobile m_Mobile;

			public BreatheTimer( Mobile m, Dragon1 owner ) : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.0 ) )
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