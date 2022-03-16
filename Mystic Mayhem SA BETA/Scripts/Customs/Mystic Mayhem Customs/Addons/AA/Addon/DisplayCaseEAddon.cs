using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class DisplayCaseEAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new DisplayCaseEAddonDeed();
			}
		}

		[ Constructable ]
		public DisplayCaseEAddon()
		{
			AddComponent( new AddonComponent( 2824 ), 0, -1, 0 );
			AddComponent( new AddonComponent( 2823 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 2822 ), 0, 1, 0 );	
			AddComponent( new AddonComponent( 2821 ), 0, -1, 1 );
			AddComponent( new AddonComponent( 2820 ), 0, 0, 1 );
			AddComponent( new AddonComponent( 2819 ), 0, 1, 1 );						//AddonComponent ac = null;
		}

		public DisplayCaseEAddon( Serial serial ) : base( serial )
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

	public class DisplayCaseEAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new DisplayCaseEAddon();
			}
		}

		[Constructable]
		public DisplayCaseEAddonDeed()
		{
			Name = "Display Case East";
		}

		public DisplayCaseEAddonDeed( Serial serial ) : base( serial )
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