using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class RedTblEAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new RedTblEAddonDeed();
			}
		}

		[ Constructable ]
		public RedTblEAddon()
		{
			AddComponent( new AddonComponent( 4493 ), 0, 0, 0 );

			//AddonComponent ac = null;

		}

		public RedTblEAddon( Serial serial ) : base( serial )
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

	public class RedTblEAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new RedTblEAddon();
			}
		}

		[Constructable]
		public RedTblEAddonDeed()
		{
			Name = "Covered Table";
		}

		public RedTblEAddonDeed( Serial serial ) : base( serial )
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