using System;
using Server;

namespace Server.Items
{
	public class TotalManaRefreshPotion : BaseManaRefreshPotion
	{
		public override double Refresh{ get{ return 1.0; } }

		/*[Constructable]
		public TotalManaRefreshPotion() : this( 1 )
		{
		}*/

		[Constructable]
		public TotalManaRefreshPotion(/*int amount*/) : base( PotionEffect.TotalManaRefresh/*, amount*/ )
		{
			Name = "Total Mana Potion";
			Hue = 1078;
		}

		public TotalManaRefreshPotion( Serial serial ) : base( serial )
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
