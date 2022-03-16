using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class CooperBenchSAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CooperBenchSAddonDeed();
			}
		}

		[ Constructable ]
		public CooperBenchSAddon()
		{
			AddComponent( new AddonComponent( 6651 ), 0, 0, 0 );

			AddComponent( new AddonComponent( 6652 ), 1, 0, 0 );							//AddonComponent ac = null;

		}

		public CooperBenchSAddon( Serial serial ) : base( serial )
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

	public class CooperBenchSAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CooperBenchSAddon();
			}
		}

		[Constructable]
		public CooperBenchSAddonDeed()
		{
			Name = "Cooper Bench";
		}

		public CooperBenchSAddonDeed( Serial serial ) : base( serial )
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