using System;

namespace Server.Items
{
	public class FireRubyBracelet : GoldBracelet
	{
		public override int LabelNumber{ get{ return 1073454; } } // fire ruby bracelet

		[Constructable]
		public FireRubyBracelet() : base()
		{
			Weight = 1.0;
			
			Resistances.Fire = 15;
			
			BaseRunicTool.ApplyAttributesTo( this, Utility.RandomMinMax( 3, 5 ), 25, 100 );
			
			if ( Utility.Random( 100 ) < 10 )
				Attributes.RegenHits += 5;
			else
				Resistances.Fire += 10;		
		}

		public FireRubyBracelet( Serial serial ) : base( serial )
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
