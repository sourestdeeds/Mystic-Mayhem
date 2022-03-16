using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class CooperBenchEAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CooperBenchEAddonDeed();
			}
		}

		[ Constructable ]
		public CooperBenchEAddon()
		{
			AddComponent( new AddonComponent( 6650 ), 0, 0, 0 );

			AddComponent( new AddonComponent( 6649 ), 0, 1, 0 );							//AddonComponent ac = null;

		}

		public CooperBenchEAddon( Serial serial ) : base( serial )
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

	public class CooperBenchEAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CooperBenchEAddon();
			}
		}

		[Constructable]
		public CooperBenchEAddonDeed()
		{
			Name = "Cooper Bench";
		}

		public CooperBenchEAddonDeed( Serial serial ) : base( serial )
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