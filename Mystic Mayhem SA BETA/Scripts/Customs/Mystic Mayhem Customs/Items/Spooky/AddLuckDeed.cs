// Coded by GreyWolf and Lord_Greywolf.
// Version 2.0 for RunUO 2.0 RC1
// September 8, 2007
// Please do not remove this header section so credit is given where due.

using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server;

namespace Server.Items
{
	public class AddLuckTarget : Target // Create our targeting class (which we derive from the base target class)
	{
		private AddLuckDeed m_Deed;

		public AddLuckTarget( AddLuckDeed deed ) : base( 1, false,
 TargetFlags.None )
		{
			m_Deed = deed;
		}

		protected override void OnTarget( Mobile from, object target ) //Override the protected OnTarget() for our feature
 		{
			if ( target is BaseArmor )
			{
				Item item = (Item)target;

				((BaseArmor)item).Attributes.Luck += 100;
				from.SendMessage( "You magically add luck to your armor...." );

				m_Deed.Delete(); // Delete the deed
			}

            else if (target is BaseWeapon)
            {
                Item item = (Item)target;

                ((BaseWeapon)item).Attributes.Luck += 100;
                from.SendMessage("You magically add luck to your weapon....");

                m_Deed.Delete(); // Delete the deed
            }

            else if (target is BaseClothing)
            {
                Item item = (Item)target;

                ((BaseClothing)item).Attributes.Luck += 100;
                from.SendMessage("You magically add luck to your clothing....");

                m_Deed.Delete(); // Delete the deed
            }

			else
			{
			from.SendMessage( "You cannot put Luck on that" );
			}
		}
    }

	public class AddLuckDeed : Item // Create the item class which is derived from the base item class
 	{
		[Constructable]
		public AddLuckDeed() : base( 0x14F0 )
		{
			Weight = 1.0;
			Name = "a luck deed";
			LootType = LootType.Blessed;
			Hue = 1150;
		}

		public AddLuckDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			LootType = LootType.Blessed;

			int version = reader.ReadInt();
		}

		public override bool DisplayLootType{ get{ return false; } }

		public override void OnDoubleClick( Mobile from ) // Override double click of the deed to call our target
 
		{
			if ( !IsChildOf( from.Backpack ) ) // Make sure its in their pack
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
 
			}
			else
			{
				from.SendMessage("What item would you like to add Luck to?" );
				from.Target = new AddLuckTarget( this ); // Call our target
			}
        }
	} 
}
