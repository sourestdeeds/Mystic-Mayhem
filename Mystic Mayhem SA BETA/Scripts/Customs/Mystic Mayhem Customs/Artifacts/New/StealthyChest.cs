// Created with UO Armor Generator
// Created On: 2/9/2010 12:20:18 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class StealthyChest : LeatherChest, ITokunoDyable
    {
        public override int BasePhysicalResistance{ get{ return 16; } }
        public override int BaseColdResistance{ get{ return 4; } }
        public override int BaseFireResistance{ get{ return 4; } }
        public override int BaseEnergyResistance{ get{ return 12; } }
        public override int BasePoisonResistance{ get{ return 7; } }
        public override int ArtifactRarity{ get{ return 15; } }
        public override int InitMinHits{ get{ return 255; } }
        public override int InitMaxHits{ get{ return 255; } }

        [Constructable]
        public StealthyChest()
        {
            Name = "Stealthy Chest";
            Hue = 2051;
            Attributes.NightSight = 1;
            Attributes.BonusDex = 3;
            Attributes.RegenStam = 2;
            Attributes.DefendChance = 10;
            ArmorAttributes.MageArmor = 1;
            ArmorAttributes.SelfRepair = 1;
            Attributes.LowerRegCost = 15;
            SkillBonuses.SetValues( 0, SkillName.Stealing, 5.0 );
            SkillBonuses.SetValues( 1, SkillName.Snooping, 5.0 );
            SkillBonuses.SetValues( 2, SkillName.Lockpicking, 5.0 );
            SkillBonuses.SetValues( 3, SkillName.Hiding, 5.0 );
        }

        public StealthyChest(Serial serial) : base( serial )
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
    } // End Class
} // End Namespace
