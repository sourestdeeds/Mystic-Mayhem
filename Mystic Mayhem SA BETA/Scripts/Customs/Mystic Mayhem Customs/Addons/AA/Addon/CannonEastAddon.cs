using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class CannonEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CannonEastAddonDeed();
			}
		}

		[ Constructable ]
		public CannonEastAddon()
		{
			AddComponent( new AddonComponent( 3733 ), 0, 0, 0 );
			
	
			AddComponent( new AddonComponent( 3732 ), -1, 0, 0 );

			AddComponent( new AddonComponent( 3734 ), 1, 0, 0 );							//AddonComponent ac = null;

		}

		public CannonEastAddon( Serial serial ) : base( serial )
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

	public class CannonEastAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CannonEastAddon();
			}
		}

		[Constructable]
		public CannonEastAddonDeed()
		{
			Name = "Cannon East";
		}

		public CannonEastAddonDeed( Serial serial ) : base( serial )
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