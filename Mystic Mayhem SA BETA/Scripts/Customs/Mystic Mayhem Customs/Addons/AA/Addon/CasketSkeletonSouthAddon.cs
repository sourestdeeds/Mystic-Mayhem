using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class CasketSkeletonSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CasketSkeletonSouthAddonDeed();
			}
		}

		[ Constructable ]
		public CasketSkeletonSouthAddon()
		{
			AddComponent( new AddonComponent( 7501 ), 0, 0, 0 );
			
	
			AddComponent( new AddonComponent( 7500 ), 1, 0, 0 );

			AddComponent( new AddonComponent( 7502 ), -1, 0, 0 );
			AddComponent( new AddonComponent( 7211 ), -2, -1, 0 );
			AddComponent( new AddonComponent( 7446 ), -1, -1, 0 );
			AddComponent( new AddonComponent( 7447 ), 0, -1, 0 );
			AddComponent( new AddonComponent( 7505 ), 1, -1, 0 );

		}

		public CasketSkeletonSouthAddon( Serial serial ) : base( serial )
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

	public class CasketSkeletonSouthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CasketSkeletonSouthAddon();
			}
		}

		[Constructable]
		public CasketSkeletonSouthAddonDeed()
		{
			Name = "Casket South";
		}

		public CasketSkeletonSouthAddonDeed( Serial serial ) : base( serial )
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