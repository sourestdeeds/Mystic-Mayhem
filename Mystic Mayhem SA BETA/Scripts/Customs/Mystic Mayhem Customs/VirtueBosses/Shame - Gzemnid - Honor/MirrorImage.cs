using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	
	
	[CorpseName( "The Remains Of Gzemnid" )]
	public class Gzemnid2 : BaseCreature
	{
		public override bool AlwaysMurderer{ get{ return true; } }
				[Constructable]
		public Gzemnid2 () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Gzemnid";
			Title = "the Ancient Beholder";
			Body = 22;
			BaseSoundID = 377;
			Hue = 1194;

			SetStr( 296, 325 );
			SetDex( 86, 105 );
			SetInt( 599, 1000 );

			SetHits( 25, 50 );

			SetDamage( 8, 19 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 62.0, 100.0 );
			SetSkill( SkillName.EvalInt, 90.1, 150.0 );
			SetSkill( SkillName.Magery, 90.1, 150.0 );
			SetSkill( SkillName.MagicResist, 115.1, 130.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 100.0 );

			Fame = 12500;
			Karma = -12500;

			VirtualArmor = 50;
		}

		public override int TreasureMapLevel{ get{ return Core.AOS ? 4 : 0; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
		}

		public Gzemnid2( Serial serial ) : base( serial )
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