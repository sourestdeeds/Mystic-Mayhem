using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class LgTreasurePileAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new LgTreasurePileAddonDeed();
			}
		}

		[ Constructable ]
		public LgTreasurePileAddon()
		{
			AddComponent( new AddonComponent( 7005 ), 0, 0, 0 );
							AddComponent( new AddonComponent( 6998 ), -1, 0, 0 );
		
					AddComponent( new AddonComponent( 6997 ), -1, 1, 0 );
		
					AddComponent( new AddonComponent( 7003 ), 2, 0, 0 );
		
					AddComponent( new AddonComponent( 7004 ), 1, 0, 0 );
		
					AddComponent( new AddonComponent( 6995 ), 1, 1, 0 );
							AddComponent( new AddonComponent( 7000 ), 0, -1, 0 );
		
					AddComponent( new AddonComponent( 7001 ), 1, -1, 0 );
		

					AddComponent( new AddonComponent( 6999 ), -1, -1, 0 );
		
					AddComponent( new AddonComponent( 7002 ), 2, -1, 0 );
			
	
			AddComponent( new AddonComponent( 6996 ), 0, 1, 0 );
							//AddonComponent ac = null;

			//ac = new AddonComponent( 7002 );
			//AddComponent( ac, 2, -1, 0 );

		}

		public LgTreasurePileAddon( Serial serial ) : base( serial )
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

	public class LgTreasurePileAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new LgTreasurePileAddon();
			}
		}

		[Constructable]
		public LgTreasurePileAddonDeed()
		{
			Name = "LgTreasurePile";
		}

		public LgTreasurePileAddonDeed( Serial serial ) : base( serial )
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