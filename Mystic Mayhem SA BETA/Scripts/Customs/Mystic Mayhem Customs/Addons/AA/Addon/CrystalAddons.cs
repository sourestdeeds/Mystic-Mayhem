using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class CrystalTableSAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CrystalTableSAddonDeed();
			}
		}

		[ Constructable ]
		public CrystalTableSAddon()
		{
			AddComponent( new AddonComponent( 13832 ), 0, 0, 0 );
	
			AddComponent( new AddonComponent( 13831 ), 1, 0, 0 );

		}

		public CrystalTableSAddon( Serial serial ) : base( serial )
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

	public class CrystalTableSAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CrystalTableSAddon();
			}
		}

		[Constructable]
		public CrystalTableSAddonDeed()
		{
			Name = "Crystal Table South";
			ItemID = 7961;
			Hue = 91;
		}

		public CrystalTableSAddonDeed( Serial serial ) : base( serial )
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
//***
	public class CrystalTableEAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CrystalTableEAddonDeed();
			}
		}

		[ Constructable ]
		public CrystalTableEAddon()
		{
			AddComponent( new AddonComponent( 13830 ), 0, 0, 0 );
	
			AddComponent( new AddonComponent( 13829 ), 0, 1, 0 );

		}

		public CrystalTableEAddon( Serial serial ) : base( serial )
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

	public class CrystalTableEAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CrystalTableEAddon();
			}
		}

		[Constructable]
		public CrystalTableEAddonDeed()
		{
			Name = "Crystal Table East";
			ItemID = 7961;
			Hue = 91;
		}

		public CrystalTableEAddonDeed( Serial serial ) : base( serial )
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

//***
	public class CrystalThroneEAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CrystalThroneEAddonDeed();
			}
		}

		[ Constructable ]
		public CrystalThroneEAddon()
		{
			AddComponent( new AddonComponent( 13806 ), 0, 0, 0 );
	
		//	AddComponent( new AddonComponent( 13829 ), 0, 1, 0 );

		}

		public CrystalThroneEAddon( Serial serial ) : base( serial )
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

	public class CrystalThroneEAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CrystalThroneEAddon();
			}
		}

		[Constructable]
		public CrystalThroneEAddonDeed()
		{
			Name = "Crystal Throne East";
			ItemID = 7961;
			Hue = 91;
		}

		public CrystalThroneEAddonDeed( Serial serial ) : base( serial )
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

//***
	public class CrystalThroneSAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CrystalThroneSAddonDeed();
			}
		}

		[ Constructable ]
		public CrystalThroneSAddon()
		{
			AddComponent( new AddonComponent( 13805 ), 0, 0, 0 );
	
		//	AddComponent( new AddonComponent( 13829 ), 0, 1, 0 );

		}

		public CrystalThroneSAddon( Serial serial ) : base( serial )
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

	public class CrystalThroneSAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CrystalThroneSAddon();
			}
		}

		[Constructable]
		public CrystalThroneSAddonDeed()
		{
			Name = "Crystal Throne South";
			ItemID = 7961;
			Hue = 91;
		}

		public CrystalThroneSAddonDeed( Serial serial ) : base( serial )
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

//***
	public class CrystalStatue1SAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CrystalStatue1SAddonDeed();
			}
		}

		[ Constructable ]
		public CrystalStatue1SAddon()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 13816 );
			ac.Name = "Crystal Statue 1";
			AddComponent( ac, 0, 0, 0 );
	
		}

		public CrystalStatue1SAddon( Serial serial ) : base( serial )
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

	public class CrystalStatue1SAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CrystalStatue1SAddon();
			}
		}

		[Constructable]
		public CrystalStatue1SAddonDeed()
		{
			Name = "Crystal Statue1 South";
			ItemID = 7961;
			Hue = 91;
		}

		public CrystalStatue1SAddonDeed( Serial serial ) : base( serial )
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

//***
	public class CrystalStatue1EAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CrystalStatue1EAddonDeed();
			}
		}

		[ Constructable ]
		public CrystalStatue1EAddon()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 13817 );
			ac.Name = "Crystal Statue 1";
			AddComponent( ac, 0, 0, 0 );
		}

		public CrystalStatue1EAddon( Serial serial ) : base( serial )
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

	public class CrystalStatue1EAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CrystalStatue1EAddon();
			}
		}

		[Constructable]
		public CrystalStatue1EAddonDeed()
		{
			Name = "Crystal Statue1 East";
			ItemID = 7961;
			Hue = 91;
		}

		public CrystalStatue1EAddonDeed( Serial serial ) : base( serial )
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

//***
	public class CrystalStatue2SAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CrystalStatue2SAddonDeed();
			}
		}

		[ Constructable ]
		public CrystalStatue2SAddon()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 13819 );
			ac.Name = "Crystal Statue 2";
			AddComponent( ac, 0, 0, 0 );
		}

		public CrystalStatue2SAddon( Serial serial ) : base( serial )
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

	public class CrystalStatue2SAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CrystalStatue2SAddon();
			}
		}

		[Constructable]
		public CrystalStatue2SAddonDeed()
		{
			Name = "Crystal Statue2 South";
			ItemID = 7961;
			Hue = 91;
		}

		public CrystalStatue2SAddonDeed( Serial serial ) : base( serial )
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

//***
	public class CrystalStatue2EAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CrystalStatue2EAddonDeed();
			}
		}

		[ Constructable ]
		public CrystalStatue2EAddon()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 13818 );
			ac.Name = "Crystal Statue 2";
			AddComponent( ac, 0, 0, 0 );
		}

		public CrystalStatue2EAddon( Serial serial ) : base( serial )
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

	public class CrystalStatue2EAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CrystalStatue2EAddon();
			}
		}

		[Constructable]
		public CrystalStatue2EAddonDeed()
		{
			Name = "Crystal Statue2 East";
			ItemID = 7961;
			Hue = 91;
		}

		public CrystalStatue2EAddonDeed( Serial serial ) : base( serial )
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

//***
	public class CrystalStatue3SAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CrystalStatue3SAddonDeed();
			}
		}

		[ Constructable ]
		public CrystalStatue3SAddon()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 13821 );
			ac.Name = "Crystal Statue 3";
			AddComponent( ac, 0, 0, 0 );
		}

		public CrystalStatue3SAddon( Serial serial ) : base( serial )
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

	public class CrystalStatue3SAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CrystalStatue3SAddon();
			}
		}

		[Constructable]
		public CrystalStatue3SAddonDeed()
		{
			Name = "Crystal Statue3 South";
			ItemID = 7961;
			Hue = 91;
		}

		public CrystalStatue3SAddonDeed( Serial serial ) : base( serial )
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

//***
	public class CrystalStatue3EAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CrystalStatue3EAddonDeed();
			}
		}

		[ Constructable ]
		public CrystalStatue3EAddon()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 13820 );
			ac.Name = "Crystal Statue 3";
			AddComponent( ac, 0, 0, 0 );
		}

		public CrystalStatue3EAddon( Serial serial ) : base( serial )
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

	public class CrystalStatue3EAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CrystalStatue3EAddon();
			}
		}

		[Constructable]
		public CrystalStatue3EAddonDeed()
		{
			Name = "Crystal Statue3 East";
			ItemID = 7961;
			Hue = 91;
		}

		public CrystalStatue3EAddonDeed( Serial serial ) : base( serial )
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