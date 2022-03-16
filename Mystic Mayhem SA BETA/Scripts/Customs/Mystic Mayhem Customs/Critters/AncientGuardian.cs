using System;
using Server;
using Server.Items;
using Server.SpellCrafting.Items;

namespace Server.Mobiles
{
	[CorpseName( "an ancient corpse" )]
	public class AncientGuardian : BaseCreature
	{
		[Constructable]
		public AncientGuardian() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an ancient guardian";
			Body = 76;
			Hue = 2413;
			BaseSoundID = 609;

			SetStr( 536, 785 );
			SetDex( 126, 145 );
			SetInt( 281, 305 );

			SetHits( 1322, 1351 );

			SetDamage( 20, 35 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 35, 45 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.EvalInt, 85.1, 100.0 );
			SetSkill( SkillName.Magery, 85.1, 100.0 );
			SetSkill( SkillName.MagicResist, 100.2, 130.0 );
			SetSkill( SkillName.Tactics, 70.1, 90.0 );
			SetSkill( SkillName.Wrestling, 40.1, 50.0 );

			Fame = 11500;
			Karma = -11500;

			VirtualArmor = 40;
			//if ( Utility.RandomDouble() <= 0.5 )
			PackItem( new AncientMortarPestle() );
			if ( Utility.RandomDouble() <= 0.10 )
			PackItem( new MagicJewel( 1 ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.MedScrolls );
		}

		public override int Meat{ get{ return 4; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public AncientGuardian( Serial serial ) : base( serial )
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