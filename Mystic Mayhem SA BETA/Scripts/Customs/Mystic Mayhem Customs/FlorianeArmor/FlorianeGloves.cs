using System;
using Server;

namespace Server.Items
{
  	public class FlorianeGloves : LeatherGloves 
   	{  
      		public override int ArtifactRarity{ get{ return 22; } } 

      		public override int InitMinHits{ get{ return 125; } } 
      		public override int InitMaxHits{ get{ return 125; } } 
		[Constructable] 
      		public FlorianeGloves()
      		{      Weight = 6.0; 
            		Name = "Floriane Gloves"; 
            		Hue = 1157;
        
            		Attributes.WeaponDamage=5;
            		Attributes.RegenHits=5;
            		Attributes.WeaponSpeed=5;
            		Attributes.ReflectPhysical=5;
	    		Attributes.LowerRegCost=5;
			ArmorAttributes.MageArmor = 1;

   			PhysicalBonus = 8; 
         		FireBonus = 7; 
         		ColdBonus = 6; 
         		PoisonBonus =9; 
         		EnergyBonus = 7; 
		}

		public FlorianeGloves( Serial serial ) : base( serial ) 
      		{ 
      		} 

      		public override void Serialize( GenericWriter writer ) 
      		{ 
         		base.Serialize( writer ); 

         		writer.Write( (int) 0 ); 
      		} 
        
      		public override void Deserialize(GenericReader reader) 
      		{ 
         		base.Deserialize( reader ); 

         		int version = reader.ReadInt(); 
      		} 
   	} 
} 


