using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class CannonNorthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CannonNorthAddonDeed();
			}
		}

		[ Constructable ]
		public CannonNorthAddon()
		{
			AddComponent( new AddonComponent( 3724 ), 0, 0, 0 );
			
	
			AddComponent( new AddonComponent( 3725 ), 0, -1, 0 );

			AddComponent( new AddonComponent( 3723 ), 0, 1, 0 );							//AddonComponent ac = null;

		}

		public CannonNorthAddon( Serial serial ) : base( serial )
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

	public class CannonNorthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CannonNorthAddon();
			}
		}

		[Constructable]
		public CannonNorthAddonDeed()
		{
			Name = "Cannon North";
		}

		public CannonNorthAddonDeed( Serial serial ) : base( serial )
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