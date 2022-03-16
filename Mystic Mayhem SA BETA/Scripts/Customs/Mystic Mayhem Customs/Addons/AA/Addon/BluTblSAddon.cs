using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BluTblSAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new BluTblSAddonDeed();
			}
		}

		[ Constructable ]
		public BluTblSAddon()
		{
			AddComponent( new AddonComponent( 4492 ), 0, 0, 0 );

			//AddonComponent ac = null;

		}

		public BluTblSAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class BluTblSAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new BluTblSAddon();
			}
		}

		[Constructable]
		public BluTblSAddonDeed()
		{
			Name = "Blue Covered Table";
		}

		public BluTblSAddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}