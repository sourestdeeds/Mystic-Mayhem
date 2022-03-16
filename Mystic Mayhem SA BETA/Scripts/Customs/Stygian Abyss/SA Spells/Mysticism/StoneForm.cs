using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.Mystic
{
	public class StoneFormSpell : MysticTransformationSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Stone Form", "In Rel Ylem",
				230,
				9022,
				Reagent.Bloodmoss,
				Reagent.FertileDirt,
				Reagent.Garlic
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }

		public override double RequiredSkill{ get{ return 33.0; } }
		public override int RequiredMana{ get{ return 11; } }

		public override int Body{ get{ return 705; } }

		public StoneFormSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void DoEffect( Mobile m )
		{
			m.PlaySound( 0x65B );
			m.FixedParticles( 0x3728, 1, 13, 9918, 92, 3, EffectLayer.Head );

			m.Delta( MobileDelta.WeaponDamage );
		}

		public override void RemoveEffect( Mobile m )
		{
			m.Delta( MobileDelta.WeaponDamage );
		}
	}
}