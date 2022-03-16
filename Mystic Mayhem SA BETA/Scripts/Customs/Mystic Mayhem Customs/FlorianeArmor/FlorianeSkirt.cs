using System;
using Server;

namespace Server.Items
{
  	public class FlorianeSkirt : LeatherSkirt
   	{  
      		public override int ArtifactRarity{ get{ return 22; } } 

      		public override int InitMinHits{ get{ return 125; } } 
      		public override int InitMaxHits{ get{ return 125; } } 
		public override bool AllowMaleWearer{ get{ return false; } }
		[Constructable] 
      		public FlorianeSkirt()
      		{      Weight = 6.0; 
            		Name = "Floriane Skirt"; 
            		Hue = 1157;

            		Attributes.WeaponDamage=5;
            		Attributes.SpellDamage=6;
            		Attributes.RegenHits=5;
            		Attributes.LowerRegCost=4;
			ArmorAttributes.MageArmor = 1;

   			PhysicalBonus = 10; 
         		FireBonus = 10; 
         		ColdBonus = 10; 
         		PoisonBonus =10; 
         		EnergyBonus = 10; 
		}

		public FlorianeSkirt( Serial serial ) : base( serial ) 
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


