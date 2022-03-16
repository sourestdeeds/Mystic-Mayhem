    //////////////////////////////////
   //			          		 //
  //      Scripted by Viago      //
 //		   www.fallensouls.org	 //
//////////////////////////////////
using System; 
using System.Collections; 
using Server; 
using Server.Commands;
using Server.Mobiles; 
using Server.Network; 
using Server.Prompts;
using Server.Items;
using Server.Guilds;
using Server.Gumps;
using Server.Targeting;


namespace Server.Mobiles
{ 
	public class LoreWand : BaseWand
	{
		[Constructable]
		public LoreWand() : base( WandEffect.Identification, 25, 175 )
		{
		Name = "Advanced Animal Lore Wand";
		ItemID = 3570;
		}
		public LoreWand( Serial serial ) : base( serial )
		{
		Name = "Advanced Animal Lore Wand";
		ItemID = 3570;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
		public override void OnWandUse( Mobile from )
		{
				from.Target = new InternalTarget();
		}
		public static void Initialize()
		{
			CommandSystem.Register( "L", AccessLevel.Counselor, new CommandEventHandler( Ex_OnCommand ) );    
		} 

		public static void Ex_OnCommand( CommandEventArgs args )
		{ 
			Mobile m = args.Mobile; 
			PlayerMobile from = m as PlayerMobile; 
          
			if( from != null ) 
			{  
				from.SendMessage ( "Target a Mobile to Examine it in detail." );
				m.Target = new InternalTarget();
			} 
		} 

		private class InternalTarget : Target
		{
			public InternalTarget() : base( 18, false, TargetFlags.None )
			{
			}

		protected override void OnTarget( Mobile from, object target ) // Override the protected OnTarget() for our feature
		{
         if ( target is BaseCreature ) 
         { 
            BaseCreature t = ( BaseCreature ) target; 


				if ( !t.Alive )
				{
					from.SendMessage( "That Is Dead, Can Not Examine it" ); // That item is already blessed
				}

				if ( from.AccessLevel > AccessLevel.Player )
					from.SendGump ( new AdvLoreGump( t ) );			
				else if ( (!t.Controlled || !t.Tamable) && from.Skills[SkillName.AnimalLore].Base < 100.0 )
				{
				from.SendLocalizedMessage( 1049674 ); // At your skill level, you can only lore tamed creatures.
				}
				else if ( !t.Tamable && from.Skills[SkillName.AnimalLore].Base < 110.0 )
				{
				from.SendLocalizedMessage( 1049675 ); // At your skill level, you can only lore tamed or tameable creatures.
				}
				else if ( !from.CheckTargetSkill( SkillName.AnimalLore, t, 0.0, 120.0 ) )
				{
				from.SendLocalizedMessage( 500334 ); // You can't think of anything you know offhand.
				}
				else
				{
				from.SendGump ( new AdvLoreGump( t ) );
				}
		}
         else if ( target is BaseMount ) 
         { 
            BaseMount m = ( BaseMount ) target; 
				if ( !m.Alive )
				{
					from.SendMessage( "That Is Dead, Can Not Examine it" ); // That item is already blessed
				}

				if ( from.AccessLevel > AccessLevel.Player )
					from.SendGump ( new AdvLoreGump( m ) );
				else if ( (!m.Controlled || !m.Tamable) && from.Skills[SkillName.AnimalLore].Base < 100.0 )
				{
				from.SendLocalizedMessage( 1049674 ); // At your skill level, you can only lore tamed creatures.
				}
				else if ( !m.Tamable && from.Skills[SkillName.AnimalLore].Base < 110.0 )
				{
				from.SendLocalizedMessage( 1049675 ); // At your skill level, you can only lore tamed or tameable creatures.
				}
				else if ( !from.CheckTargetSkill( SkillName.AnimalLore, m, 0.0, 120.0 ) )
				{
				from.SendLocalizedMessage( 500334 ); // You can't think of anything you know offhand.
				}
				else
				{
				from.SendGump ( new AdvLoreGump( m ) );
				}
		}
         else 
         { 
            from.SendMessage( "That is not a valid traget." );  
         } 
		}

	public class AdvLoreGump : Gump
	{
		private BaseCreature m_from;
		private int slot;
		private Map Map;
		private double skill;
		Mobile ControlMaster;
		private const int LabelColor = 1153;
		private string tere;
		private static string FormatSkill( BaseCreature c, SkillName name )
		{
			Skill skill = c.Skills[name];

			if ( skill.Base < 10.0 )
				return "---";

			return String.Format( "{0:F1}", skill.Base );
		}

		private static string FormatAttributes( int cur, int max )
		{
			if ( max == 0 )
				return "---";

			return String.Format( "{0}/{1}", cur, max );
		}

		private static string FormatStat( int val )
		{
			if ( val == 0 )
				return "---";

			return String.Format( "{0}", val );
		}

		private static string FormatElement( int val )
		{
			if ( val <= 0 )
				return "---";

			return String.Format( "{0}%", val );
		}
		public AdvLoreGump( BaseCreature t) : base( 200, 200 )
		{
			m_from = t;

			int slot = t.ControlSlots;
			double skill = t.MinTameSkill;
			Mobile ControlMaster = t.ControlMaster;
					Map map = t.Map;
			tere = t.Name;
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(0, 33, 514, 440, 3000);
			this.AddLabel(225, 48, 1150, tere );
			
				if ( t.Tamable == true  )
				{
		this.AddLabel(200, 128, 0, "Min Skill");this.AddLabel(280, 128, 0, skill.ToString());
				}
				else if ( t.Tamable == false  )
				{
				}
		this.AddLabel(200, 128, 0, "Min Skill");this.AddLabel(280, 128, 0, skill.ToString());
				if ( t.ControlMaster == null  )
				{
			this.AddLabel(200, 88, 0, "Has Master");this.AddLabel(280, 88, 0, "No");
				}
				else if ( t.Controlled == true  )
				{
		this.AddLabel(200, 88, 0, "Has Master");this.AddLabel(280, 88, 0, "Yes");
				}
				
			this.AddLabel(200, 108, 0, "Slots");this.AddLabel(280, 108, 0, slot.ToString() );

			AddHtmlLocalized( 28, 68, 160, 18, 1049593, 200, false, false ); // Attributes

			AddHtmlLocalized( 28, 88, 160, 18, 1049578, 1153, false, false ); // Hits
			AddHtml( 114, 88, 75, 18, FormatAttributes( t.Hits, t.HitsMax ), false, false );

			AddHtmlLocalized( 28, 108, 160, 18, 1049579, 1153, false, false ); // Stamina
			AddHtml( 114, 108, 75, 18, FormatAttributes( t.Stam, t.StamMax ), false, false );

			AddHtmlLocalized( 28, 128, 160, 18, 1049580, 1153, false, false ); // Mana
			AddHtml( 114, 128, 75, 18, FormatAttributes( t.Mana, t.ManaMax ), false, false );

			AddHtmlLocalized( 28, 148, 160, 18, 1028335, 1153, false, false ); // Strength
			AddHtml( 114, 148, 35, 18, FormatStat( t.Str ), false, false );

			AddHtmlLocalized( 28, 168, 160, 18, 3000113, 1153, false, false ); // Dexterity
			AddHtml( 114, 168, 35, 18, FormatStat( t.Dex ), false, false );

			AddHtmlLocalized( 28, 188, 160, 18, 3000112, 1153, false, false ); // Intelligence
			AddHtml( 114, 188, 35, 18, FormatStat( t.Int ), false, false );

		this.AddLabel(228, 148, 0, "Location");this.AddLabel(233, 168, 0, map.ToString());
			AddHtmlLocalized( 28, 208, 160, 18, 1049594, 200, false, false ); // Loyalty Rating
			AddHtmlLocalized( 28, 228, 200, 200, (!t.Controlled || t.Loyalty == 0) ? 1061643 : 1049595 + ((int)t.Loyalty / 10), LabelColor, false, false );
		this.AddLabel(212, 208, 0, "Advanced Lore");this.AddLabel(220, 228, 0, "Version 1.0");this.AddLabel(200, 248, 0, " ");
				AddHtmlLocalized( 28, 248, 160, 18, 1049581, LabelColor, false, false ); // Armor Rating
				AddHtml( 114, 248, 100, 100, FormatStat( t.VirtualArmor ), false, false );

				AddHtmlLocalized( 28, 268, 160, 18, 1061645, 200, false, false ); // Resistances

				AddHtmlLocalized( 28, 288, 160, 18, 1061646, 1153, false, false ); // Physical
				AddHtml( 114, 288, 35, 18, FormatElement( t.PhysicalResistance ), false, false );

				AddHtmlLocalized( 28, 308, 160, 18, 1061647, 1153, false, false ); // Fire
				AddHtml( 114, 308, 35, 18, FormatElement( t.FireResistance ), false, false );

				AddHtmlLocalized( 28, 328, 160, 18, 1061648, 1153, false, false ); // Cold
				AddHtml( 114, 328, 35, 18, FormatElement( t.ColdResistance ), false, false );

				AddHtmlLocalized( 28, 348, 160, 18, 1061649, 1153, false, false ); // Poison
				AddHtml( 114, 348, 35, 18, FormatElement( t.PoisonResistance ), false, false );

				AddHtmlLocalized( 28, 368, 160, 18, 1061650, 1153, false, false ); // Energy
				AddHtml( 114, 368, 35, 18, FormatElement( t.EnergyResistance ), false, false );
		
				AddHtmlLocalized( 350, 68, 160, 18, 1017319, 200, false, false ); // Damage

				AddHtmlLocalized( 350, 88, 160, 18, 1061646, 1153, false, false ); // Physical
				AddHtml( 440, 88, 35, 18, FormatElement( t.PhysicalDamage ), false, false );

				AddHtmlLocalized( 350, 108, 160, 18, 1061647, 1153, false, false ); // Fire
				AddHtml( 440, 108, 35, 18, FormatElement( t.FireDamage ), false, false );

				AddHtmlLocalized( 350, 128, 160, 18, 1061648, 1153, false, false ); // Cold
				AddHtml( 440, 128, 35, 18, FormatElement( t.ColdDamage ), false, false );

				AddHtmlLocalized( 350, 148, 160, 18, 1061649, 1153, false, false ); // Poison
				AddHtml( 440, 148, 35, 18, FormatElement( t.PoisonDamage ), false, false );

				AddHtmlLocalized( 350, 168, 160, 18, 1061650, 1153, false, false ); // Energy
				AddHtml( 440, 168, 35, 18, FormatElement( t.EnergyDamage ), false, false );
				
			AddHtmlLocalized( 350, 188, 160, 18, 3001030, 200, false, false ); // Combat Ratings

			AddHtmlLocalized( 350, 208, 160, 18, 1044103, 1153, false, false ); // Wrestling
			AddHtml( 440, 208, 35, 18, FormatSkill( t, SkillName.Wrestling ), false, false );

			AddHtmlLocalized( 350, 228, 160, 18, 1044087, 1153, false, false ); // Tactics
			AddHtml( 440, 228, 35, 18, FormatSkill( t, SkillName.Tactics ), false, false );

			AddLabel( 350, 248, 0,  "Magic Res"); // Magic Resistance
			AddHtml( 440, 248, 35, 18, FormatSkill( t, SkillName.MagicResist ), false, false );

			AddHtmlLocalized( 350, 268, 160, 18, 1044061, 1153, false, false ); // Anatomy
			AddHtml( 440, 268, 35, 18, FormatSkill( t, SkillName.Anatomy ), false, false );

			AddHtmlLocalized( 350, 288, 160, 18, 1044090, 1153, false, false ); // Poisoning
			AddHtml( 440, 288, 35, 18, FormatSkill( t, SkillName.Poisoning ), false, false );

			AddHtmlLocalized( 350, 308, 160, 18, 3001032, 200, false, false ); // Lore & Knowledge

			AddHtmlLocalized( 350, 328, 160, 18, 1044085, 1153, false, false ); // Magery
			AddHtml( 440, 328, 35, 18, FormatSkill( t, SkillName.Magery ), false, false );

			AddLabel( 350, 348, 0, "Eval Int"); // Evaluating Intelligence
			AddHtml( 440, 348, 35, 18,FormatSkill( t, SkillName.EvalInt ), false, false );

			AddHtmlLocalized( 350, 368, 160, 18, 1044106, 1153, false, false ); // Meditation
			AddHtml( 440, 368, 35, 18, FormatSkill( t, SkillName.Meditation ), false, false );
			
			AddHtmlLocalized( 208, 348, 160, 18, 1049563, 200, false, false ); // Preferred Foods

			int foodPref = 3000340;

			if ( (t.FavoriteFood & FoodType.FruitsAndVegies) != 0 )
				foodPref = 1049565; // Fruits and Vegetables
			else if ( (t.FavoriteFood & FoodType.GrainsAndHay) != 0 )
				foodPref = 1049566; // Grains and Hay
			else if ( (t.FavoriteFood & FoodType.Fish) != 0 )
				foodPref = 1049568; // Fish
			else if ( (t.FavoriteFood & FoodType.Meat) != 0 )
				foodPref = 1049564; // Meat

			AddHtmlLocalized( 180, 368, 160, 33, foodPref, 1153, false, false );

			AddHtmlLocalized( 208, 388, 160, 18, 1049569, 200, false, false ); // Pack Instincts

			int packInstinct = 3000340;

			if ( (t.PackInstinct & PackInstinct.Canine) != 0 )
				packInstinct = 1049570; // Canine
			else if ( (t.PackInstinct & PackInstinct.Ostard) != 0 )
				packInstinct = 1049571; // Ostard
			else if ( (t.PackInstinct & PackInstinct.Feline) != 0 )
				packInstinct = 1049572; // Feline
			else if ( (t.PackInstinct & PackInstinct.Arachnid) != 0 )
				packInstinct = 1049573; // Arachnid
			else if ( (t.PackInstinct & PackInstinct.Daemon) != 0 )
				packInstinct = 1049574; // Daemon
			else if ( (t.PackInstinct & PackInstinct.Bear) != 0 )
				packInstinct = 1049575; // Bear
			else if ( (t.PackInstinct & PackInstinct.Equine) != 0 )
				packInstinct = 1049576; // Equine
			else if ( (t.PackInstinct & PackInstinct.Bull) != 0 )
				packInstinct = 1049577; // Bull

			AddHtmlLocalized( 180, 408, 160, 18, packInstinct, 1153, false, false );
		}
	}
		}
		}
		}
