using System;
using System.Collections;
using Server;
using Server.Commands;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
	public class PUniversalTubTarget : Target
	{
		private Item m_Item;

		public PUniversalTubTarget( Item item ) : base( 12, false, TargetFlags.None )
		{
			m_Item = item;
		}

		protected override void OnTarget( Mobile from, object target )
		{
			Gold m_Gold = (Gold)from.Backpack.FindItemByType( typeof( Gold ) );
			Gold b_Gold = (Gold)from.BankBox.FindItemByType( typeof( Gold ) );
			int m_Amount = from.Backpack.GetAmount( typeof( Gold ) );
			int b_Amount = from.BankBox.GetAmount( typeof( Gold ) );
			

			if (target is BaseContainer)
			{
				if (b_Amount > 99999)
				{
					BaseContainer z = target as BaseContainer;

                             		if ( !z.IsChildOf (from.Backpack))
                             		{
						if ( z is Backpack ) //it allow players to dye their backpacks for 1kk
						{
							from.BankBox.ConsumeTotal( typeof( Gold ), 100000 );
							from.SendMessage( "Removed 100,000 gold from your bank and hued your backpack." );
                               		  		z.Hue = m_Item.Hue;
                               		  		from.PlaySound( 0x23F );
						}

						else
						{                            	 	
							from.SendMessage( "You lack the gold to pay for that!" );
						}
                             		}					
					else
					{
                                 		z.Hue = m_Item.Hue;
                                 		from.PlaySound( 0x23F );
						from.BankBox.ConsumeTotal( typeof( Gold ), 10000 );
						from.SendMessage( "Removed 10,000 gold from your bank and hued your item." );
					}
				}

				else if (m_Amount >= 10000)
				{
					BaseContainer z = target as BaseContainer;
					
					if ( !z.IsChildOf (from.Backpack))
					{
						from.SendMessage( "The item is not in your pack!" );
					}
					else
					{
						z.Hue = m_Item.Hue;
                                 		from.PlaySound( 0x23F );
						from.Backpack.ConsumeTotal( typeof( Gold ), 10000 );
						from.SendMessage( "Removed 10,000 gold from your backpack and hued your item." );
					}
				}

				else if (b_Amount >= 10000)
				{
					BaseContainer z = target as BaseContainer;
					
					if ( !z.IsChildOf (from.Backpack))
					{
						from.SendMessage( "The item is not in your pack!" );
					}
					else
					{
						z.Hue = m_Item.Hue;
                                 		from.PlaySound( 0x23F );
						from.BankBox.ConsumeTotal( typeof( Gold ), 10000 );
						from.SendMessage( "Removed 10,000 gold from your bank and hued your item." );
					}
				}
			}

			else if (target is BaseJewel || target is BaseArmor || target is BaseClothing || target is BaseWeapon || target is BaseShield || target is EtherealMount || target is BaseSuit || target is Item )
			{
				if (m_Amount > 9999)
				{
					if ( target is Item )
                        		{
                            			Item x = (Item)target;
                             			if ( !x.IsChildOf (from.Backpack))
                             			{
                                  			from.SendMessage( "The item is not in your pack!" );
                             			}
                             			else
                             			{
                                 			x.Hue = m_Item.Hue;
                                 			from.PlaySound( 0x23F );
							from.Backpack.ConsumeTotal( typeof( Gold ), 10000 );
							from.SendMessage( "Removed 10,000 gold from your backpack and hued your item." );
                             			}
					}
				}

				else if (b_Amount > 9999)
				{
					if ( target is Item )
                        		{
                            			Item x = (Item)target;
                             			if ( !x.IsChildOf (from.Backpack))
                             			{
                                  			from.SendMessage( "The item is not in your pack!" );
                             			}
                             			else
                             			{
                                 			x.Hue = m_Item.Hue;
                                 			from.PlaySound( 0x23F );
							from.BankBox.ConsumeTotal( typeof( Gold ), 10000 );
							from.SendMessage( "Removed 10,000 gold from your bank and hued your item." );
                             			}
					}
				}
				
			}

			else if (target is BaseCreature)
			{
				if (m_Amount > 49999)
				{
					BaseCreature y = target as BaseCreature;
					
					if (y.Controlled && y.ControlMaster == from)
					{
						y.Hue = m_Item.Hue;
						from.Backpack.ConsumeTotal( typeof( Gold ), 50000 );
						from.SendMessage( "Removed 50,000 gold from backpack and hued your pet." );
					}
					else
					{
						from.SendMessage("You can only dye animals whom you control!");
					}
				}

				else if (b_Amount > 49999)
				{
					BaseCreature y = target as BaseCreature;
					
					if (y.Controlled && y.ControlMaster == from)
					{
						y.Hue = m_Item.Hue;
						from.BankBox.ConsumeTotal( typeof( Gold ), 50000 );
						from.SendMessage( "Removed 50,000 gold from your bank and hued your pet." );
					}
					else
					{
						from.SendMessage("You can only dye animals whom you control!");
					}
				}
			}

			else
			{
				from.SendMessage("Invalid target.");
			}
		}
	}
	
	public class PaidUniversalDyeTub : Item
	{
		private bool m_Redyable;

		[Constructable]
		public PaidUniversalDyeTub() : base( 0xFAB )
		{
			Weight = 0.0;
			Hue = 0;
			Name = "Universal Dye Tub";
			m_Redyable = false;
			Movable = false;
		}

		public PaidUniversalDyeTub( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			{
				from.Target = new PUniversalTubTarget( this );
				from.SendMessage( "You may now hue your items for 10,000 gold each and your pets for 50,000 gold each." );
				//from.SendMessage( "and your pets for 10,000 gold each." );
				from.SendMessage( "You may also hue your backpack for 100,000 gold." );
			}
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			if ( Name == "Ultimate Dye Tub" )
			{
				int intNumber = this.Hue;
				string strNumber = intNumber.ToString("#");
				Name = strNumber;
			}
		}
	}
}
