using System; 
using Server.Items; 

namespace Server.Items 
{ 
   	public class GlycaneVenom: Item 
   	{ 
		[Constructable]
		public GlycaneVenom() : this( 1 )
		{
		}

		[Constructable]
		public GlycaneVenom( int amount ) : base( 0xF8F ) //F82
		{
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
			Name = "Glycane Venom";
			Hue = 1372;
		}

            	public GlycaneVenom( Serial serial ) : base ( serial ) 
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
           	} 
        } 
} 