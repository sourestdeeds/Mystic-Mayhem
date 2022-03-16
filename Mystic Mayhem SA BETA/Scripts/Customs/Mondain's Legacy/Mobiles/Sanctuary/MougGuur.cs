using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "an ettins corpse" )]
	public class MougGuur : BaseCreature
	{
		[Constructable]
		public MougGuur() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Moug-Guur";
			Body = 18;
			BaseSoundID = 367;
			Hue = 2318;

			SetStr( 236, 265 );
			SetDex( 156, 175 );
			SetInt( 131, 155 );

			SetHits( 1182, 1199 );

			SetDamage( 17, 27 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 65, 70 );
			SetResistance( ResistanceType.Fire, 45, 55 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 45, 55 );
			SetResistance( ResistanceType.Energy, 45, 55 );

			SetSkill( SkillName.MagicResist, 80.1, 110.0 );
			SetSkill( SkillName.Tactics, 100.1, 140.0 );
			SetSkill( SkillName.Wrestling, 100.1, 120.0 );

			Fame = 6000;
			Karma = -6000;

			VirtualArmor = 68;
		}

		public override void GenerateLoot()
		{

			AddLoot( LootPack.Average, 3 );
			//aaAddLoot( LootPack.Potions );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 4; } }

		public MougGuur( Serial serial ) : base( serial )
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