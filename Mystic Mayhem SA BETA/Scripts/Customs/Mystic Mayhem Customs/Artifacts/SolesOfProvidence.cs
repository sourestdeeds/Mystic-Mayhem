using System;
using Server;

namespace Server.Items
{
    public class SolesOfProvidence : Sandals, ITokunoDyable
	{
		//public override int LabelNumber{ get{ return 1074493; } } // Mark of Travesty
		
		//public override int BasePhysicalResistance{ get{ return 5; } }
		//public override int BaseFireResistance{ get{ return 3; } }
		//public override int BaseColdResistance{ get{ return 2; } }
		//public override int BasePoisonResistance{ get{ return 4; } }
		//public override int BaseEnergyResistance{ get{ return 5; } }
	
		[Constructable]
		public SolesOfProvidence() : base()
		{
			Name = "Soles Of Providence";
			Hue = 1161;
			
			Attributes.Luck = 80;
			
		}

		public SolesOfProvidence( Serial serial ) : base( serial )
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

