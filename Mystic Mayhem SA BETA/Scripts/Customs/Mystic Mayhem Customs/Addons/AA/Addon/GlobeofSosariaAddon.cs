using System;
using Server;

namespace Server.Items
{
	public class GlobeofSosariaAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new GlobeofSosariaDeed(); } }

		[Constructable]
		public GlobeofSosariaAddon()
		{
			AddComponent( new AddonComponent( 0x3657 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 0x3658 ), -1, 0, 0 );
			AddComponent( new AddonComponent( 0x3659 ), 0, -1, 0 );
			AddComponent( new AddonComponent( 0x3660 ), 0, 0, 0 );
		}

		public GlobeofSosariaAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class GlobeofSosariaDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new GlobeofSosariaAddon(); } }
		public override int LabelNumber{ get{ return 1076660; } } 

		[Constructable]
		public GlobeofSosariaDeed()
		{
		}

		public GlobeofSosariaDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}