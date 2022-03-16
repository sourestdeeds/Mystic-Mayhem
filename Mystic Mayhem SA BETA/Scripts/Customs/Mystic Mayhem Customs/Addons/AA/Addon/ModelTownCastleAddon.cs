using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ModelTownCastleAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new ModelTownCastleAddonDeed();
			}
		}

		[ Constructable ]
		public ModelTownCastleAddon()
		{
			AddComponent( new AddonComponent( 8984 ), 0, 0, 0 );
			
	
			AddComponent( new AddonComponent( 8983 ), -1, 0, 0 );

			AddComponent( new AddonComponent( 8985 ), 0, -1, 0 );

		}

		public ModelTownCastleAddon( Serial serial ) : base( serial )
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

	public class ModelTownCastleAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new ModelTownCastleAddon();
			}
		}

		[Constructable]
		public ModelTownCastleAddonDeed()
		{
			Name = "Model Town Castle";
		}

		public ModelTownCastleAddonDeed( Serial serial ) : base( serial )
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