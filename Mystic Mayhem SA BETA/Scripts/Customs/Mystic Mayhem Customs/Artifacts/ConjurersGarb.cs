using System;
using Server;

namespace Server.Items
{
    public class ConjurersGarb : Robe, ITokunoDyable
	{
		//public override int LabelNumber{ get{ return 1074493; } } // Mark of Travesty
		
		//public override int BasePhysicalResistance{ get{ return 5; } }
		//public override int BaseFireResistance{ get{ return 3; } }
		//public override int BaseColdResistance{ get{ return 2; } }
		//public override int BasePoisonResistance{ get{ return 4; } }
		//public override int BaseEnergyResistance{ get{ return 5; } }
	
		[Constructable]
		public ConjurersGarb() : base()
		{
			Name = "Conjurer's Garb";
			Hue = 1194;
			
			Attributes.Luck = 140;
			Attributes.RegenMana = 2;
			Attributes.DefendChance = 5;
			
		}

		public ConjurersGarb( Serial serial ) : base( serial )
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

