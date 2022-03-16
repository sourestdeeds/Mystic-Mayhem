using Server.Items;
using Server.Mobiles;
using Server.Commands;
using System.Collections;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using System.Collections.Generic;
using System;
using Server.Accounting;
using Server.Gumps;

namespace Server.Items
{
    public class MOTDStone : Item
    {
        private ArrayList messages = new ArrayList();
        private ArrayList given = new ArrayList();

        [CommandProperty(AccessLevel.GameMaster)]
        public ArrayList Given
        {
            get { return given; }
            set { given = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public ArrayList Messages
        {
            get { return messages; }
            set { messages = value; }
        }

        public static void Initialize()
        {
            ArrayList stones = new ArrayList();
            if (StoneExists())
            {
                foreach (Item item in World.Items.Values)
                {
                    if (item is MOTDStone)
                    {
                        stones.Add(item);
                    }
                }

                if (stones.Count > 1)
                {
                    for (int i = 0; i < stones.Count; ++i)
                    {
                        MOTDStone ms = (MOTDStone)stones[i];

                        if(stones.Count > 1)
                            ms.Delete();
                    }
                }
            }
            else
            {
                MOTDStone ms = new MOTDStone();
            }
        }

        [Constructable]
        public MOTDStone()
            : base(0x9A8)
        {
            Movable = false;
            Visible = false;
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.CloseGump(typeof(MotdChange));
            from.SendGump(new MotdChange());
        }

        public static bool StoneExists()
        {
            foreach (Item item in World.Items.Values)
            {
                if (item is MOTDStone)
                    return true;
            }
            return false;
        }

        public MOTDStone(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version

            writer.Write(messages.Count);

            for (int i = 0; i < messages.Count; ++i)
            {
                writer.Write((string)messages[i]);
            }

            writer.Write(given.Count);

            for (int i = 0; i < given.Count; ++i)
            {
                writer.Write((Mobile)given[i]);
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            messages = new ArrayList();
            given = new ArrayList();

            int count1 = reader.ReadInt();

            for (int i = 0; i < count1; ++i)
            {
                string stoadd = reader.ReadString();
                messages.Add(stoadd);
            }

            int count2 = reader.ReadInt();

            for (int i = 0; i < count2; ++i)
            {
                Mobile mtoadd = reader.ReadMobile();
                given.Add(mtoadd);
            }
        }
    }
}