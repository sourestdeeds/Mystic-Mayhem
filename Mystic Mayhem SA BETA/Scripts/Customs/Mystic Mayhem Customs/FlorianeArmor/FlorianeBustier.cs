using System;
using Server;

namespace Server.Items
{
  	public class FlorianeBustier : LeatherBustierArms 
   	{  
      		public override int ArtifactRarity{ get{ return 22; } } 

      		public override int InitMinHits{ get{ return 125; } } 
      		public override int InitMaxHits{ get{ return 125; } } 
		public override bool AllowMaleWearer{ get{ return false; } }
		[Constructable] 
      		public FlorianeBustier()  
      		{      Weight = 6.0; 
            		Name = "Floriane Bustier"; 
            		Hue = 1157;

            		Attributes.WeaponDamage=5;
            		Attributes.SpellDamage=5;
            		Attributes.WeaponSpeed=5;
            		Attributes.LowerRegCost=5;
            		Attributes.DefendChance=5;
			ArmorAttributes.MageArmor = 1;

   			PhysicalBonus = 16; 
         		FireBonus = 8; 
         		ColdBonus = 9; 
         		PoisonBonus =6; 
         		EnergyBonus = 6; 
		}

		public FlorianeBustier( Serial serial ) : base( serial ) 
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


