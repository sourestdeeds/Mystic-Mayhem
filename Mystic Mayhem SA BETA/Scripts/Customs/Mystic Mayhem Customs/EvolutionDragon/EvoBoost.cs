using System; 
using Server.Items; 

namespace Server.Items 
{ 
   	public class EvoBoost: Item 
   	{ 
	/*	private int m_Points = 5;
		private string m_BaseName = "Evo Boost +";

		[CommandProperty( AccessLevel.GameMaster )] 
		public int Points
		{
			get { return m_Points; }
			set 
			{ 
				m_Points = value;
				this.Name = m_BaseName + Convert.ToString(m_Points); 
			}
		} */
		[Constructable]
		public EvoBoost() : this( 1 )
		{
		//	Name = m_BaseName + Convert.ToString(m_Points);
		}

		[Constructable]
		public EvoBoost( int amount ) : base( 9911 )
		{
			Stackable = true;
			Weight = 0.01;
			Amount = amount;
			Name = "Evo Boost"; //m_BaseName + Convert.ToString(m_Points);
			Hue = 92;
		}

            	public EvoBoost( Serial serial ) : base ( serial ) 
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
		//	if ( Weight == 0.0 )
		//		Weight = 0.1; 
           	} 
        } 
} 