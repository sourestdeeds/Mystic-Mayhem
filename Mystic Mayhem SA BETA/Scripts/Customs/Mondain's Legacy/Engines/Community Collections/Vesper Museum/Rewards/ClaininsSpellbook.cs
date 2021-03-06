using System;
using Server;

namespace Server.Items
{
	public class ClaininsSpellbook : Spellbook
	{
		public override int LabelNumber{ get{ return 1073262; } } // Clainin's Spellbook - Museum of Vesper Replica
	
		[Constructable]
		public ClaininsSpellbook() : base()
		{
			Hue = 0x84D;
			
			
			Attributes.RegenMana = 3;
			Attributes.Luck = 80;
			Attributes.LowerManaCost = 12;	
			Attributes.LowerRegCost = 15;
			Attributes.DefendChance = 5;
			
		}

		public ClaininsSpellbook( Serial serial ) : base( serial )
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

