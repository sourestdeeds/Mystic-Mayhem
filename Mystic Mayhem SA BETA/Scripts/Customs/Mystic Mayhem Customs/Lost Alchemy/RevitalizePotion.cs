using System;
using Server.Network;
using Server;
namespace Server.Items
{
	public class RevitalizePotion : BasePotion
	{
		[Constructable]
		public RevitalizePotion() : base( 0xF0B, PotionEffect.Revitalize )
		{
			Weight = 1.0;
			Movable = true;
			Hue = 493;
			Name = "Revitalize Potion";
		}

		public RevitalizePotion( Serial serial ) : base( serial )
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
	  
	  	public override void Drink( Mobile m )
      	{ 
         	if ( m.InRange( this.GetWorldLocation(), 1 ) ) 
         	{ 
                        m.Hits = m.HitsMax ;
                        m.Mana = m.ManaMax ;
                        m.Stam = m.StamMax ;
           		m.SendMessage( "You feel completely Revitalized!" );
                        this.Delete();
                        m.AddToBackpack( new Bottle() );
         	} 
         	else 
         	{ 
            	m.LocalOverheadMessage( MessageType.Regular, 906, 1019045 ); // I can't reach that. 
         	} 
		}
	}
}
