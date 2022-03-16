using System;
using Server.Items;

namespace Server.Items
{
	public class AghorHelm : BoneHelm
	{
	//	public override int LabelNumber{ get{ return 1074471; } } // Gauntlets of the Grizzle
		
		public override SetItem SetID{ get{ return SetItem.Aghor; } }
		public override int Pieces{ get{ return 5; } }
	
		public override int BasePhysicalResistance{ get{ return 6; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 7; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		[Constructable]
		public AghorHelm() : base()
		{
			Name = "The Head Of Agh'or The Cold Blooded";
			SetHue = 2419;
			
			ArmorAttributes.MageArmor = 1;
			Attributes.BonusHits = 5;
			Attributes.BonusDex = 2;
			
			SetAttributes.DefendChance = 45;
			SetAttributes.BonusStr = 25;
			
			SetSelfRepair = 3;
			
			SetPhysicalBonus = 10;
			SetFireBonus = 10;
			SetColdBonus = 10;
			SetPoisonBonus = 10;
			SetEnergyBonus = 10;
		}

		public AghorHelm( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
}