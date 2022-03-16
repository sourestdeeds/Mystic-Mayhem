//aa
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class DresserSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new DresserSouthAddonDeed();
			}
		}
		public override bool RetainDeedHue{ get{ return true; } }

		[ Constructable ]
		public DresserSouthAddon() : this( 0 )
		{
		}

		[ Constructable ]
		public DresserSouthAddon( int hue )
		{
			AddComponent( new AddonComponent( 2620 ), 0, 0, 0 );
						AddComponent( new AddonComponent( 2621 ), -1, 0, 0 );
						Hue = hue;
		}

		public DresserSouthAddon( Serial serial ) : base( serial )
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

	public class DresserSouthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new DresserSouthAddon( this.Hue );
			}
		}

		[Constructable]
		public DresserSouthAddonDeed()
		{
			Name = "Dresser South";
		}

		public DresserSouthAddonDeed( Serial serial ) : base( serial )
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