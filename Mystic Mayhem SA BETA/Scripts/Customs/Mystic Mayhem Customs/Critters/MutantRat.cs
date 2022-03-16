using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a giant rat corpse" )]

	public class MutantRat : BaseCreature
	{
		[Constructable]
		public MutantRat() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a mutant rat";
			Body = 0xD7;
			BaseSoundID = 0x188;
			Hue = 2130;

			SetStr( 10, 200 );
			SetDex( 10, 200 );
			SetInt( 10, 200 );

			SetHits( 100, 1000 );
			//SetMana( 0 );

			SetDamage( 4, 35 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Cold, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 15, 100 );
			SetResistance( ResistanceType.Fire, 15, 100 );
			SetResistance( ResistanceType.Poison, 15, 100 );
			SetResistance( ResistanceType.Cold, 15, 100 );
			SetResistance( ResistanceType.Energy, 15, 100 );

			SetSkill( SkillName.EvalInt, 25, 150.0 ); // 200
			SetSkill( SkillName.Magery, 25, 150.0 );
			SetSkill( SkillName.Meditation, 25, 150.0 );
			SetSkill( SkillName.Anatomy, 25, 150.0 );
			SetSkill( SkillName.Wrestling, 25, 150.0 );
			SetSkill( SkillName.MagicResist, 25, 100.0 );
			SetSkill( SkillName.Tactics, 25, 150.0 );

			Fame = 1000;
			Karma = -1000;

			VirtualArmor = 18;

			Tamable = false;
			PackGold( 15, 250 );

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}


		public MutantRat(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}