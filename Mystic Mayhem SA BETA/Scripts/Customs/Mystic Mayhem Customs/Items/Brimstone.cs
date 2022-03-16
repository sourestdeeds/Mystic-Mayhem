using System; 
using Server.Items; 

namespace Server.Items 
{ 
   	public class Brimstone: Item 
   	{ 
		[Constructable]
		public Brimstone() : this( 1 )
		{
		}

		[Constructable]
		public Brimstone( int amount ) : base( 0xF7F )
		{
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
			Name = "brimstone";
		//	Hue = 1109;
		}

            	public Brimstone( Serial serial ) : base ( serial ) 
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