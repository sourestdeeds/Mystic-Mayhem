using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class CannonWestAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CannonWestAddonDeed();
			}
		}

		[ Constructable ]
		public CannonWestAddon()
		{
			AddComponent( new AddonComponent( 3727 ), 0, 0, 0 );
			
	
			AddComponent( new AddonComponent( 3726 ), -1, 0, 0 );

			AddComponent( new AddonComponent( 3728 ), 1, 0, 0 );							//AddonComponent ac = null;

		}

		public CannonWestAddon( Serial serial ) : base( serial )
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

	public class CannonWestAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CannonWestAddon();
			}
		}

		[Constructable]
		public CannonWestAddonDeed()
		{
			Name = "Cannon West";
		}

		public CannonWestAddonDeed( Serial serial ) : base( serial )
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