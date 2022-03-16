using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	public class NinjaRogue : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public NinjaRogue() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SpeechHue = Utility.RandomDyedHue();
			Title = "a ninja";
			Hue = Utility.RandomSkinHue();

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "tokuno female" );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "tokuno male" );
			}

			SetHits( 350, 400 );
			SetStr( 126, 150 );
			SetDex( 101, 125 );
			SetInt( 61, 75 );

			SetDamage( 23, 28 );

			SetSkill( SkillName.Anatomy, 66.0, 97.5 );
			SetSkill( SkillName.Fencing, 125.0, 137.5 );
			SetSkill( SkillName.MagicResist, 45.0, 67.5 );
			SetSkill( SkillName.Swords, 125.0, 137.5 );
			SetSkill( SkillName.Tactics, 105.0, 117.5 );
			SetSkill( SkillName.Wrestling, 75.0, 87.5 );

			Fame = 7000;
			Karma = -7000;

			AddItem( new NinjaTabi( Utility.RandomNeutralHue() ) );
			AddItem( new LeatherNinjaJacket());
			AddItem( new LeatherNinjaHood());
			AddItem( new LeatherNinjaBelt());
			AddItem( new LeatherNinjaMitts());
			AddItem( new LeatherNinjaPants());

			switch ( Utility.Random( 7 ))
			{
				case 0: AddItem( new Sai() ); break;
				case 1: AddItem( new Daisho() ); break;
				case 2: AddItem( new Kama() ); break;
				case 3: AddItem( new Bokuto() ); break;
				case 4: AddItem( new Tekagi() ); break;
				case 5: AddItem( new Wakizashi() ); break;
				case 6: AddItem( new Nunchaku() ); break;
			}

			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );
			hair.Hue = Utility.RandomNondyedHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}

		public override bool AlwaysMurderer{ get{ return true; } }

		public NinjaRogue( Serial serial ) : base( serial )
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