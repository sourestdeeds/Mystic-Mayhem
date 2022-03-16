using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	public class HighExecutioner : BaseCreature
	{
		//public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public HighExecutioner() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SpeechHue = Utility.RandomDyedHue();
			Title = "High Executioner";
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
			SetHits( 1400, 1500 );
			SetStr( 1256, 1300 );
			SetDex( 135, 195 );
			SetInt( 61, 75 );

			SetDamage( 33, 39 );

			SetSkill( SkillName.Anatomy, 66.0, 97.5 );
			SetSkill( SkillName.Macing, 65.0, 87.5 );
			SetSkill( SkillName.MagicResist, 55.0, 77.5 );
			SetSkill( SkillName.Swords, 75.0, 97.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 115.0, 137.5 );

			Fame = 15000;
			Karma = -15000;

			switch ( Utility.Random( 2 ) )
			{
				case 0: AddItem( new Waraji() ); break;
				case 1: AddItem( new NinjaTabi() ); break;
			}

			switch ( Utility.Random( 3 ) )
			{
				case 0: AddItem( new PlateDo() );
					AddItem( new PlateHiroSode() );
					AddItem( new PlateHaidate() );
				//	AddItem( new PlateHeavyJingasa() );
				break;
				case 1: AddItem( new LeatherDo() );
					AddItem( new LeatherHiroSode() );
					AddItem( new LeatherHaidate() );
					AddItem( new LeatherJingasa() );
				break;
				case 2: AddItem( new StuddedDo() );
					AddItem( new StuddedHiroSode() );
					AddItem( new StuddedHaidate() );
				//	AddItem( new StuddedKabuto() );
				break;
			}

			switch ( Utility.Random( 4 ))
			{
				case 0: AddItem( new ExecutionersAxe() ); break;
				case 1: AddItem( new OrnateAxe() ); break;
				case 2: AddItem( new WarCleaver() ); break;
				case 3: AddItem( new RadiantScimitar() ); break;

			}

			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );
			hair.Hue = Utility.RandomNondyedHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );
		}

		public override void OnDeath( Container c )
		{
			c.DropItem( new TigerClaw() );
  
			base.OnDeath( c );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override bool AlwaysMurderer{ get{ return true; } }

		public HighExecutioner( Serial serial ) : base( serial )
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