using System;
using Server;

namespace Server.Items
{
	public class ShieldOfSalvation : MetalShield
	{
		public override int ArtifactRarity{ get{ return 23; } }

		public override int InitMinHits{ get{ return 125; } }
		public override int InitMaxHits{ get{ return 125; } }

        	public override int AosStrReq { get { return 70; } }
	        public override int OldStrReq { get { return 70; } }

	        public override int BasePhysicalResistance { get { return 10; } }
	        public override int BaseFireResistance { get { return 10; } }
        	public override int BaseColdResistance { get { return 10; } }
	        public override int BasePoisonResistance { get { return 10; } }
        	public override int BaseEnergyResistance { get { return 10; } }


		[Constructable]
		public ShieldOfSalvation()
		{
			Name = "Shield Of Salvation";

			Resource = 0;

			LootType = LootType.Cursed;

			Hue = 2958;
			Attributes.NightSight = 1;
			Attributes.SpellChanneling = 1;
			Attributes.DefendChance = 25;
			Attributes.AttackChance = 25;
			Attributes.CastSpeed = 2;
			Attributes.CastRecovery = 2;
			Attributes.LowerRegCost = 35;
			Attributes.LowerManaCost = 25;


			switch (Utility.Random(5))
			{
			case 0: Attributes.WeaponDamage = 5; break;
			case 1: Attributes.WeaponDamage = 10; break;
			case 2: Attributes.WeaponDamage = 15; break;
			case 3: Attributes.WeaponDamage = 20; break;
			case 4: Attributes.WeaponDamage = 25; break;
			}

			switch (Utility.Random(3))
			{
			case 0: Attributes.WeaponSpeed = 5; break;
			case 1: Attributes.WeaponSpeed = 10; break;
			case 2: Attributes.WeaponSpeed = 15; break;
			}

			switch (Utility.Random(5))
			{
			case 0: Attributes.ReflectPhysical = 5; break;
			case 1: Attributes.ReflectPhysical = 10; break;
			case 2: Attributes.ReflectPhysical = 15; break;
			case 3: Attributes.ReflectPhysical = 20; break;
			case 4: Attributes.ReflectPhysical = 25; break;
			}

			switch (Utility.Random(3))
			{
			case 0: Attributes.BonusHits = 5; break;
			case 1: Attributes.BonusHits = 10; break;
			case 2: Attributes.BonusHits = 15; break;
			}


		}

		public ShieldOfSalvation( Serial serial ) : base( serial )
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

			if ( Attributes.NightSight == 0 )
				Attributes.NightSight = 1;
		}
	}
}