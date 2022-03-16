using System;
using Server;

namespace Server.Items
{
	public class StoneAnvilSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new StoneAnvilSouthDeed(); } }
		public override bool RetainDeedHue{ get{ return true; } }

		[Constructable]
		public StoneAnvilSouthAddon()
		{
			AddComponent( new AnvilComponent( 0x2DD5 ), 0, 0, 0 );
		}

		public StoneAnvilSouthAddon( Serial serial ) : base( serial )
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

	public class StoneAnvilSouthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new StoneAnvilSouthAddon(); } }
		public override int LabelNumber{ get{ return 1072876; } } // stone anvil (south)

		[Constructable]
		public StoneAnvilSouthDeed()
		{
		}

		public StoneAnvilSouthDeed( Serial serial ) : base( serial )
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