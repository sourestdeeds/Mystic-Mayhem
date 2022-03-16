using System;
using System.Collections;
using Server.Items;
using Server.Mobiles;

namespace Server
{
    public class Ability2
    {

        //Aura Start
        public static void Aura(Mobile from, int min, int max, int type, int range, int poisons, string text)
        {
            ArrayList targets = new ArrayList();

            foreach (Mobile m in from.GetMobilesInRange(range))
            {
                if (m == from || m == null)
                    continue;

                if (from is BaseCreature && ((BaseCreature)from).Controlled)
                {
                    if (m is BaseCreature && !(((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned))
                        targets.Add(m);
                    else if (m.Player && m.AccessLevel == AccessLevel.Player && m.Alive && m.Kills >= 5)
                        targets.Add(m);
                }
                else
                {
                    if (m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned))
                        targets.Add(m);
                    else if (m.Player && m.AccessLevel == AccessLevel.Player && m.Alive)
                        targets.Add(m);
                }
            }

            for (int i = 0; i < targets.Count; ++i)
            {
                Mobile m = (Mobile)targets[i];
                m.RevealingAction();
                m.SendMessage(text);

                int auradamage = Utility.RandomMinMax(min, max);

                if (type == 1)
                {
                    AOS.Damage(m, from, auradamage, 0, 100, 0, 0, 0);
                }
                else if (type == 2)
                {
                    AOS.Damage(m, from, auradamage, 0, 0, 100, 0, 0);
                }
                else if (type == 3)
                {
                    AOS.Damage(m, from, auradamage, 0, 0, 0, 100, 0);
                    m.FixedParticles(0x374A, 10, 15, 5021, EffectLayer.Waist);
                }
                else if (type == 4)
                {
                    AOS.Damage(m, from, auradamage, 0, 0, 0, 0, 100);
                }
                else
                {
                    AOS.Damage(m, from, auradamage, 100, 0, 0, 0, 0);
                }

                if (poisons == 1)
                    m.ApplyPoison(from, Poison.Lesser);
                else if (poisons == 2)
                    m.ApplyPoison(from, Poison.Regular);
                else if (poisons == 3)
                    m.ApplyPoison(from, Poison.Greater);
                else if (poisons == 4)
                    m.ApplyPoison(from, Poison.Deadly);
                else if (poisons == 5)
                    m.ApplyPoison(from, Poison.Lethal);
                else
                { };
            }

        }
        //Aura End
    }
}