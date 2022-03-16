//ST
using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a black order master corpse" )] 
	public class TigersClawMaster : BasePeerless
	{
		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool ShowFameTitle{ get{ return false; } }

		[Constructable]
		public TigersClawMaster()  : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Black Order Master Thief";
			Title = "of the Tiger's Claw Sect";
			Female = Utility.RandomBool();
			Race = Race.Human;
			Hue = Race.RandomSkinHue();
			HairItemID = Race.RandomHair( Female );
			HairHue = Race.RandomHairHue();
			Race.RandomFacialHair( this );
			
			AddItem( new Wakizashi() );
			AddItem( new FancyShirt( 0x51D ) );
			AddItem( new StuddedMempo() );
			AddItem( new JinBaori( 0x69 ) );
			
			Item item;
			
			item = new StuddedGloves();
			item.Hue = 0x69;
			AddItem( item );
			
			item = new LeatherNinjaPants();
			item.Hue = 0x51D;
			AddItem( item );			
			
			item = new LightPlateJingasa();
			item.Hue = 0x51D;
			AddItem( item );
				
			// TODO quest items

			SetStr( 225, 275 );
			SetDex( 175, 275 );
			SetInt( 85, 105 );

			SetHits( 500, 1000 );

			SetDamage( 14, 22 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 60 );
			SetResistance( ResistanceType.Fire, 45, 65 );
			SetResistance( ResistanceType.Cold, 25, 45 );
			SetResistance( ResistanceType.Poison, 40, 60 );
			SetResistance( ResistanceType.Energy, 40, 65 );
                  
			SetSkill( SkillName.MagicResist, 80.0, 100.0 );
			SetSkill( SkillName.Tactics, 115.0, 130.0 );
			SetSkill( SkillName.Wrestling, 95.0, 120.0 );
			SetSkill( SkillName.Anatomy, 105.0, 120.0 );
			SetSkill( SkillName.Fencing, 78.0, 100.0 );
			SetSkill( SkillName.Swords, 90.1, 105.0 );
			SetSkill( SkillName.Ninjitsu, 90.0, 120.0 );
			SetSkill( SkillName.Hiding, 100.0, 120.0 );
			SetSkill( SkillName.Stealth, 100.0, 120.0 );

			Fame = 5000;
			Karma = -5000;

			VirtualArmor = 58;
		}

		public TigersClawMaster( Serial serial ) : base( serial )
		{
		}
		
		private bool m_SpawnedHelpers;
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosFilthyRich, 2 ); //ST 6
		}		
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );	
			
			if ( Utility.RandomDouble() < 0.2 )
				c.DropItem( new TigerClawKey() );
				
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
		
		#region Spawn Helpers
		public override bool CanSpawnHelpers{ get{ return true; } }
		public override int MaxHelpersWaves{ get{ return 3; } }

		public override bool CanSpawnWave()
		{
			if ( Hits > 1100 )
				m_SpawnedHelpers = false;

			return !m_SpawnedHelpers && Hits < 500;
		}

		public override void SpawnHelpers()
		{
			m_SpawnedHelpers = true;

			for ( int i = 0; i < 10; i++ )
			{
				switch ( Utility.Random( 3 ) )
				{
					case 0: SpawnHelper( new DragonsFlameMage(), 10 ); break;
					case 1: SpawnHelper( new SerpentsFangAssassin(), 10 ); break;
					case 2: SpawnHelper( new TigersClawThief(), 10 ); break;
				}
			}
		}
		#endregion
		
		public virtual Point3D GetSpawnPosition( int range )
		{
			return GetSpawnPosition( Location, Map, range );
		}
		
		public static Point3D GetSpawnPosition( Point3D from, Map map, int range )
		{
			if ( map == null )
				return from;
				
			for ( int i = 0; i < 10; i ++ )
			{
				int x = from.X + Utility.Random( range );
				int y = from.Y + Utility.Random( range );
				int z = map.GetAverageZ( x, y );
				
				if ( Utility.RandomBool() )
					x *= -1;
					
				if ( Utility.RandomBool() )
					y *= -1;
					
				Point3D p = new Point3D( x, y, from.Z );
				
				if ( map.CanSpawnMobile( p ) && map.LineOfSight( from, p ) )
					return p;
				
				p = new Point3D( x, y, z );
					
				if ( map.CanSpawnMobile( p ) && map.LineOfSight( from, p ) )
					return p;
			}
			
			return from;
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
