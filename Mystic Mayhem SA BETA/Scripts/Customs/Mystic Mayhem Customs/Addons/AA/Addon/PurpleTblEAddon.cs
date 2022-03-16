using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class PurpleTblEAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new PurpleTblEAddonDeed();
			}
		}

		[ Constructable ]
		public PurpleTblEAddon()
		{
			AddComponent( new AddonComponent( 4491 ), 0, 0, 0 );

			//AddonComponent ac = null;

		}

		public PurpleTblEAddon( Serial serial ) : base( serial )
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

	public class PurpleTblEAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new PurpleTblEAddon();
			}
		}

		[Constructable]
		public PurpleTblEAddonDeed()
		{
			Name = "Covered Table";
		}

		public PurpleTblEAddonDeed( Serial serial ) : base( serial )
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