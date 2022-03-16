//aa
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class DresserEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new DresserEastAddonDeed();
			}
		}
		public override bool RetainDeedHue{ get{ return true; } }

		[ Constructable ]
		public DresserEastAddon() : this( 0 )
		{
		}

		[ Constructable ]
		public DresserEastAddon( int hue )
		{
			AddComponent( new AddonComponent( 2628 ), 0, 0, 0 );

			AddComponent( new AddonComponent( 2629 ), 0, -1, 0 );

			Hue = hue;				
		}

		public DresserEastAddon( Serial serial ) : base( serial )
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

	public class DresserEastAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new DresserEastAddon( this.Hue );
			}
		}

		[Constructable]
		public DresserEastAddonDeed()
		{
			Name = "Dresser East";
		}

		public DresserEastAddonDeed( Serial serial ) : base( serial )
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