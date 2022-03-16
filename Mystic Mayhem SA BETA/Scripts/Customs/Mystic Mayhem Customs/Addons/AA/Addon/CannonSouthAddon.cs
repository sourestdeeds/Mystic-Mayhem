using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class CannonSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CannonSouthAddonDeed();
			}
		}

		[ Constructable ]
		public CannonSouthAddon()
		{
			AddComponent( new AddonComponent( 3730 ), 0, 0, 0 );
			
	
			AddComponent( new AddonComponent( 3731 ), 0, -1, 0 );

			AddComponent( new AddonComponent( 3729 ), 0, 1, 0 );							//AddonComponent ac = null;

		}

		public CannonSouthAddon( Serial serial ) : base( serial )
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

	public class CannonSouthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CannonSouthAddon();
			}
		}

		[Constructable]
		public CannonSouthAddonDeed()
		{
			Name = "Cannon South";
		}

		public CannonSouthAddonDeed( Serial serial ) : base( serial )
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