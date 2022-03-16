using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a training cow corpse" )]
	public class TrainingCow : BaseCreature
	{
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }
		[Constructable]
		public TrainingCow() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a training cow";
			Body = Utility.RandomList( 0xD8, 0xE7 );
			Hue = 1153;
			BaseSoundID = 0x78;

		    	SetStr( 5, 10 );
			SetDex( 100 );
			SetInt( 1 );

			SetHits( 990, 999 );

			SetDamage( 1, 1 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 50 );
			SetResistance( ResistanceType.Fire, 10 );
			SetResistance( ResistanceType.Cold, 10 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 10 );

			SetSkill( SkillName.Tactics, 5.1, 10.0 );
			SetSkill( SkillName.Wrestling, 5.1, 10.0 );
			SetSkill( SkillName.Anatomy, 5.1, 10.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 200;

			
		}

		public override int Meat{ get{ return 12; } }
		public override int Hides{ get{ return 8; } } 

		public TrainingCow( Serial serial ) : base( serial )
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
