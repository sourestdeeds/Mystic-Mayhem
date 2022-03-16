using System;
using Server;

namespace Server.Items
{
	public class ShimmeringEffusionStatuette : BaseStatuette
	{
		public override int LabelNumber{ get{ return 1074503; } } // Shimmering Effusion Statuette
	
		[Constructable]
		public ShimmeringEffusionStatuette() : base( 0x2D87 )
		{
			Weight = 1.0;			
		}

		public ShimmeringEffusionStatuette( Serial serial ) : base( serial )
		{
		}
		
		private static int[] m_Sounds = new int[]
		{
			0x58D, 0x58E, 0x58F, 0x590, 0x591, 0x592, 0x593, 0x594, 0x595		
		};
		
		public override void PlaySound( Mobile to )
		{
			Effects.PlaySound( Location, Map, m_Sounds[ Utility.Random( m_Sounds.Length ) ] );
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

