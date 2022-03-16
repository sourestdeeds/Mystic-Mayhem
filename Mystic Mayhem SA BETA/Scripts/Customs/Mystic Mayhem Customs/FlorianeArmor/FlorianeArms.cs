using System;
using Server;

namespace Server.Items
{
  	public class FlorianeArms : BoneArms 
   	{  
      		public override int ArtifactRarity{ get{ return 22; } } 

      		public override int InitMinHits{ get{ return 125; } } 
      		public override int InitMaxHits{ get{ return 125; } } 
		//public override bool AllowMaleWearer{ get{ return false; } }
		[Constructable] 
      		public FlorianeArms()
      		{      Weight = 6.0; 
            		Name = "Floriane Arms"; 
            		Hue = 1157;
        
           		Attributes.WeaponDamage=9;
           		Attributes.AttackChance=6;
            		Attributes.RegenMana=5;
	   		Attributes.RegenHits=5;
	    		Attributes.RegenStam=5;
	    		Attributes.LowerRegCost=9;
			ArmorAttributes.MageArmor = 1;

   			PhysicalBonus = 9; 
         		FireBonus = 6; 
         		ColdBonus = 6; 
         		PoisonBonus =6; 
         		EnergyBonus = 7; 
		}

		public FlorianeArms( Serial serial ) : base( serial ) 
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


