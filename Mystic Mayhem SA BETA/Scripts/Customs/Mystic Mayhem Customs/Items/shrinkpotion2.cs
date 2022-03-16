using System;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Xanthos.Interfaces;
using Xanthos.ShrinkSystem;
using Server.Regions;

namespace Server.Items
{
    public class NewShrinkPotion : Item
    {

        [Constructable]
        public NewShrinkPotion() : base( 0xF09 )
        {
            Name="a shrink potion";
            Hue = 2315;
        }

        public override void OnDoubleClick( Mobile from )
        {
             if ( !Movable )
                return;
            else if ( from.Combatant != null ) { from.SendMessage( "You can not do this in the heat of battle" ); return; }
            else if( from.InRange( this.GetWorldLocation(), 2 ) == false )
            {
                from.SendLocalizedMessage( 500486 );    //That is too far away.
                return;
            }

            Container pack = from.Backpack;

            if ( !(Parent == from || ( pack != null && Parent == pack )) ) //If not in pack.
            {
                from.SendLocalizedMessage( 1042001 );    //That must be in your pack to use it.
                return;
            }
            from.Target=new NewShrinkPotionTarget( this );
            from.SendMessage( "What do you wish to shrink?" );
        }

        private class NewShrinkPotionTarget : Target
        {
            private NewShrinkPotion m_Potion;

            public NewShrinkPotionTarget( Item i ) : base( 3, false, TargetFlags.None )
            {
                m_Potion=(NewShrinkPotion)i;
            }
            
            protected override void OnTarget( Mobile from, object target )
        {
            BaseCreature pet = target as BaseCreature;

            if ( target == from )
                from.SendMessage( "You cannot shrink yourself!" );

            else if ( target is Item )
                from.SendMessage( "You cannot shrink that!" );

            else if (target is BaseEscortable)
                      from.SendMessage("That person gives you a dirty look.");

            else if ( target is PlayerMobile )
                from.SendMessage( "That person gives you a dirty look!" );

            else if ( Server.Spells.SpellHelper.CheckCombat( from ) )
                from.SendMessage( "You cannot shrink your pet while you are fighting." );

            else if ( null == pet )
                from.SendMessage( "That is not a pet!" );

            else if ( ( pet.BodyValue == 400 || pet.BodyValue == 401 ) && pet.Controlled == false )
                from.SendMessage( "That person gives you a dirty look!" );

            else if ( pet.IsDeadPet )
                from.SendMessage( "You cannot shrink the dead!" );

            else if ( pet.Summoned )
                from.SendMessage( "You cannot shrink a summoned creature!" );

            else if ( pet.Combatant != null && pet.InRange( pet.Combatant, 12 ) && pet.Map == pet.Combatant.Map )
                from.SendMessage( "Your pet is fighting; you cannot shrink it yet." );

            else if ( pet.BodyMod != 0 )
                from.SendMessage( "You cannot shrink your pet while it is polymorphed." );

            else if ( pet.Controlled == false )
                from.SendMessage( "You cannot not shrink wild creatures." );

            else if ( pet.ControlMaster != from )
                from.SendMessage( "That is not your pet." );

            //else if ( ShrinkItem.IsPackAnimal( pet ) && ( null != pet.Backpack && pet.Backpack.Items.Count > 0 ) )
              //  from.SendMessage( "You must unload this pet's pack before it can be shrunk." );

            else if ( !(m_Potion.Deleted) )
            {
                if ( pet.ControlMaster != from && !pet.Controlled )
                {
                    SpawnEntry se = pet.Spawner as SpawnEntry;
                    if ( se != null && se.UnlinkOnTaming )
                    {
                        pet.Spawner.Remove( this );
                        pet.Spawner = null;
                    }

                    pet.CurrentWayPoint = null;
                    pet.ControlMaster = from;
                    pet.Controlled = true;
                    pet.ControlTarget = null;
                    pet.ControlOrder = OrderType.Come;
                    pet.Guild = null;
                    pet.Delta( MobileDelta.Noto );
                }

                IEntity p1 = new Entity( Serial.Zero, new Point3D( from.X, from.Y, from.Z ), from.Map );
                IEntity p2 = new Entity( Serial.Zero, new Point3D( from.X, from.Y, from.Z + 50 ), from.Map );

                Effects.SendMovingParticles( p2, p1, ShrinkTable.Lookup( pet ), 1, 0, true, false, 0, 3, 1153, 1, 0, EffectLayer.Head, 0x100 );
                from.PlaySound( 492 );
                from.AddToBackpack( new ShrinkItem( pet ) );
                m_Potion.Delete();

            }
            return;
        }
    }

        #region Serialization
        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int) 0 ); // version
        }

        public NewShrinkPotion( Serial serial ) : base( serial )
        {
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();
        } 
        #endregion
        
            
    }
}