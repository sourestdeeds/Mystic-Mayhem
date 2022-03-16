using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a crystal hydra corpse" )]
	public class CrystalHydra : BaseCreature
	{
		[Constructable]
		public CrystalHydra() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a crystal hydra";
			Body = 0x109;
			Hue = 0x47E;
			BaseSoundID = 362;

			SetStr( 804, 827 );
			SetDex( 103, 119 );
			SetInt( 101, 109 );

			SetHits( 1486, 1500 );

			SetDamage( 21, 26 );

			SetDamageType( ResistanceType.Physical, 5 );
			SetDamageType( ResistanceType.Fire, 5 );
			SetDamageType( ResistanceType.Cold, 80 );
			SetDamageType( ResistanceType.Poison, 5 );
			SetDamageType( ResistanceType.Energy, 5 );

			SetResistance( ResistanceType.Physical, 67, 74 );
			SetResistance( ResistanceType.Fire, 20, 29 );
			SetResistance( ResistanceType.Cold, 87, 98 );
			SetResistance( ResistanceType.Poison, 36, 45 );
			SetResistance( ResistanceType.Energy, 80, 100 );

			SetSkill( SkillName.Wrestling, 100.6, 115.1 );
			SetSkill( SkillName.Tactics, 101.7, 108.1 );
			SetSkill( SkillName.MagicResist, 89.9, 99.5 );
			SetSkill( SkillName.Anatomy, 75.2, 79.1 );
		}
		
		public CrystalHydra( Serial serial ) : base( serial )
		{
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosUltraRich, 3 );
		}		
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
			
			if ( Utility.RandomDouble() < 0.4 )
				c.DropItem( new ShatteredCrystals() );				
			
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
		
		#region Breath
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 100; } }		
		public override int BreathEffectHue{ get{ return 0x47E; } }
		public override int BreathEffectSound{ get{ return 0x56D; } }
		public override bool HasBreath{ get{ return true; } } 
		#endregion
		
		public override int Hides{ get{ return 40; } }
		public override int Meat{ get{ return 19; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		
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