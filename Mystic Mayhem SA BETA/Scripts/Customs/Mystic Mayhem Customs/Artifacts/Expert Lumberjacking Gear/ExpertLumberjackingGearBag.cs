// Created by GreyWolf
// Created On: 11/4/2007

using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class ExpertLumberjackingGearBag : Bag
	{
           	[Constructable]
           	public ExpertLumberjackingGearBag()
           	{
           		Name = "Bag of Expert Lumberjacking Gear";
                Hue = 1269;

			DropItem (new LegsofExpertLumberjacking() );    	
			DropItem(new ArmsofExpertLumberjacking());
			DropItem(new GlovesofExpertLumberjacking());
			DropItem(new CapofExpertLumberjacking());
			DropItem(new GorgetofExpertLumberjacking());
			DropItem( new TunicofExpertLumberjacking() );
           	}

           	[Constructable]
           	public ExpertLumberjackingGearBag(int amount)
           	{
           	}

		
           	public ExpertLumberjackingGearBag(Serial serial) : base( serial )
           	{
           	}

          	public override void Serialize(GenericWriter writer)
          	{
           		base.Serialize(writer);

           		writer.Write((int)0); // version 
     		}

           	public override void Deserialize(GenericReader reader)
      	{
           		base.Deserialize(reader);

          		int version = reader.ReadInt();
           	}
	}
}
