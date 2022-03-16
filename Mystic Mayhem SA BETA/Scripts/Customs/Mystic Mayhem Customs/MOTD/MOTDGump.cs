using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Commands;
using Server.Gumps;
using Server.Network;

namespace Server.Gumps
{
    public class MOTDGump : Gump
    {

        public string mesg;

        public MOTDGump(string message, Mobile m)
            : base(0, 20)
        {
            MOTDStone ms = (MOTDStone)GetMS();
            mesg = message;

            this.Closable = true;
            this.Disposable = false;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);

		AddBackground( 50, 10, 296, 334, 9270 );
		AddBackground( 60, 20, 277, 315, 3000 );
		AddLabel( 100, 27, 1365, "Mystic Mayhem Message of the Day" );

            this.AddHtml(73, 62, 247, 233, ""+mesg, (bool)true, (bool)true);
            this.AddButton(250, 305, 247, 248, 0, GumpButtonType.Reply, 0);

            this.AddLabel(130, 306, 999, @"Display On Login");

            if (!ms.Given.Contains(m))
            {
                this.AddButton(90, 300, 2153, 2153, 1, GumpButtonType.Reply, 0);
            }
            else
            {
                this.AddButton(90, 300, 2151, 2151, 1, GumpButtonType.Reply, 0);
            }
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            MOTDStone ms = (MOTDStone)GetMS();

            switch (info.ButtonID)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        if (!ms.Given.Contains(from))
                        {
                            ms.Given.Add(from);
                            from.SendMessage("You choose not to recieve on login.");
                        }
                        else
                        {
                            ms.Given.Remove(from);
                            from.SendMessage("You will now recieve the motd gump at login.");
                        }
                        break;
                    }
            }
        }

        public MOTDStone GetMS()
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

    }

}
