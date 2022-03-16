using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a master mikael corpse" )]
	public class MasterMikael : BaseCreature
	{
		[Constructable]
		public MasterMikael() : base( AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.015, 0.075 )
		{
			Name = "a master mikael";
			Hue = 0x8FD;
			Body = 0x94;
			BaseSoundID = 0x1C3;

			SetStr( 93, 122 );
			SetDex( 91, 100 );
			SetInt( 252, 271 );

			SetHits( 789, 1014 );

			SetDamage( 11, 19 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 59 );
			SetResistance( ResistanceType.Fire, 40, 46 );
			SetResistance( ResistanceType.Cold, 72, 80 );
			SetResistance( ResistanceType.Poison, 44, 49 );
			SetResistance( ResistanceType.Energy, 50, 57 );

			SetSkill( SkillName.Wrestling, 80.1, 87.2 );
			SetSkill( SkillName.Tactics, 79.0, 90.9 );
			SetSkill( SkillName.MagicResist, 90.3, 106.9 );
			SetSkill( SkillName.Magery, 103.8, 108.0 );
			SetSkill( SkillName.EvalInt, 96.1, 105.3 );
			SetSkill( SkillName.Necromancy, 103.8, 108.0 );
			SetSkill( SkillName.SpiritSpeak, 96.1, 105.3 );
			
			if ( Utility.RandomBool() )
				PackNecroScroll( Utility.RandomMinMax( 5, 9 ) );
			else
				PackScroll( 4, 7 );
				
			PackReg( 3 );
			PackNecroReg( 1, 10 );
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosUltraRich, 3 );
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
			
			if ( Utility.RandomDouble() < 0.15 )
				c.DropItem( new DisintegratingThesisNotes() );
							
			if ( Utility.RandomDouble() < 0.1 )
				c.DropItem( new ParrotItem() );
				
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
	
		public MasterMikael( Serial serial ) : base( serial )
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

