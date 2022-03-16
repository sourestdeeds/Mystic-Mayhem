using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class GreenTblSAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new GreenTblSAddonDeed();
			}
		}

		[ Constructable ]
		public GreenTblSAddon()
		{
			AddComponent( new AddonComponent( 4495 ), -1, 0, 0 );
			
				AddComponent( new AddonComponent( 4495 ), 1, 0, 0 );
			AddComponent( new AddonComponent( 4496 ), 0, 0, 0 );
						//AddonComponent ac = null;

		}

		public GreenTblSAddon( Serial serial ) : base( serial )
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

	public class GreenTblSAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new GreenTblSAddon();
			}
		}

		[Constructable]
		public GreenTblSAddonDeed()
		{
			Name = "Covered Table";
		}

		public GreenTblSAddonDeed( Serial serial ) : base( serial )
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