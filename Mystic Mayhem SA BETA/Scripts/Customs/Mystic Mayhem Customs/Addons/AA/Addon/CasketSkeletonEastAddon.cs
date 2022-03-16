using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class CasketSkeletonEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CasketSkeletonEastAddonDeed();
			}
		}

		[ Constructable ]
		public CasketSkeletonEastAddon()
		{
			AddComponent( new AddonComponent( 7533 ), 0, 0, 0 );
			
	
			AddComponent( new AddonComponent( 7532 ), 0, 1, 0 );

			AddComponent( new AddonComponent( 7534 ), 0, -1, 0 );
			AddComponent( new AddonComponent( 7224 ), -1, -2, 0 );
			AddComponent( new AddonComponent( 7535 ), -1, -1, 0 );
			AddComponent( new AddonComponent( 7536 ), -1, 0, 0 );
			AddComponent( new AddonComponent( 7537 ), -1, 1, 0 );

		}

		public CasketSkeletonEastAddon( Serial serial ) : base( serial )
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

	public class CasketSkeletonEastAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CasketSkeletonEastAddon();
			}
		}

		[Constructable]
		public CasketSkeletonEastAddonDeed()
		{
			Name = "Casket East";
		}

		public CasketSkeletonEastAddonDeed( Serial serial ) : base( serial )
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