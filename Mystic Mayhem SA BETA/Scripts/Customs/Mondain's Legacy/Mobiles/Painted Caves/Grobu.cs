using System;
using Server;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "a grobu's corpse" )] 
	public class Grobu : BaseCreature 
	{ 		
		[Constructable] 
		public Grobu() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 			
			Name = "a grobu";
			Hue = 0x455;
			Body = 0xD3;
			
			SetStr( 192, 210 );
			SetDex( 132, 150 );
			SetInt( 50, 52 );
			
			SetHits( 1244, 1299 );
			SetStam( 132, 150 );
			SetMana( 8 );

			SetDamage( 6, 12 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 45 );
			SetResistance( ResistanceType.Fire, 20, 32 );
			SetResistance( ResistanceType.Cold, 32, 35 );
			SetResistance( ResistanceType.Poison, 25, 29 );
			SetResistance( ResistanceType.Energy, 22, 34 );

			SetSkill( SkillName.Wrestling, 107.4, 119.0 );	
			SetSkill( SkillName.Tactics, 97.3, 116.5 );
			SetSkill( SkillName.MagicResist, 66.2, 83.7 );

			Fame = 1000;
			Karma = 1000;			
		}

		public Grobu( Serial serial ) : base( serial )
		{
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosFilthyRich, 3 );
		}		
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );	
			
			c.DropItem( new GrobusFur() );
			
			if ( Utility.RandomDouble() < 0.01 )
			{
				switch ( Utility.Random( 37 ) )
				{
					case 0: c.DropItem( new AssassinChest() ); break;
					case 1: c.DropItem( new AssassinArms() ); break;
					case 2: c.DropItem( new DeathChest() );	break;		
					case 3: c.DropItem( new MyrmidonArms() ); break;
					case 4: c.DropItem( new MyrmidonLegs() ); break;
					case 5: c.DropItem( new MyrmidonGorget() ); break;
					case 6: c.DropItem( new LeafweaveGloves() ); break;
					case 7: c.DropItem( new LeafweaveLegs() ); break;
					case 8: c.DropItem( new LeafweavePauldrons() ); break;
					case 9: c.DropItem( new PaladinGloves() ); break;
					case 10: c.DropItem( new PaladinGorget() ); break;
					case 11: c.DropItem( new PaladinArms() ); break;
					case 12: c.DropItem( new HunterArms() ); break;
					case 13: c.DropItem( new HunterGloves() ); break;
					case 14: c.DropItem( new HunterLegs() ); break;
					case 15: c.DropItem( new HunterChest() ); break;
					case 16: c.DropItem( new GreymistArms() ); break;
					case 17: c.DropItem( new GreymistGloves() ); break;
					case 18: c.DropItem( new GreymistChest() ); break;
					case 19: c.DropItem( new GreymistLegs() ); break;
					case 20: c.DropItem( new AssassinGloves() ); break;
					case 21: c.DropItem( new AssassinLegs() ); break;
					case 22: c.DropItem( new Evocaricus() ); break;
					case 23: c.DropItem( new MalekisHonor() ); break;
					case 24: c.DropItem( new LeafweaveChest() ); break;
					case 25: c.DropItem( new Feathernock() ); break;
					case 26: c.DropItem( new Swiftflight() ); break;
					case 27: c.DropItem( new MyrmidonChest() ); break;
					case 28: c.DropItem( new MyrmidonCloseHelm() ); break;
					case 29: c.DropItem( new MyrmidonGloves() ); break;
					case 30: c.DropItem( new DeathGloves() );	break;
					case 31: c.DropItem( new DeathArms() );	break;	
					case 32: c.DropItem( new DeathLegs() );	break;	
					case 33: c.DropItem( new DeathBoneHelm() );	break;
					case 34: c.DropItem( new PaladinChest() ); break;
					case 35: c.DropItem( new PaladinHelm() ); break;
					case 36: c.DropItem( new PaladinLegs() ); break;
				}
			}
		}
		
		public override bool GivesMinorArtifact{ get{ return true; } }
		public override int Hides{ get{ return 12; } }
		public override int Meat{ get{ return 1; } }

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