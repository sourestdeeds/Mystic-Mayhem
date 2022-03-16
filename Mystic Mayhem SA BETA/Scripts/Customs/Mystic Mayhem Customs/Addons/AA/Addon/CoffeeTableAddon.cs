using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class CoffeeTableAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CoffeeTableAddonDeed();
			}
		}

		[ Constructable ]
		public CoffeeTableAddon()
		{
			AddComponent( new AddonComponent( 6422 ), 0, 0, 0 );

			AddComponent( new AddonComponent( 6423 ), 1, 0, 0 );							//AddonComponent ac = null;

		}

		public CoffeeTableAddon( Serial serial ) : base( serial )
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

	public class CoffeeTableAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CoffeeTableAddon();
			}
		}

		[Constructable]
		public CoffeeTableAddonDeed()
		{
			Name = "Coffee Table";
		}

		public CoffeeTableAddonDeed( Serial serial ) : base( serial )
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