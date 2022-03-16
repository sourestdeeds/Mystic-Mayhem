using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class GreenTblEAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new GreenTblEAddonDeed();
			}
		}

		[ Constructable ]
		public GreenTblEAddon()
		{
			AddComponent( new AddonComponent( 4497 ), 0, -1, 0 );
			
				AddComponent( new AddonComponent( 4497 ), 0, 1, 0 );
			AddComponent( new AddonComponent( 4498 ), 0, 0, 0 );
						//AddonComponent ac = null;

		}

		public GreenTblEAddon( Serial serial ) : base( serial )
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

	public class GreenTblEAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new GreenTblEAddon();
			}
		}

		[Constructable]
		public GreenTblEAddonDeed()
		{
			Name = "Covered Table";
		}

		public GreenTblEAddonDeed( Serial serial ) : base( serial )
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