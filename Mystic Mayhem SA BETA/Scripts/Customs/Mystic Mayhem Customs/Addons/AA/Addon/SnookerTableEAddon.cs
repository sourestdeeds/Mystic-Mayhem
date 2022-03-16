using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class Felt : AddonComponent
    {



        [Constructable]
        public Felt() : base(2738)
        {
            Weight = 1.0;
            Name = "felt";
		Hue = 372;
        }

        public Felt(Serial serial) : base( serial )
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
	public class SnookerTableEAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new SnookerTableEAddonDeed();
			}
		}

		[ Constructable ]
		public SnookerTableEAddon()
		{
			AddComponent( new AddonComponent( 2869 ), 0, 2, 0 );

			AddComponent( new Felt( ), 0, 2, 6 );

			AddComponent( new AddonComponent( 925 ), 0, 2, 5 );

			AddComponent( new AddonComponent( 926 ), -1, 1, 5 );

			AddComponent( new AddonComponent( 926 ), -1, 0, 5 );

			AddComponent( new AddonComponent( 927 ), -1, -1, 5 );


			AddComponent( new AddonComponent( 2869 ), 1, 1, 0 );

			AddComponent( new Felt( ), 1, 1, 6 );

			AddComponent( new AddonComponent( 926 ), 1, 1, 5 );

			AddComponent( new Felt( ), 1, 0, 6 );

			AddComponent( new AddonComponent( 2869 ), 1, 0, 0 );

			AddComponent( new AddonComponent( 926 ), 1, 0, 5 );


			AddComponent( new AddonComponent( 925 ), 1, -1, 5 );
			AddComponent( new AddonComponent( 2869 ), 1, 2, 0 );

			AddComponent( new Felt( ), 1, 2, 6 );

			AddComponent( new AddonComponent( 924 ), 1, 2, 5 );

			AddComponent( new AddonComponent( 926 ), -1, 2, 5 );

			AddComponent( new AddonComponent( 2869 ), 0, 0, 0 );

			AddComponent( new Felt( ), 0, 0, 6 );

			AddComponent( new AddonComponent( 2869 ), 0, 1, 0 );

			AddComponent( new Felt( ), 0, 1, 6 );

			AddComponent( new AddonComponent( 925 ), 0, -1, 5 );

			//AddonComponent ac = null;

		}

		public SnookerTableEAddon( Serial serial ) : base( serial )
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

	public class SnookerTableEAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new SnookerTableEAddon();
			}
		}

		[Constructable]
		public SnookerTableEAddonDeed()
		{
			Name = "SnookerTableE";
		}

		public SnookerTableEAddonDeed( Serial serial ) : base( serial )
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