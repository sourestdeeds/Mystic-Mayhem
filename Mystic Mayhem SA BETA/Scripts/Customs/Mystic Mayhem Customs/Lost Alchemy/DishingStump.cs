using System; 
using Server.Network; 

namespace Server.Items 
{ 

   [FlipableAttribute( 0x1865, 0x1866 )] 
   public class DishingStump : Item//, IRare
   { 
      [Constructable] 
      public DishingStump() : base( 0x1865 )
      { 
         //Movable = false;
         Weight = 10.0;
      } 

      public DishingStump( Serial serial ) : base( serial )
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
