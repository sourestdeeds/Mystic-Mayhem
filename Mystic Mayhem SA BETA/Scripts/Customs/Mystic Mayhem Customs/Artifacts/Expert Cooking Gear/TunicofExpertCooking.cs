// Created by GreyWolf
// Created On: 6/8/2008 

using System;
using Server;

namespace Server.Items
{
    public class TunicofExpertCooking : LeatherChest
    {
        public override int BasePhysicalResistance{ get{ return 5; } }
        public override int BaseColdResistance{ get{ return 5; } }
        public override int BaseFireResistance{ get{ return 5; } }
        public override int BaseEnergyResistance{ get{ return 5; } }
        public override int BasePoisonResistance{ get{ return 5; } }
        public override int InitMinHits{ get{ return 50; } }
        public override int InitMaxHits{ get{ return 100; } }

        // For skill mods above cap without changing everything to above cap - GreyWolf.
        private SkillMod m_SkillMod0;

        [Constructable]
        public TunicofExpertCooking()
        {
            Name = "Tunic of Expert Cooking";
            Hue = 702;
            

            DefineMods();

        }

        private void DefineMods()
        {

            m_SkillMod0 = new DefaultSkillMod(SkillName.Cooking, true, 5);

        }

        private void SetMods(Mobile wearer)
        {

            wearer.AddSkillMod(m_SkillMod0);

        }

        public override bool OnEquip(Mobile from)
        {
            SetMods(from);
            return true;
        }

        public override void OnRemoved(object parent)
        {
            if (parent is Mobile)
            {
                Mobile m = (Mobile)parent;
                m.RemoveStatMod("TunicofExpertCooking");

                if (m_SkillMod0 != null)
                    m_SkillMod0.Remove();


            }
        }

        public override void OnSingleClick(Mobile from)
        {
            this.LabelTo(from, Name);
        }

        public TunicofExpertCooking(Serial serial) : base( serial )
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
