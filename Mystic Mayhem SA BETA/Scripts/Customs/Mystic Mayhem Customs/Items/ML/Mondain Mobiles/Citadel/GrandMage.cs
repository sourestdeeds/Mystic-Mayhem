using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	public class GrandMage : BaseCreature
	{
		//public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public GrandMage() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SpeechHue = Utility.RandomDyedHue();
			Title = "Grand Mage";
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

			SetHits( 1700, 1850 );
			SetStr( 226, 250 );
			SetDex( 81, 95 );
			SetInt( 231, 245 );

			SetDamage( 28, 33 );

			SetSkill( SkillName.Anatomy, 96.0, 107.5 );
			SetSkill( SkillName.Meditation, 95.0, 107.5 );
			SetSkill( SkillName.MagicResist, 85.0, 97.5 );
			SetSkill( SkillName.EvalInt, 95.0, 107.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 75.0, 87.5 );
			SetSkill( SkillName.Magery, 120.1, 140.0 );

			SetResistance( ResistanceType.Physical, 90 );
			SetResistance( ResistanceType.Fire, 75 );
			SetResistance( ResistanceType.Cold, 85 );
			SetResistance( ResistanceType.Poison, 85 );
			SetResistance( ResistanceType.Energy, 75 );

			Fame = 15000;
			Karma = -15000;

		/*	AddItem( new NinjaTabi( Utility.RandomNeutralHue() ) );
			AddItem( new LeatherNinjaJacket());
			AddItem( new LeatherNinjaHood());
			AddItem( new LeatherNinjaBelt());
			AddItem( new LeatherNinjaMitts());
			AddItem( new LeatherNinjaPants()); */
			AddItem( new Kamishimo(Utility.Random(1150, 25) ) );

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

		public override void OnDeath( Container c )
		{
  			c.DropItem( new SerpentFang() );
			base.OnDeath( c );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}

		public override bool AlwaysMurderer{ get{ return true; } }

		public GrandMage( Serial serial ) : base( serial )
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