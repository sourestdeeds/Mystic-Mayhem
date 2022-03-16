    //////////////////////////////////
   //			           //
  //      Scripted by Raelis      //
 //		             	 //
//////////////////////////////////
using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a giant dragon hatchling corpse" )]
	public class GiantDragonHatchling : BaseCreature
	{
		[Constructable]
		public GiantDragonHatchling() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a giant dragon hatchling";
			Body = 89;
			Hue = Utility.RandomList( 2419, 2406, 2413, 2418, 2213, 2425, 2207, 2219, 2220, 2117, 2129, 1150, 1153, 1161, 1259, 1175 );
			BaseSoundID = 219;

			SetStr( 496, 525 );
			SetDex( 76, 95 );
			SetInt( 106, 126 );

			SetHits( 500, 750 );

			SetDamage( 12, 18 );

			SetDamageType( ResistanceType.Physical, 100 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Cold, 25 );
			SetDamageType( ResistanceType.Poison, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 20 );
			SetResistance( ResistanceType.Fire, 20 );
			SetResistance( ResistanceType.Cold, 20 );
			SetResistance( ResistanceType.Poison, 20 );
			SetResistance( ResistanceType.Energy, 20 );

			SetSkill( SkillName.Magery, 60.1, 70.0 );
			SetSkill( SkillName.Meditation, 60.1, 70.0 );
			SetSkill( SkillName.EvalInt, 60.1, 70.0 );
			SetSkill( SkillName.MagicResist, 65.1, 70.0 );
			SetSkill( SkillName.Tactics, 39.3, 54.0 );
			SetSkill( SkillName.Wrestling, 39.3, 54.0 );
			SetSkill( SkillName.Anatomy, 39.3, 54.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 40;

			PackGold( 150, 200 );
			PackMagicItems( 2, 3, 0.95, 0.95 );
			PackMagicItems( 2, 3, 0.80, 0.65 );

			if ( Utility.RandomDouble() <= 0.15 )
			{
				int amount = Utility.RandomMinMax( 1, 5 );

				PackItem( new DragonDust(amount) );
			}
		}

		public GiantDragonHatchling(Serial serial) : base(serial)
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

			this.MovingParticles( m, 0x1FA9, 1, 0, false, true, ( this.Hue - 1 ), 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0 );
		}

		private class BreatheTimer : Timer
		{
			private GiantDragonHatchling d;
			private Mobile m_Mobile;

			public BreatheTimer( Mobile m, GiantDragonHatchling owner ) : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.0 ) )
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

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			if ( BaseSoundID == -1 )
				BaseSoundID = 219;
		}
	}
}