using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class DisplayCaseSAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new DisplayCaseSAddonDeed();
			}
		}

		[ Constructable ]
		public DisplayCaseSAddon()
		{
			AddComponent( new AddonComponent( 2818 ), -1, 0, 0 );
			AddComponent( new AddonComponent( 2817 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 2816 ), 1, 0, 0 );	
			AddComponent( new AddonComponent( 2815 ), -1, 0, 1 );
			AddComponent( new AddonComponent( 2814 ), 0, 0, 1 );
			AddComponent( new AddonComponent( 2813 ), 1, 0, 1 );						//AddonComponent ac = null;
		}

		public DisplayCaseSAddon( Serial serial ) : base( serial )
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

	public class DisplayCaseSAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new DisplayCaseSAddon();
			}
		}

		[Constructable]
		public DisplayCaseSAddonDeed()
		{
			Name = "Display Case South";
		}

		public DisplayCaseSAddonDeed( Serial serial ) : base( serial )
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