// Created by GreyWolf
// Created On: 6/8/2008

using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class ExpertCookingGearBag : Bag
	{
           	[Constructable]
           	public ExpertCookingGearBag()
           	{
           		Name = "Bag of Expert Cooking Gear";
                Hue = 62;

			DropItem (new LegsofExpertCooking() );    	
			DropItem(new ArmsofExpertCooking());
			DropItem(new GlovesofExpertCooking());
			DropItem(new CapofExpertCooking());
			DropItem(new GorgetofExpertCooking());
			DropItem( new TunicofExpertCooking() );
           	}

           	[Constructable]
           	public ExpertCookingGearBag(int amount)
           	{
           	}


        public ExpertCookingGearBag(Serial serial) : base(serial)
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
