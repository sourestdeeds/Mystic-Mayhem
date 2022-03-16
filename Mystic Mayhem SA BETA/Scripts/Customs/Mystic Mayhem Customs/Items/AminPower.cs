using System;
using Server;

namespace Server.Items
{
	public class AminPower : GoldBracelet
	{
		public override int ArtifactRarity{ get{ return 22; } }

		[Constructable]
		public AminPower()
		{
			Weight = 1.0; 
            		Name = "Power of Amin"; 
            		// Hue = 1171;
			LootType = LootType.Cursed;

			Resistances.Physical = 50;
			Resistances.Cold = 50;
			Resistances.Fire = 50;
			Resistances.Energy = 50;
			Resistances.Poison = 50;
			Attributes.CastRecovery = 3;
			Attributes.CastSpeed = 2;
			
		}

		public AminPower( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}