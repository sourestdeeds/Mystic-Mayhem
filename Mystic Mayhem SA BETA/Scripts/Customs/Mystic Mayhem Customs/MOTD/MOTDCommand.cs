using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Commands;
using Server.Gumps;
using Server.Network;
using Server.Engines.Quests;
using System.IO;
using System.Collections; 

namespace Server.Commands
{
    public class MOTD
    {

        public static Mobile m_mob;

        public static void Initialize()
        {
            CommandSystem.Register("deletemotd", AccessLevel.Seer, new CommandEventHandler(Modify_MOTD_OnCommand));
            CommandSystem.Register("addmotd", AccessLevel.Seer, new CommandEventHandler(Update_MOTD_OnCommand));
            CommandSystem.Register("motd", AccessLevel.Player, new CommandEventHandler(MOTDD_OnCommand));
            EventSink.Login += new LoginEventHandler(EventSink_Login);
            EventSink.Speech += new SpeechEventHandler(MOTD_OnCommand);
        }

        private static void MOTD_OnCommand(SpeechEventArgs e)
        {
            m_mob = (Mobile)e.Mobile;

            if (e.Speech == "motd")
            {
                m_mob.CloseGump(typeof(MOTDGump));
                m_mob.SendGump(new MOTDGump(LoadMessage(), m_mob));
            }
        }

        private static void MOTDD_OnCommand(CommandEventArgs e)
        {
            m_mob = (Mobile)e.Mobile;

                m_mob.CloseGump(typeof(MOTDGump));
                m_mob.SendGump(new MOTDGump(LoadMessage(), m_mob));
        }

        private static void Modify_MOTD_OnCommand(CommandEventArgs e)
        {
            m_mob = e.Mobile;

            MOTDStone ms = (MOTDStone)GetMS();

            m_mob.CloseGump(typeof(MOTDModify));
            m_mob.SendGump(new MOTDModify());
        }

        private static void Update_MOTD_OnCommand(CommandEventArgs e)
        {
            m_mob = e.Mobile;

            m_mob.CloseGump(typeof(MotdChange));
            m_mob.SendGump(new MotdChange());
        }

        private static void EventSink_Login(LoginEventArgs args)
        {
            Mobile m = args.Mobile;

            MOTDStone ms = (MOTDStone)GetMS();

            if (!ms.Given.Contains(m) && ms.Messages.Count != 0)
            {
                m.CloseGump(typeof(MOTDGump));
                m.SendGump(new MOTDGump(LoadMessage(), m));
            }
        }

        public static string LoadMessage()
        {
            MOTDStone ms = (MOTDStone)GetMS();

            int count = ms.Messages.Count;
            count -= 1;

            string toreturn = "";

            if (count > 0)
            {
                for (int i = count; i > 1; i--)
                {
                    string stoadd = (string)ms.Messages[i];
                    toreturn = toreturn + stoadd;
                }
            }

            return toreturn;
        }

        private static MOTDStone MS = null;
        public static MOTDStone GetMS()
        {

            if (MS == null || MS.Deleted)
            {
                foreach (Item item in World.Items.Values)
                {
                    if (item is MOTDStone && !item.Deleted)
                    {
                        MS = (MOTDStone)item;
                        break;
                    }
                }
                if (MS == null || MS.Deleted) MS = new MOTDStone();
            }
            return MS;
        }

    }
}  