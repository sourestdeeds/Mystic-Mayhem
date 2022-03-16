using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using Server.Network;
using System.Collections;

namespace Server.Gumps
{
    public class MOTDModify : Gump
    {

        public MOTDModify()
            : base(0, 0)
        {
            MOTDStone ms = (MOTDStone)GetMS();

            int count = ms.Messages.Count;
            count -= 1;

            string entry1 = "";
            string entry2 = "";
            string entry3 = "";

            int check = 0;

            if (count > 0)
            {
                for (int i = count; i > 1; i--)
                {
                    string stoadd = (string)ms.Messages[i];
                    check += 1;

                    if (check == 1)
                    {
                        entry1 = stoadd;
                    }
                    if (check == 2)
                    {
                        entry2 = stoadd;
                    }
                    if (check == 3)
                    {
                        entry3 = stoadd;
                    }
                }
            }

            this.Closable = true;
            this.Disposable = false;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);

            this.AddBackground(170, 50, 309, 423, 9200);

            this.AddHtml(182, 64, 200, 100, "" + entry1, (bool)true, (bool)true);
            this.AddHtml(182, 187, 200, 100, "" + entry2, (bool)true, (bool)true);
            this.AddHtml(182, 312, 200, 100, "" + entry3, (bool)true, (bool)true);

            this.AddButton(439, 76, 1209, 1210, 1, GumpButtonType.Reply, 0); //Entry 1 Delete
            this.AddLabel(391, 71, 999, @"Delete");

            this.AddButton(439, 199, 1209, 1210, 2, GumpButtonType.Reply, 0); //Entry 2 Delete
            this.AddLabel(391, 194, 999, @"Delete");

            this.AddButton(439, 324, 1209, 1210, 3, GumpButtonType.Reply, 0); //Entry 2 Delete
            this.AddLabel(391, 319, 999, @"Delete");
        }

        public static MOTDStone GetMS()
        {
            foreach (Item item in World.Items.Values)
            {
                if (item is MOTDStone)
                {
                    MOTDStone ms = (MOTDStone)item;
                    return ms;
                }
            }
            MOTDStone ms2 = new MOTDStone();
            return ms2;
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            MOTDStone ms = (MOTDStone)GetMS();

            int count = ms.Messages.Count;
            count -= 1;

            switch (info.ButtonID)
            {
                case 0:
                    {
                        from.SendMessage("MOTD unchanged.");
                        break;
                    }
                case 1: 
                    {
                        try
                        {
                            ms.Messages.Remove(ms.Messages[count]);
                            from.SendMessage("Removed.");
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            from.SendMessage("No entry exists.");
                        }
                        break;
                    }
                case 2: 
                    {
                        try
                        {
                            ms.Messages.Remove(ms.Messages[count - 1]);
                            from.SendMessage("Removed.");
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            from.SendMessage("No entry exists.");
                        }
                        break;
                    }
                case 3: 
                    {
                        try
                        {
                            ms.Messages.Remove(ms.Messages[count - 2]);
                            from.SendMessage("Removed.");
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            from.SendMessage("No entry exists.");
                        }
                        break;
                    }
            }
        }
    }
}