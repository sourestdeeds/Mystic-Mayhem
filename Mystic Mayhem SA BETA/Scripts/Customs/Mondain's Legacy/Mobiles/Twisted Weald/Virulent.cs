using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a dread spider corpse" )]
	public class Virulent : BaseCreature
	{
		[Constructable]
		public Virulent () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Virulent";
			Body = 11;
			Hue = 0x8FF;
			BaseSoundID = 1170;

			SetStr( 207, 252 );
			SetDex( 156, 194 );
			SetInt( 346, 398 );

			SetHits( 616, 740 );
			SetStam( 156, 194 );
			SetMana( 346, 398 );

			SetDamage( 15, 25 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Poison, 80 );

			SetResistance( ResistanceType.Physical, 60, 68 );
			SetResistance( ResistanceType.Fire, 40, 49 );
			SetResistance( ResistanceType.Cold, 41, 50 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 40, 49 );

			SetSkill( SkillName.Wrestling, 92.8, 111.7 );
			SetSkill( SkillName.Tactics, 91.6, 107.4 );
			SetSkill( SkillName.MagicResist, 78.1, 93.3 );
			SetSkill( SkillName.Poisoning, 120.0 );
			SetSkill( SkillName.Magery, 104.2, 119.8 );
			SetSkill( SkillName.EvalInt, 102.8, 116.8 );
			
			PackItem( new SpidersSilk( 8 ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosUltraRich, 4 );
		}		
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
			
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
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.MortalStrike;
		}

		public Virulent( Serial serial ) : base( serial )
		{
		}
            public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
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
