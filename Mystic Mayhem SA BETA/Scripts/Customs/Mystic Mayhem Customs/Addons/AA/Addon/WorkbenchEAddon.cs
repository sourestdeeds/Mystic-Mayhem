using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class WorkbenchEAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new WorkbenchEAddonDeed();
			}
		}

		[ Constructable ]
		public WorkbenchEAddon()
		{
			AddComponent( new AddonComponent( 6641 ), 0, 1, 0 );
							AddComponent( new AddonComponent( 6642 ), 0, 0, 0 );
							AddComponent( new AddonComponent( 6643 ), 0, -1, 0 );
				
		}

		public WorkbenchEAddon( Serial serial ) : base( serial )
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

	public class WorkbenchEAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new WorkbenchEAddon();
			}
		}

		[Constructable]
		public WorkbenchEAddonDeed()
		{
			Name = "Workbench";
		}

		public WorkbenchEAddonDeed( Serial serial ) : base( serial )
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