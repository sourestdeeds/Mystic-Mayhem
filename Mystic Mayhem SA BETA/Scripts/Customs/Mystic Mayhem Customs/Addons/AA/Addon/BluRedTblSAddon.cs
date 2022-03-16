using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BluRedTblSAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new BluRedTblSAddonDeed();
			}
		}

		[ Constructable ]
		public BluRedTblSAddon()
		{
			AddComponent( new AddonComponent( 5738 ), -1, 0, 0 );
			
	
			AddComponent( new AddonComponent( 5739 ), 0, 0, 0 );

			AddComponent( new AddonComponent( 5740 ), 1, 0, 0 );							//AddonComponent ac = null;

		}

		public BluRedTblSAddon( Serial serial ) : base( serial )
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

	public class BluRedTblSAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new BluRedTblSAddon();
			}
		}

		[Constructable]
		public BluRedTblSAddonDeed()
		{
			Name = "Blue Red Covered Table";
		}

		public BluRedTblSAddonDeed( Serial serial ) : base( serial )
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