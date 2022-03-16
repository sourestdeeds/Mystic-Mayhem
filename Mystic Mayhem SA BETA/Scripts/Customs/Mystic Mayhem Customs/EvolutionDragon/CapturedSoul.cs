using System; 
using Server.Items; 

namespace Server.Items 
{ 
   	public class CapturedSoul: Item 
   	{ 
		[Constructable]
		public CapturedSoul() : this( 1 )
		{
		}

		[Constructable]
		public CapturedSoul( int amount ) : base( 0xDF8 )
		{
			Stackable = true;
			Weight = 0.5;
			Amount = amount;
			Name = "captured soul";
			Hue = 623;
		}

            	public CapturedSoul( Serial serial ) : base ( serial ) 
            	{             
           	} 


           	public override void Serialize( GenericWriter writer ) 
           	{ 
              		base.Serialize( writer ); 
              		writer.Write( (int) 0 ); 
           	} 
            
           	public override void Deserialize( GenericReader reader ) 
           	{ 
              		base.Deserialize( reader ); 
              		int version = reader.ReadInt();
			if ( Weight == 0.0 )
				Weight = 0.5; 
           	} 
        } 
} 