using System;

namespace Server.Items
{
	public class TurqouiseRing : GoldRing
	{
		public override int LabelNumber{ get{ return 1073460; } } // turquoise ring

		[Constructable]
		public TurqouiseRing() : base()
		{
			Weight = 1.0;
			
			Resistances.Cold = 15;
			
			BaseRunicTool.ApplyAttributesTo( this, Utility.RandomMinMax( 3, 5 ), 25, 100 );
			
			if ( Utility.Random( 100 ) < 10 )
				Attributes.WeaponSpeed += 25;	
			else
				Attributes.WeaponDamage += 25;
		}

		public TurqouiseRing( Serial serial ) : base( serial )
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
