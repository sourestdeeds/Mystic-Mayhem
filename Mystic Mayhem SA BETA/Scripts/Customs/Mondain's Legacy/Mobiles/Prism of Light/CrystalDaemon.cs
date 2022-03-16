using System;
using Server;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "a crystal daemon corpse" )] 
	public class CrystalDaemon : BaseCreature 
	{ 		
		[Constructable] 
		public CrystalDaemon() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 			
			Hue = Race.Human.RandomSkinHue();

			Body = 0x310;
			Hue = 0x3E8;
			Name = "a crystal daemon";
			BaseSoundID = 0x47D;
					
			SetStr( 142, 190 );
			SetDex( 128, 145 );
			SetInt( 801, 850 );
			
			SetHits( 202, 215 );
			SetStam( 128, 145 );
			SetMana( 801, 850 );

			SetDamage( 16, 20 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Cold, 40 );
			SetDamageType( ResistanceType.Energy, 60 );

			SetResistance( ResistanceType.Physical, 20, 35 );
			SetResistance( ResistanceType.Fire, 6, 18 );
			SetResistance( ResistanceType.Cold, 60, 80 );
			SetResistance( ResistanceType.Poison, 31, 40 );
			SetResistance( ResistanceType.Energy, 65, 74 );

			SetSkill( SkillName.Wrestling, 63.1, 79.6 );	
			SetSkill( SkillName.Tactics, 71.2, 79.6 );
			SetSkill( SkillName.MagicResist, 100.4, 108.4 );
			SetSkill( SkillName.Magery, 120.9, 128.5 );
			SetSkill( SkillName.EvalInt, 101.5, 108.7 );
			SetSkill( SkillName.Meditation, 100.2, 109.1 );
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosFilthyRich, 2 );
		}

		public CrystalDaemon( Serial serial ) : base( serial )
		{
		}		
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );	
						
			if ( Utility.RandomDouble() < 0.4 )
				c.DropItem( new ScatteredCrystals() );
				
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