using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	public class RoninArcher : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public RoninArcher() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SpeechHue = Utility.RandomDyedHue();
			//Title = "a ronin";
			Hue = Utility.RandomSkinHue();
			Name = "Ronin Archer";

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				//Name = NameList.RandomName( "female" );
			}
			else
			{
				Body = 0x190;
				//Name = NameList.RandomName( "male" );
			}

			SetStr( 100, 130 );
			SetDex( 81, 95 );
			SetInt( 71, 95 );

			SetDamage( 6, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 55 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.Anatomy, 66.0, 97.5 );
			SetSkill( SkillName.Archery, 80.1, 90.0 );
			SetSkill( SkillName.MagicResist, 55.0, 57.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 35.0, 57.5 );

			Fame = 6000;
			Karma = -6000;

			int lowHue = GetRandomHue();
	
			AddItem( new NinjaTabi());

			AddItem( new LeatherNinjaBelt());
			Item jack = new LeatherNinjaJacket();
			Item pant = new LeatherNinjaPants();
			jack.Hue = lowHue;
			pant.Hue = lowHue;
			AddItem( jack );
			AddItem( pant );
		//	AddItem( new LeatherNinjaJacket());
		//	AddItem( new LeatherNinjaPants());
		//	AddItem( new LeatherNinjaHood());

			AddItem( new Yumi() );

			HairItemID = Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A );
			HairHue = Utility.RandomNondyedHue();

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		private static int GetRandomHue()
		{
			switch ( Utility.Random( 3 ) )
			{
				default:
				case 0: return Utility.RandomBlueHue();
				case 1: return Utility.RandomGreenHue();
				case 2: return Utility.RandomRedHue();

			}
		}


	//	public override bool AlwaysMurderer{ get{ return true; } }

		public RoninArcher( Serial serial ) : base( serial )
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