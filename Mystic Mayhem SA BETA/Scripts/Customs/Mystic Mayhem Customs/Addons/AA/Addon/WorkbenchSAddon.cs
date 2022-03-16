using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class WorkbenchSAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new WorkbenchSAddonDeed();
			}
		}

		[ Constructable ]
		public WorkbenchSAddon()
		{
			AddComponent( new AddonComponent( 6647 ), -1, 0, 0 );
							AddComponent( new AddonComponent( 6645 ), 1, 0, 0 );
							AddComponent( new AddonComponent( 6646 ), 0, 0, 0 );
				
		}

		public WorkbenchSAddon( Serial serial ) : base( serial )
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

	public class WorkbenchSAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new WorkbenchSAddon();
			}
		}

		[Constructable]
		public WorkbenchSAddonDeed()
		{
			Name = "Workbench";
		}

		public WorkbenchSAddonDeed( Serial serial ) : base( serial )
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