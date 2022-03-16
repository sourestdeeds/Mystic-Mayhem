//aa
using System;
using System.Collections;
using Server;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0x1508, 0x151C, 0x151A, 0x1512 )]
	public class DecoArmor : AddonComponent
	{


		[Constructable]
		public DecoArmor() : this( 0x1508 )
		{
		}
		[Constructable]
		public DecoArmor( int itemID ) : base( itemID )
		{
		}

		public DecoArmor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

		}
	}

	public class DecoArmorAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new DecoArmorDeed(); } }

		[Constructable]
		public DecoArmorAddon()
		{
			AddComponent( new DecoArmor( 0x1508 ), 0, 0, 0 );
		}

		public DecoArmorAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class DecoArmorDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new DecoArmorAddon(); } }
		public override int LabelNumber{ get{ return 1025384; } } // decorative armor

		[Constructable]
		public DecoArmorDeed()
		{
		}

		public DecoArmorDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}