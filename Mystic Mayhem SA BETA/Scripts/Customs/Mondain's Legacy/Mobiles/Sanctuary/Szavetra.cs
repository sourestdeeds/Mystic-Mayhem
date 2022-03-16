using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Factions;

namespace Server.Mobiles
{
	[CorpseName( "a szavetra corpse" )]
	public class Szavetra : Succubus
	{
		[Constructable]
		public Szavetra () : base()
		{
			Name = "Szavetra";
			Body = 0xAE;
			Hue = 306;
			BaseSoundID = 0x4B0;

			SetStr( 950, 1240 );
			SetDex( 242, 340 );
			SetInt( 998, 1314 );
			SetStam( 129, 243 );
			SetMana( 582, 1105 );

			SetHits( 13000, 13500 );

			SetDamage( 28, 38 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 80, 90 );
			SetResistance( ResistanceType.Fire, 70, 80 );
			SetResistance( ResistanceType.Cold, 40, 45 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 50, 60 );

			SetSkill( SkillName.EvalInt, 160.1, 180.0 ); //90-100
			SetSkill( SkillName.Magery, 180.1, 185.0 ); // "
			SetSkill( SkillName.Meditation, 160.1, 180.0 ); //"
			SetSkill( SkillName.MagicResist, 200.5, 220.0 ); //150
			SetSkill( SkillName.Tactics, 160.1, 180.0 );
			SetSkill( SkillName.Wrestling, 160.1, 180.0 );

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 80; //80
		//	PackItem( new Silver( 8, 18 ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 4 );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override void OnDeath( Container c )
		{
			if ( Utility.Random( 3 ) < 1 )
				c.DropItem( new Muculent() );
	/*		int swtch = Utility.Random(160);
			if ( swtch < 80 )
			{
				switch ( Utility.Random( 6 ) )
				{
				case 0: c.DropItem( new OvenMitts(2) ); break;
				case 1: c.DropItem( new NimbleFingers(2) ); break;
				case 2: c.DropItem( new RubberGloves(2) ); break;
				case 3: c.DropItem( new TailorsTouch(2) ); break;
				case 4: c.DropItem( new TinkersAid(2) ); break;
				case 5: c.DropItem( new WoodWonder(2) ); break;
				}
			}
			else if ( swtch < 115 )
			{
				switch ( Utility.Random( 6 ) )
				{
				case 0: c.DropItem( new OvenMitts(4) ); break;
				case 1: c.DropItem( new NimbleFingers(4) ); break;
				case 2: c.DropItem( new RubberGloves(4) ); break;
				case 3: c.DropItem( new TailorsTouch(4) ); break;
				case 4: c.DropItem( new TinkersAid(4) ); break;
				case 5: c.DropItem( new WoodWonder(4) ); break;
				}
			}
			else if ( swtch < 125 )
			{
				switch ( Utility.Random( 6 ) )
				{
				case 0: c.DropItem( new OvenMitts(6) ); break;
				case 1: c.DropItem( new NimbleFingers(6) ); break;
				case 2: c.DropItem( new RubberGloves(6) ); break;
				case 3: c.DropItem( new TailorsTouch(6) ); break;
				case 4: c.DropItem( new TinkersAid(6) ); break;
				case 5: c.DropItem( new WoodWonder(6) ); break;
				}
			} */
			base.OnDeath( c );
		}



		public override int Meat{ get{ return 10; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public void DrainLife()
		{
			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 2 ) )
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;

				if ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team) )
					list.Add( m );
				else if ( m.Player )
					list.Add( m );
			}

			foreach ( Mobile m in list )
			{
				DoHarmful( m );

				m.FixedParticles( 0x374A, 10, 15, 5013, 0x496, 0, EffectLayer.Waist );
				m.PlaySound( 0x231 );

				m.SendMessage( "You feel the life drain out of you!" );

				int toDrain = Utility.RandomMinMax( 10, 40 );

				Hits += toDrain;
				m.Damage( toDrain, this );
			}
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.1 >= Utility.RandomDouble() )
				DrainLife();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.1 >= Utility.RandomDouble() )
				DrainLife();
		}

		public Szavetra( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}