using System;

namespace Server.Items
{
	public class BrilliantAmberBracelet : GoldBracelet
	{
		public override int LabelNumber{ get{ return 1073453; } } // brilliant amber bracelet

		[Constructable]
		public BrilliantAmberBracelet() : base()
		{
			Weight = 1.0;
			
			Attributes.SpellDamage = 15;
			
			BaseRunicTool.ApplyAttributesTo( this, Utility.RandomMinMax( 3, 5 ), 25, 100 );
			
			switch ( Utility.Random( 4 ) )
			{
				case 0: Attributes.LowerRegCost += 10; break;
				case 1: Attributes.CastSpeed += 1; break;
				case 2: Attributes.CastRecovery += 2; break;
				case 3: Attributes.SpellDamage += 15; break;
			}		
		}

		public BrilliantAmberBracelet( Serial serial ) : base( serial )
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
