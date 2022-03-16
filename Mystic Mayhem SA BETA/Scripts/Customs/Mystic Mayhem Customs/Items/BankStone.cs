using System; 
using Server.Items; 

namespace Server.Items 
{ 
   public class BankStone : Item 
   { 
      [Constructable] 
      public BankStone() : base( 0xED4 ) 
      { 
         Name = "Bank Stone"; 
         Movable = false; 
	 Hue = 0x480;
      }

      public override void OnDoubleClick( Mobile from ) 
      {
      	BankBox box = from.BankBox; 

      	if ( box != null ) 
      	 box.Open(); 
      } 

	  public BankStone( Serial serial ) : base( serial ) 
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
