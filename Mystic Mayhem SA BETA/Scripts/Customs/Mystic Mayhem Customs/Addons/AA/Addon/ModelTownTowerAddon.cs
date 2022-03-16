using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ModelTownTowerAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new ModelTownTowerAddonDeed();
			}
		}

		[ Constructable ]
		public ModelTownTowerAddon()
		{
			AddComponent( new AddonComponent( 8982 ), 0, 0, 0 );
			
	
			AddComponent( new AddonComponent( 8981 ), -1, 0, 0 );

			AddComponent( new AddonComponent( 8980 ), 0, -1, 0 );
			AddComponent( new AddonComponent( 8979 ), -1, -1, 0 );


		}

		public ModelTownTowerAddon( Serial serial ) : base( serial )
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

	public class ModelTownTowerAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new ModelTownTowerAddon();
			}
		}

		[Constructable]
		public ModelTownTowerAddonDeed()
		{
			Name = "Model Town Tower";
		}

		public ModelTownTowerAddonDeed( Serial serial ) : base( serial )
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