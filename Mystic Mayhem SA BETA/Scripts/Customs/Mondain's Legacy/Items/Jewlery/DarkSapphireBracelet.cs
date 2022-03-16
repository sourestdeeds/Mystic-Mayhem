using System;

namespace Server.Items
{
	public class DarkSapphireBracelet : GoldBracelet
	{
		public override int LabelNumber{ get{ return 1073455; } } // dark sapphire bracelet

		[Constructable]
		public DarkSapphireBracelet() : base()
		{
			Weight = 1.0;
			
			Resistances.Energy = 15;
			
			BaseRunicTool.ApplyAttributesTo( this, Utility.RandomMinMax( 3, 5 ), 25, 100 );
			
			if ( Utility.Random( 100 ) < 10 )
				Attributes.RegenMana += 2;
			else
				Resistances.Energy += 10;		
		}

		public DarkSapphireBracelet( Serial serial ) : base( serial )
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
