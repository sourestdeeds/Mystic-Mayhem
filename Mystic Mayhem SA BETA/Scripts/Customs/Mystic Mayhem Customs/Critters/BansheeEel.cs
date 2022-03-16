using System;
using Server.Mobiles;
using Server.Factions;

namespace Server.Mobiles
{

	[CorpseName("an eel corpse")]
	public class BansheeEel : BaseCreature
	{
	//	public HideType ht = HideType.Frost;
		[Constructable]
		public BansheeEel() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 92;
			Name = "a banshee eel";
			BaseSoundID = 0x482;
			CanSwim = true;
			CantWalk = false;
			Hue = 1175;

			SetStr( 161, 360 );
			SetDex( 151, 300 );
			SetInt( 41, 60 );

			SetHits( 297, 316 );

			SetDamage( 15, 21 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Poison, 25 );

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 65, 70 );
			SetResistance( ResistanceType.Cold, 35, 40 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 65, 70 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.Meditation, 65.4, 75.0 );
			SetSkill( SkillName.MagicResist, 90.1, 100.0 );
			SetSkill( SkillName.Poisoning, 90.1, 100.0 );
			SetSkill( SkillName.Tactics, 80.1, 90.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 9000;
			Karma = -9000;

			VirtualArmor = 40;


		//	switch(Utility.Random(2))
		//	{
		//		case 0: ht = HideType.Vulcon; break;
		//		case 1: ht = HideType.Aquas; break;
		//	}

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems, 2 );
		}


	//	public override int Hides{ get{ return ( Utility.RandomBool() ? 1 : 2 ); } }
	//	public override HideType HideType{ get{ return ht; } }
		public override int Meat{ get{ return 4; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }

		public BansheeEel(Serial serial) : base(serial)
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