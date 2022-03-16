using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BluRedTblEAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new BluRedTblEAddonDeed();
			}
		}

		[ Constructable ]
		public BluRedTblEAddon()
		{
			AddComponent( new AddonComponent( 5737 ), 0, -1, 0 );
			AddComponent( new AddonComponent( 5736 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 5735 ), 0, 1, 0 );							//AddonComponent ac = null;
		}

		public BluRedTblEAddon( Serial serial ) : base( serial )
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

	public class BluRedTblEAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new BluRedTblEAddon();
			}
		}

		[Constructable]
		public BluRedTblEAddonDeed()
		{
			Name = "Blue Red Covered Table";
		}

		public BluRedTblEAddonDeed( Serial serial ) : base( serial )
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