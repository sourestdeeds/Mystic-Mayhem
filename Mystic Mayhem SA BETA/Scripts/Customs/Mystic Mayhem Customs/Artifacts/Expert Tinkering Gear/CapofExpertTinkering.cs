// Created by GreyWolf
// Created On: 11/4/2007
// 

using System;
using Server;

namespace Server.Items
{
    public class CapofExpertTinkering : LeatherCap
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
        public CapofExpertTinkering()
        {
            Name = "Cap of Expert Tinkering";
            Hue = 702;
            //LootType = LootType.Blessed;
            Attributes.NightSight = 1;
          

            DefineMods();

        }

        private void DefineMods()
        {

            m_SkillMod0 = new DefaultSkillMod(SkillName.Tinkering, true, 5);

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
                m.RemoveStatMod("CapofExpertTinkering");

                if (m_SkillMod0 != null)
                    m_SkillMod0.Remove();


            }
        }

        public override void OnSingleClick(Mobile from)
        {
            this.LabelTo(from, Name);
        }

        public CapofExpertTinkering(Serial serial) : base( serial )
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