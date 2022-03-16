//st    //////////////////////////////////
   //			           //
  //      Scripted by Raelis      //
 //		             	 //
//////////////////////////////////
using System;
using Server;
using Server.Items; 

namespace Server.Mobiles
{
	[CorpseName( "an ancient dragon corpse" )]
	public class AncientDragon : BaseCreature
	{
		[Constructable]
		public AncientDragon () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an ancient dragon";
			Body = 172;
			Hue = Utility.RandomList( 2419, 2406, 2413, 2418, 2213, 2425, 2207, 2219, 2220, 2117, 2129, 1150, 1153, 1161, 1259, 1175 );
			BaseSoundID = 362;

			SetStr( 1196, 1285 );
			SetDex( 90, 185 );
			SetInt( 706, 726 );

			SetHits( 1000, 1250 );

			SetDamage( 29, 35 );

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

			SetSkill( SkillName.Magery, 110.1, 120.0 );
			SetSkill( SkillName.Meditation, 110.1, 120.0 );
			SetSkill( SkillName.EvalInt, 110.1, 120.0 );
			SetSkill( SkillName.MagicResist, 115.1, 120.0 );
			SetSkill( SkillName.Tactics, 109.3, 120.0 );
			SetSkill( SkillName.Wrestling, 109.3, 120.0 );
			SetSkill( SkillName.Anatomy, 109.3, 120.0 );

			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 90;

			PackGold( 2500, 3000 );
			PackMagicItems( 5, 5, 0.95, 0.95 );
			PackMagicItems( 5, 5, 0.80, 0.65 );
			PackMagicItems( 5, 5, 0.80, 0.65 );
			PackMagicItems( 6, 6, 0.80, 0.65 );
			if ( Utility.RandomDouble() <= 0.10 )
			PackItem( new RunicSewingKit( CraftResource.SpinedLeather, 5 ) ); //st

			if ( Utility.RandomDouble() <= 0.65 )
			{
				int amount = Utility.RandomMinMax( 2, 7 );

				PackItem( new DragonDust(amount) );
			}
		}

		public override void OnDeath( Container c )
		{
			switch ( Utility.Random( 160 ))
			{
				case 0: c.DropItem( new DragonHeartArms() ); break;
				case 1: c.DropItem( new DragonHeartArmor() ); break;
				case 2: c.DropItem( new DragonHeartGloves() ); break;
				case 3: c.DropItem( new DragonHeartGorg() ); break;
				case 4: c.DropItem( new DragonHeartHelm() ); break;
				case 5: c.DropItem( new DragonHeartLegs() ); break;
				case 6: c.DropItem( new DragonHeartShield() ); break;
				case 7: c.DropItem( new DragonHeartSword() ); break;
			}

			base.OnDeath( c );
		}

		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 5; } }
		public override int Hides{ get{ return 20; } } 
      	public override HideType HideType{ get{ return HideType.Spined; } }
		public override ScaleType ScaleType{ get{ return ScaleType.All; } }
		public override int Scales{ get{ return 10; } }

		public override int GetIdleSound()
		{
			return 0x2D3;
		}

		public override int GetHurtSound()
		{
			return 0x2D1;
		}

		public override bool AutoDispel{ get{ return true; } }

		public AncientDragon( Serial serial ) : base( serial )
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

			this.MovingParticles( m, 0x1FBE, 1, 0, false, true, ( this.Hue - 1 ), 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0 );
		}

		private class BreatheTimer : Timer
		{
			private AncientDragon d;
			private Mobile m_Mobile;

			public BreatheTimer( Mobile m, AncientDragon owner ) : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.0 ) )
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