using System; 
using Server.Items; 

namespace Server.Items 
{ 
   public class BankBall : Item 
   { 
      [Constructable] 
      public BankBall() : base( 0xE73 ) 
      { 
         Name = "Bank Ball"; 
         Movable = true; 
	 Hue = 0x480;
	LootType = LootType.Blessed;
      }

      public override void OnDoubleClick( Mobile from ) 
      {
      	BankBox box = from.BankBox; 

      	if ( box != null ) 
      	 box.Open(); 
      } 

	  public BankBall( Serial serial ) : base( serial ) 
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
