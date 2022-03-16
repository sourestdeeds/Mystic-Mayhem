using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class OrangeTblSAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new OrangeTblSAddonDeed();
			}
		}

		[ Constructable ]
		public OrangeTblSAddon()
		{
			AddComponent( new AddonComponent( 4494 ), 0, 0, 0 );

			//AddonComponent ac = null;

		}

		public OrangeTblSAddon( Serial serial ) : base( serial )
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

	public class OrangeTblSAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new OrangeTblSAddon();
			}
		}

		[Constructable]
		public OrangeTblSAddonDeed()
		{
			Name = "Covered Table";
		}

		public OrangeTblSAddonDeed( Serial serial ) : base( serial )
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