using System;
using Server;

namespace Server.Items
{
	public class ManaPotion : BaseManaRefreshPotion
	{
		public override double Refresh{ get{ return 0.25; } }

		/*[Constructable]
		public ManaPotion() : this( 1 )
		{
		}*/

		[Constructable]
		public ManaPotion(/*int amount*/) : base( PotionEffect.Mana)
		{
			Name = "Mana Potion";
			Hue = 1078;
		}

		public ManaPotion( Serial serial ) : base( serial )
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
