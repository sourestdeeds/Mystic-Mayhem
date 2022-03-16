using System;
using Server;

namespace Server.Items
{
  	public class FlorianeHeadress : Bandana 
   	{  
      		public override int ArtifactRarity{ get{ return 22; } } 

      		public override int InitMinHits{ get{ return 125; } } 
      		public override int InitMaxHits{ get{ return 125; } } 
		[Constructable] 
      		public FlorianeHeadress()
      		{      Weight = 6.0; 
            		Name = "Floriane Headress"; 
            		Hue = 1157;

            		Attributes.RegenMana=9;
            		Attributes.SpellDamage=6;
            		Attributes.WeaponSpeed=6;
            		Attributes.LowerRegCost=9;
		//	ArmorAttributes.MageArmor = 1;

   			Resistances.Physical = 9; 
         		Resistances.Fire = 6; 
         		Resistances.Cold = 6; 
         		Resistances.Poison =6; 
         		Resistances.Energy = 6; 
		}

		public FlorianeHeadress( Serial serial ) : base( serial ) 
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


