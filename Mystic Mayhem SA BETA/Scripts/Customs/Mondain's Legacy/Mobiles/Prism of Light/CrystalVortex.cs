using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a crystal vortex corpse" )]
	public class CrystalVortex : BaseCreature
	{
		[Constructable]
		public CrystalVortex() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a crystal vortex";
			Body = 0xA4;
			Hue = 0x2B2;
			BaseSoundID = 0x107;

			SetStr( 831, 896 );
			SetDex( 542, 595 );
			SetInt( 200 );

			SetHits( 359, 395 );
			SetStam( 450 );

			SetDamage( 20, 30 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Cold, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 60, 77 );
			SetResistance( ResistanceType.Fire, 0, 8 );
			SetResistance( ResistanceType.Cold, 70, 78 );
			SetResistance( ResistanceType.Poison, 40, 49 );
			SetResistance( ResistanceType.Energy, 62, 88 );

			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 120.0 );
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosFilthyRich, 3 );
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
			
			if ( Utility.RandomDouble() < 0.05 )
				c.DropItem( new JaggedCrystals() );			
			
			if ( Utility.RandomDouble() < 0.1 )				
				c.DropItem( new ParrotItem() );
				
			if ( Utility.RandomDouble() < 0.01 )
			{
				switch ( Utility.Random( 39 ) )
				{
					case 0: c.DropItem( new AssassinChest() ); break;
					case 1: c.DropItem( new AssassinArms() ); break;
					case 2: c.DropItem( new DeathChest() );	break;		
					case 4: c.DropItem( new MyrmidonArms() ); break;
					case 5: c.DropItem( new MyrmidonLegs() ); break;
					case 6: c.DropItem( new MyrmidonGorget() ); break;
					case 7: c.DropItem( new LeafweaveGloves() ); break;
					case 8: c.DropItem( new LeafweaveLegs() ); break;
					case 9: c.DropItem( new LeafweavePauldrons() ); break;
					case 10: c.DropItem( new PaladinGloves() ); break;
					case 11: c.DropItem( new PaladinGorget() ); break;
					case 12: c.DropItem( new PaladinArms() ); break;
					case 13: c.DropItem( new HunterArms() ); break;
					case 14: c.DropItem( new HunterGloves() ); break;
					case 15: c.DropItem( new HunterLegs() ); break;
					case 16: c.DropItem( new HunterChest() ); break;
					case 17: c.DropItem( new GreymistArms() ); break;
					case 18: c.DropItem( new GreymistGloves() ); break;
					case 19: c.DropItem( new GreymistChest() ); break;
					case 20: c.DropItem( new GreymistLegs() ); break;
					case 21: c.DropItem( new AssassinGloves() ); break;
					case 23: c.DropItem( new AssassinLegs() ); break;
					case 24: c.DropItem( new Evocaricus() ); break;
					case 25: c.DropItem( new MalekisHonor() ); break;
					case 26: c.DropItem( new LeafweaveChest() ); break;
					case 27: c.DropItem( new Feathernock() ); break;
					case 28: c.DropItem( new Swiftflight() ); break;
					case 29: c.DropItem( new MyrmidonChest() ); break;
					case 30: c.DropItem( new MyrmidonCloseHelm() ); break;
					case 31: c.DropItem( new MyrmidonGloves() ); break;
					case 32: c.DropItem( new DeathGloves() );	break;
					case 33: c.DropItem( new DeathArms() );	break;	
					case 34: c.DropItem( new DeathLegs() );	break;	
					case 35: c.DropItem( new DeathBoneHelm() );	break;
					case 36: c.DropItem( new PaladinChest() ); break;
					case 37: c.DropItem( new PaladinHelm() ); break;
					case 38: c.DropItem( new PaladinLegs() ); break;
				}
			}
		}

		public override int GetAngerSound() { return 0x15; }
		public override int GetAttackSound() { return 0x28; }

		public CrystalVortex( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
