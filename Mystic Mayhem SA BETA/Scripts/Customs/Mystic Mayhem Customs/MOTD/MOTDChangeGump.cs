using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Gumps
{
    public class MotdChange : Gump
    {
        public bool Reset_Lists = true; // Set to "true" or "false".

        public MotdChange()
            : base(0, 0)
        {

            this.Closable = true;
            this.Disposable = false;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
        //    this.AddBackground(214, 220, 343, 234, 9200);
			AddBackground( 214, 220, 343, 234, 9270 );
			AddBackground( 224, 230, 324, 215, 3000 );
		AddLabel( 350, 230, 1365, "Add MOTD" );
            AddTextEntry(233, 250, 305, 182, 1153, 0, "");
            AddButton(478, 420, 247, 248, 1, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case 0:
                    {
                        from.SendMessage("MOTD Unchanged.");

                        break;
                    }
                case 1:
                    {
                        TextRelay entry = info.GetTextEntry(0);
                        string text = (entry == null ? "" : entry.Text.Trim());

                        MOTDStone ms = (MOTDStone)GetMS();

                        if (AddMessage(ms, text))
                        {
                            World.Broadcast(70, true, "MOTD has been updated, say motd to see whats new.");

                            if(Reset_Lists)
                                ms.Given.Clear();
                        }
                        else
                        {
                            from.SendMessage("Invalid Message.");
                        }

                        break;
                    }
            }

        }

        public bool AddMessage(MOTDStone ms, string s)
        {
            if (s == "")
                return false;
            if (s.Length < 1)
                return false;

            if (ms.Messages.Count == 0)
            {
                ms.Messages.Add("basztestm");
                ms.Messages.Add("basztestm");
            }

            string stoadd = DateTime.Now.ToString() + ": PFT<BR>" + s + "<BR><BR>";

            ms.Messages.Add(stoadd);

            if (ms.Messages.Count > 5)
            {
                string remove = (string)ms.Messages[2];

                if (ms.Messages.Contains(remove))
                    ms.Messages.Remove(remove);
            }

            return true;
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