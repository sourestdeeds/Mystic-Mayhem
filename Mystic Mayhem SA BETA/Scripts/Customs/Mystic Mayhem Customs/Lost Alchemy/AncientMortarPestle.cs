using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class AncientMortarPestle : BaseTool//, IRare
	{
		public override CraftSystem CraftSystem{ get{ return DefLostAlchemy.CraftSystem; } }

		[Constructable]
		public AncientMortarPestle() : base( 0xE9B )
		{
			Weight = 1.0;
                        //Movable = false;
                        Hue = 1272;
			Name = "ancient mortar and pestle";
		}

		[Constructable]
		public AncientMortarPestle( int uses ) : base( uses, 0xE9B )
		{
			Weight = 1.0;
                        //Movable = false;
                        Hue = 1272;
			Name = "ancient mortar and pestle";
		}

		public AncientMortarPestle( Serial serial ) : base( serial )
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
