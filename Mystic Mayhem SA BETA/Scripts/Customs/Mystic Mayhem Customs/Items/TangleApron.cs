using System;

namespace Server.Items
{
    public class TangleApron : HalfApron, ITokunoDyable
	{
		//public override int LabelNumber{ get{ return 1075043; } } // Crimson Cincture
	
		[Constructable]
		public TangleApron() : base()
		{
			Name = "Tangle";
			Hue = 2220;
			
			Attributes.DefendChance = 5;
			Attributes.BonusInt = 10;
			Attributes.RegenMana = 2;
		}

		public TangleApron( Serial serial ) : base( serial )
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

