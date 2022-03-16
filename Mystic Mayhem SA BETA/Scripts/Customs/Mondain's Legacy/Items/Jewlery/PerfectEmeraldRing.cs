using System;

namespace Server.Items
{
	public class PerfectEmeraldRing : GoldRing
	{
		public override int LabelNumber{ get{ return 1073459; } } // perfect emerald ring

		[Constructable]
		public PerfectEmeraldRing() : base()
		{
			Weight = 1.0;
			
			Resistances.Poison = 15;
			
			BaseRunicTool.ApplyAttributesTo( this, Utility.RandomMinMax( 3, 5 ), 25, 100 );
			
			if ( Utility.RandomBool() )
				Resistances.Poison += 10;	
			else
				Attributes.DefendChance += 15;
		}

		public PerfectEmeraldRing( Serial serial ) : base( serial )
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
