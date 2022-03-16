using System;
using System.Collections;
using Server;
using Server.Prompts;
using Server.Mobiles;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Multis;
using Server.Regions;


namespace Server.Items
{
        [FlipableAttribute( 3649, 3648, 10251, 10252, 10253, 10254, 10255, 10256, 10257, 10258, 10259, 10260 )]
        public class ToolBox : BaseContainer, ITelekinesisable
        {

                private int m_TinkerTools;
                private int m_SewingKit;
                private int m_Saw;
                private int m_FletcherTools;
                private int m_ScribesPen;
                private int m_Tongs;
                private int m_SmithHammer;
		private int m_MalletAndChisel;
		private int m_MortarPestle;

                private int m_Shovel;
		private int m_SturdyShovel;
		private int m_Pickaxe;
		private int m_SturdyPickaxe;
		private int m_GargoylesPickaxe;

		private int m_ProspectorsTool;
                private int m_Hatchet;
		private int m_Axe;
		private int m_Extra1;

		private int m_Extra2;
		private int m_PowderOfTemperament;
		private int m_Extra3;
		private int m_Extra4;
		private int m_Extra5;
		private int m_Extra6;
		private int m_Extra7;
		private int m_Extra8;
		private int m_Extra9;
		private int m_ExtraA;
		private int m_ExtraB;




                [CommandProperty( AccessLevel.GameMaster )]
                public int Shovel{ get{ return m_Shovel; } set{ m_Shovel = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int SewingKit{ get{ return m_SewingKit; } set{ m_SewingKit = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int Saw{ get{ return m_Saw; } set{ m_Saw = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int TinkerTools{ get{ return m_TinkerTools; } set{ m_TinkerTools = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int ScribesPen{ get{ return m_ScribesPen; } set{ m_ScribesPen = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int Tongs{ get{ return m_Tongs; } set{ m_Tongs = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int FletcherTools{ get{ return m_FletcherTools; } set{ m_FletcherTools = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int Hatchet{ get{ return m_Hatchet; } set{ m_Hatchet = value; InvalidateProperties(); } }

                [CommandProperty( AccessLevel.GameMaster )]
                public int SmithHammer{ get{ return m_SmithHammer; } set{ m_SmithHammer = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
                public int Axe{ get{ return m_Axe; } set{ m_Axe = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
                public int MalletAndChisel{ get{ return m_MalletAndChisel; } set{ m_MalletAndChisel = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
                public int MortarPestle{ get{ return m_MortarPestle; } set{ m_MortarPestle = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
                public int Extra1{ get{ return m_Extra1; } set{ m_Extra1 = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
                public int GargoylesPickaxe{ get{ return m_GargoylesPickaxe; } set{ m_GargoylesPickaxe = value; InvalidateProperties(); } }


		[CommandProperty( AccessLevel.GameMaster )]
                public int ProspectorsTool{ get{ return m_ProspectorsTool; } set{ m_ProspectorsTool = value; InvalidateProperties(); } }
		[CommandProperty( AccessLevel.GameMaster )]
                public int Extra2{ get{ return m_Extra2; } set{ m_Extra2 = value; InvalidateProperties(); } }
		[CommandProperty( AccessLevel.GameMaster )]
                public int PowderOfTemperament{ get{ return m_PowderOfTemperament; } set{ m_PowderOfTemperament = value; InvalidateProperties(); } }
		[CommandProperty( AccessLevel.GameMaster )]
                public int Extra3{ get{ return m_Extra3; } set{ m_Extra3 = value; InvalidateProperties(); } }
		[CommandProperty( AccessLevel.GameMaster )]
                public int Extra4{ get{ return m_Extra4; } set{ m_Extra4 = value; InvalidateProperties(); } }
		[CommandProperty( AccessLevel.GameMaster )]
                public int Extra5{ get{ return m_Extra5; } set{ m_Extra5 = value; InvalidateProperties(); } }
		[CommandProperty( AccessLevel.GameMaster )]
                public int Extra6{ get{ return m_Extra6; } set{ m_Extra6 = value; InvalidateProperties(); } }
		[CommandProperty( AccessLevel.GameMaster )]
                public int Extra7{ get{ return m_Extra7; } set{ m_Extra7 = value; InvalidateProperties(); } }
		[CommandProperty( AccessLevel.GameMaster )]
                public int Extra8{ get{ return m_Extra8; } set{ m_Extra8 = value; InvalidateProperties(); } }
		[CommandProperty( AccessLevel.GameMaster )]
                public int Extra9{ get{ return m_Extra9; } set{ m_Extra9 = value; InvalidateProperties(); } }
		[CommandProperty( AccessLevel.GameMaster )]
                public int ExtraA{ get{ return m_ExtraA; } set{ m_ExtraA = value; InvalidateProperties(); } }
		[CommandProperty( AccessLevel.GameMaster )]
                public int ExtraB{ get{ return m_ExtraB; } set{ m_ExtraB = value; InvalidateProperties(); } }
		[CommandProperty( AccessLevel.GameMaster )]
                public int Pickaxe{ get{ return m_Pickaxe; } set{ m_Pickaxe = value; InvalidateProperties(); } }
		[CommandProperty( AccessLevel.GameMaster )]
                public int SturdyPickaxe{ get{ return m_SturdyPickaxe; } set{ m_SturdyPickaxe = value; InvalidateProperties(); } }
		[CommandProperty( AccessLevel.GameMaster )]
                public int SturdyShovel{ get{ return m_SturdyShovel; } set{ m_SturdyShovel = value; InvalidateProperties(); } }

                [Constructable]
                public ToolBox() : base( 3649 )
                {
                        Movable = true;
                        Weight = 100.0;
                        Hue = 0x10A;
                        Name = "Tool Box";
			MaxItems = 1;
                }

		public virtual void OnTelekinesis( Mobile from )
		{
			from.BoltEffect( 0 );
			from.SendMessage("Are you trying to Exploit Something?");
			AOS.Damage( from, from, 60, 0, 0, 0, 0, 100 );
			return;
		}

                public override void OnDoubleClick( Mobile from )
                {
                        if ( Movable )
                        {
                                from.SendMessage( "You haven't locked it down!" );
                                return;
                        }
                        if ( !from.InRange( GetWorldLocation(), 2 ) )
                                from.LocalOverheadMessage( Network.MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
                        else if ( from is PlayerMobile )
                                from.SendGump( new ToolBoxGump( (PlayerMobile)from, this, 1 ) );
                }
				
//aa 

		bool dropflag = false;
		bool added = false;

		public override bool OnDragDrop ( Mobile from, Item dropped)
		{
//aa**
			added = false;
			dropflag = false;
			if ( dropped is BaseContainer )
			{
				from.SendMessage( "That does not belong in this!" );
                                return false;
			}
                        if ( Movable )
                        {
                                from.SendMessage( "You haven't locked it down!" );
                                return false;
                        }
                        if ( !from.InRange( GetWorldLocation(), 2 ) )
			{
                                from.LocalOverheadMessage( Network.MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.

			}
                        else
			{
				dropflag = true;
				EndCombine( from, dropped );
				if( added )
				{
					added = false;

					if( from.HasGump(typeof (ToolBoxGump) ) )
					{
//aa***
						from.CloseGump( typeof( ToolBoxGump ) );
						from.SendGump( new ToolBoxGump( (PlayerMobile)from, this, 1 ) );
					}
					else
					{
						this.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format( "Item added!" ) );
					}
					return true;
				}
			}
			return false;
		}
//aa end

                public void BeginCombine( Mobile from, int page )
                {
//aa***
			if ( added )
				from.CloseGump( typeof( ToolBoxGump ) );
			if( this.dropflag )
			{
				this.dropflag = false;
				return;
			}

			from.SendGump( new ToolBoxGump( (PlayerMobile)from, this, page ) );
//aa end
			from.Target = new ToolBoxTarget( this );
                }

                public void EndCombine( Mobile from, object o )
                {
			if ( o is Item ) //&& ((Item)o).IsChildOf( from.Backpack ) )
                        {                                
                                if ( o is SewingKit )
                                {
                                        if ( SewingKit >= 10000 )
                                                from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
                                                SewingKit = ( SewingKit + ((BaseTool)o).UsesRemaining );
                                                ((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 1 );
                                        }
                                }
                                else if ( o is Saw || o is Hammer || o is Inshave || o is Froe || o is DrawKnife || o is DovetailSaw || o is JointingPlane || o is MouldingPlane || o is SmoothingPlane || o is Scorp)
                                {
                                        if ( Saw >= 10000 )
                                                from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
                                                Saw = ( Saw + ((BaseTool)o).UsesRemaining );
                                                ((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 1 );
                                        }
                                }
                                else if ( o is TinkerTools )
                                {
                                        if ( TinkerTools >= 10000 )
                                                from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
                                                TinkerTools = ( TinkerTools + ((BaseTool)o).UsesRemaining );
                                                ((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 1 );
                                        }
                                }
                                else if ( o is ScribesPen )
                                {
                                        if ( ScribesPen >= 10000 )
                                                from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
                                                ScribesPen = ( ScribesPen + ((BaseTool)o).UsesRemaining );
                                                ((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 1 );
                                        }
                                }
                                else if ( o is MortarPestle )
                                {
                                        if ( MortarPestle >= 10000 )
                                                from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
                                                MortarPestle = ( MortarPestle + ((BaseTool)o).UsesRemaining );
                                                ((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 1 );
                                        }
                                }
                                else if ( o is Tongs )
                                {
                                        if ( Tongs >= 10000 )
                                                from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
                                                Tongs = ( Tongs + ((BaseTool)o).UsesRemaining );
                                                ((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 1 );
                                        }
                                }
                                else if ( o is FletcherTools )
                                {
                                        if ( FletcherTools >= 10000 )
                                                from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
                                                FletcherTools = ( FletcherTools + ((BaseTool)o).UsesRemaining );
                                                ((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 1 );
                                        }
                                }

                                else if ( o is SmithHammer || o is SledgeHammer )
                                {
                                        if ( SmithHammer >= 10000 )
                                                from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
                                                SmithHammer = ( SmithHammer + ((BaseTool)o).UsesRemaining );
                                                ((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 1 );
                                        }
                                }
                                else if ( o is MalletAndChisel )
                                {
                                        if ( MalletAndChisel >= 10000 )
                                                from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
                                                MalletAndChisel = ( MalletAndChisel + ((BaseTool)o).UsesRemaining );
                                                ((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 1 );
                                        }
                                }
                        /*        else if ( o is Extra2 )
                                {
                                        if ( Extra2 >= 10000 )
                                                from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
                                                Extra2 = ( Extra2 + ((BaseTool)o).UsesRemaining );
                                                ((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 1 );
                                        }
                                } */
                                else if ( o is Shovel )
                                {
                                        if ( Shovel >= 10000 )
	                                       from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
					
                                                Shovel = ( Shovel + ((BaseHarvestTool)o).UsesRemaining );
						((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 2 );
                                        }
                                }
                                else if ( o is Hatchet )
                                {
                                        if ( Hatchet >= 10000 )
                                                from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
                                                Hatchet = ( Hatchet + ((BaseAxe)o).UsesRemaining );
                                                ((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 2 );
                                        }
                                }
                                else if ( o is Axe )
                                {
                                        if ( Axe >= 10000 )
	                                       from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
                                                Axe = ( Axe + ((BaseAxe)o).UsesRemaining );
						((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 2 );
                                        }
                                }

                         /*       else if ( o is Extra1 )
                                {
                                        if ( Extra1 >= 10000 )
                                                from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
                                                Extra1 = ( Extra1 + ((BaseAxe)o).UsesRemaining );
                                                ((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 2 );
                                        }
                                } */
                                else if ( o is GargoylesPickaxe )
                                {
                                        if ( GargoylesPickaxe >= 10000 )
	                                       from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
                                                GargoylesPickaxe = ( GargoylesPickaxe + ((BaseAxe)o).UsesRemaining );
						((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 2 );
                                        }
				}
                                else if ( o is ProspectorsTool )
                                {
                                        if ( ProspectorsTool >= 10000 )
	                                       from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
                                                ProspectorsTool = ( ProspectorsTool + ((ProspectorsTool)o).UsesRemaining );
						((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 2 );
                                        }
				}
                                else if ( o is SturdyShovel )
                                {
                                        if ( SturdyShovel >= 10000 )
	                                       from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
                                                SturdyShovel = ( SturdyShovel + ((BaseHarvestTool)o).UsesRemaining );
						((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 2 );
                                        }
				}
                                else if ( o is SturdyPickaxe )
                                {
                                        if ( SturdyPickaxe >= 10000 )
	                                       from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
                                                SturdyPickaxe = ( SturdyPickaxe + ((BaseAxe)o).UsesRemaining );
						((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 2 );
                                        }
				}
                                else if ( o is Pickaxe )
                                {
                                        if ( Pickaxe >= 10000 )
	                                       from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
                                                Pickaxe = ( Pickaxe + ((BaseAxe)o).UsesRemaining );
						((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 2 );
                                        }
				}
                                else if ( o is PowderOfTemperament )
                                {
                                        if ( PowderOfTemperament >= 10000 )
	                                       from.SendMessage( "That tool type is too full to add more." );
                                        else
                                        {
                                                PowderOfTemperament = ( PowderOfTemperament + ((PowderOfTemperament)o).UsesRemaining );
						((Item)o).Delete();
                                                this.added = true; //aa
                                                BeginCombine( from, 2 );
                                        }
				}

//aa
				else 
				{
					from.SendMessage( "That does not belong in this box!" );
//aa**
					dropflag = false;
					added = false;
				}
//aa end

                        }
                        else
                        {
                                from.SendLocalizedMessage( 1045158 ); // You must have the item in your backpack to target it.
                        }
                }

                public ToolBox( Serial serial ) : base( serial )
                {
                }

                                public override void Serialize( GenericWriter writer ) 
		                { 
		                        base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version
		
		                        writer.Write( (int) m_Shovel); 
		                        writer.Write( (int) m_SewingKit); 
		                        writer.Write( (int) m_Saw); 
		                        writer.Write( (int) m_TinkerTools); 
		                        writer.Write( (int) m_ScribesPen); 
		                        writer.Write( (int) m_Tongs); 
		                        writer.Write( (int) m_FletcherTools); 
		                        writer.Write( (int) m_Hatchet); 
		                        writer.Write( (int) m_SmithHammer);
					writer.Write( (int) m_Axe);
					writer.Write( (int) m_MalletAndChisel);
					writer.Write( (int) m_MortarPestle);

					writer.Write( (int) m_Extra1);
					writer.Write( (int) m_GargoylesPickaxe);

					writer.Write( (int) m_ProspectorsTool);
					writer.Write( (int) m_Extra2);
					writer.Write( (int) m_PowderOfTemperament);
					writer.Write( (int) m_Extra3);
					writer.Write( (int) m_Extra4);
					writer.Write( (int) m_Extra5);
					writer.Write( (int) m_Extra6);
					writer.Write( (int) m_Extra7);
					writer.Write( (int) m_Extra8);
					writer.Write( (int) m_Extra9);
					writer.Write( (int) m_ExtraA);
					writer.Write( (int) m_ExtraB);
					writer.Write( (int) m_Pickaxe);
					writer.Write( (int) m_SturdyPickaxe);
					writer.Write( (int) m_SturdyShovel);
		
		                } 
		
		                public override void Deserialize( GenericReader reader ) 
		                { 
		                        base.Deserialize( reader ); 

					int version = reader.ReadInt();
		
		                        m_Shovel = reader.ReadInt(); 
		                        m_SewingKit = reader.ReadInt(); 
		                        m_Saw = reader.ReadInt(); 
		                        m_TinkerTools = reader.ReadInt(); 
		                        m_ScribesPen = reader.ReadInt(); 
		                        m_Tongs = reader.ReadInt(); 
		                        m_FletcherTools = reader.ReadInt(); 
		                        m_Hatchet = reader.ReadInt(); 
		                        m_SmithHammer = reader.ReadInt();
					m_Axe = reader.ReadInt();
					m_MalletAndChisel = reader.ReadInt();
					m_MortarPestle = reader.ReadInt();

						m_Extra1 = reader.ReadInt();
						m_GargoylesPickaxe = reader.ReadInt();					

						m_ProspectorsTool = reader.ReadInt();
						m_Extra2 = reader.ReadInt();
						m_PowderOfTemperament = reader.ReadInt();
						m_Extra3 = reader.ReadInt();
						m_Extra4 = reader.ReadInt();
						m_Extra5 = reader.ReadInt();
						m_Extra6 = reader.ReadInt();
						m_Extra7 = reader.ReadInt();
						m_Extra8 = reader.ReadInt();
						m_Extra9 = reader.ReadInt();
						m_ExtraA = reader.ReadInt();
						m_ExtraB = reader.ReadInt();
						m_Pickaxe = reader.ReadInt();
						m_SturdyPickaxe = reader.ReadInt();
						m_SturdyShovel = reader.ReadInt();

		                } 
		        } 
		} 
		

namespace Server.Items
{
        public class ToolBoxGump : Gump
        {
                private PlayerMobile m_From;
                private ToolBox m_Box;
		private int m_Page;

                public ToolBoxGump( PlayerMobile from, ToolBox box, int page ) : base( 25, 25 )
                {
                        m_From = from;
                        m_Box = box;
			m_Page = page;
			if ( page == 0 )
				page = 1;

                        m_From.CloseGump( typeof( ToolBoxGump ) );

                        AddPage( 0 );

			AddBackground( 50, 10, 455, 235, 9270 );
			AddBackground( 60, 20, 436, 216, 3000 );
AddItem(100, 25, 4148); // saw
AddItem(175, 25, 7864); // tinker
AddItem(325, 25, 5091); // smith hammer
AddItem(400, 25, 3739); // mortar

                        AddLabel( 250, 25, 1369, "Tool Box" );
                        AddButton( 335, 200, 5541, 5542, 30, GumpButtonType.Reply, 0 );
                        AddLabel( 375, 200, 1369, "Add Tool" );

if ( page == 2 )
                        AddButton( 150, 200, 9909, 9911, 101, GumpButtonType.Reply, 0 );
                        AddLabel( 100, 200, 1367, "Page 1" );
if ( page == 1 )
                        AddButton( 200, 200, 9903, 9905, 102, GumpButtonType.Reply, 0 );
                        AddLabel( 235, 200, 1367, "Page 2" );

			switch ( page )
			{
				case 1:
				{

                        AddLabel( 125, 50, 395, "TinkerTools" );
                        AddLabel( 230, 50, 0x480, box.TinkerTools.ToString() );
			if( box.TinkerTools > 0 )
				AddButton( 85, 50, 9903, 9905, 4, GumpButtonType.Reply, 0 );

                        AddLabel( 125, 75, 395, "Sewing Kit" );
                        AddLabel( 230, 75, 0x480, box.SewingKit.ToString() );
			if( box.SewingKit > 0 )
				AddButton( 85, 75, 9903, 9905, 2, GumpButtonType.Reply, 0 );

                        AddLabel( 125, 100, 395, "Carpentry" );
                        AddLabel( 230, 100, 0x480, box.Saw.ToString() );
			if( box.Saw > 0 )
				AddButton( 85, 100, 9903, 9905, 3, GumpButtonType.Reply, 0 );

                        AddLabel( 125, 125, 395, "FletcherTools" );
                        AddLabel( 230, 125, 0x480, box.FletcherTools.ToString() );
			if( box.FletcherTools > 0 )
				AddButton( 85, 125, 9903, 9905, 9, GumpButtonType.Reply, 0 );

                        AddLabel( 125, 150, 395, "Scribes Pen" );
                        AddLabel( 230, 150, 0x480, box.ScribesPen.ToString() );
			if( box.ScribesPen > 0 )
				AddButton( 85, 150, 9903, 9905, 5, GumpButtonType.Reply, 0 );


                        AddLabel( 325, 50, 395, "Tongs" );
                        AddLabel( 430, 50, 0x480, box.Tongs.ToString() );
			if( box.Tongs > 0 )
				AddButton( 285, 50, 9903, 9905, 6, GumpButtonType.Reply, 0 );

                        AddLabel( 325, 75, 395, "SmithHammer" );
                        AddLabel( 430, 75, 0x480, box.SmithHammer.ToString() );
			if( box.SmithHammer > 0 )
				AddButton( 285, 75, 9903, 9905, 7, GumpButtonType.Reply, 0 );


                        AddLabel( 325, 100, 395, "MalletAndChisel" );
                        AddLabel( 430, 100, 0x480, box.MalletAndChisel.ToString() );
			if( box.MalletAndChisel > 0 )
				AddButton( 285, 100, 9903, 9905, 8, GumpButtonType.Reply, 0 );

                        AddLabel( 325, 125, 395, "MortarPestle" );
                        AddLabel( 430, 125, 0x480, box.MortarPestle.ToString() );
			if( box.MortarPestle > 0 )
				AddButton( 285, 125, 9903, 9905, 10, GumpButtonType.Reply, 0 );

                   //     AddLabel( 325, 150, 395, "MarbleChisels" );
                   //     AddLabel( 430, 150, 0x480, box.Extra2.ToString() );
		//	if( box.Extra2 > 0 )
		//		AddButton( 285, 150, 9903, 9905, 25, GumpButtonType.Reply, 0 );

					break;
				} // case1
//AddPage( 2 );
				case 2:
				{

                        AddLabel( 125, 50, 395, "Shovel" );
                        AddLabel( 230, 50, 0x480, box.Shovel.ToString() );
			if( box.Shovel > 0 )
				AddButton( 85, 50, 9903, 9905, 1, GumpButtonType.Reply, 0 );

                        AddLabel( 125, 75, 395, "SturdyShovel" );
                        AddLabel( 230, 75, 0x480, box.SturdyShovel.ToString() );
			if( box.SturdyShovel > 0 )
				AddButton( 85, 75, 9903, 9905, 22, GumpButtonType.Reply, 0 );

                        AddLabel( 125, 100, 395, "Pickaxe" );
                        AddLabel( 230, 100, 0x480, box.Pickaxe.ToString() );
			if( box.Pickaxe > 0 )
				AddButton( 85, 100, 9903, 9905, 24, GumpButtonType.Reply, 0 );

                        AddLabel( 125, 125, 395, "SturdyPickaxe" );
                        AddLabel( 230, 125, 0x480, box.SturdyPickaxe.ToString() );
			if( box.SturdyPickaxe > 0 )
				AddButton( 85, 125, 9903, 9905, 23, GumpButtonType.Reply, 0 );


                        AddLabel( 125, 150, 395, "GargoylesPickaxe" );
                        AddLabel( 230, 150, 0x480, box.GargoylesPickaxe.ToString() );
			if( box.GargoylesPickaxe > 0 )
				AddButton( 85, 150, 9903, 9905, 14, GumpButtonType.Reply, 0 );


			AddLabel( 325, 50, 395, "ProspectorsTool" );
                        AddLabel( 430, 50, 0x480, box.ProspectorsTool.ToString() );
			if( box.ProspectorsTool > 0 )
				AddButton( 285, 50, 9903, 9905, 21, GumpButtonType.Reply, 0 );

                        AddLabel( 325, 75, 395, "Hatchet" );
                        AddLabel( 430, 75, 0x480, box.Hatchet.ToString() );
			if( box.Hatchet > 0 )
				AddButton( 285, 75, 9903, 9905, 11, GumpButtonType.Reply, 0 );


                        AddLabel( 325, 100, 395, "Axe" );
                        AddLabel( 430, 100, 0x480, box.Axe.ToString() );
			if( box.Axe > 0 )
				AddButton( 285, 100, 9903, 9905, 12, GumpButtonType.Reply, 0 );

                 //       AddLabel( 325, 125, 395, "Extra1" );
                 //       AddLabel( 430, 125, 0x480, box.Extra1.ToString() );
		//	if( box.Extra1 > 0 )
		//		AddButton( 285, 125, 9903, 9905, 13, GumpButtonType.Reply, 0 );


                        AddLabel( 325, 150, 395, "TemperamentPwdr" );
                        AddLabel( 430, 150, 0x480, box.PowderOfTemperament.ToString() );
			if( box.PowderOfTemperament > 0 )
				AddButton( 285, 150, 9903, 9905, 15, GumpButtonType.Reply, 0 );
                        
					break;
				} //case2
			}
                        
                }

                public override void OnResponse( NetState sender, RelayInfo info )
                {
                        if ( m_Box.Deleted )
                                return;

			if ( info.ButtonID == 101 )
			{
				m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
			}
			if ( info.ButtonID == 102 )
			{
				m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
			}

                        if ( info.ButtonID == 1 )
                        {
                                if ( m_Box.Shovel > 0 )
                                {
                                        
                                        m_From.AddToBackpack( new Shovel(m_Box.Shovel) );
					m_Box.Shovel = ( 0 );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
                                }
                        }
                        if ( info.ButtonID == 2 )
                        {
                                if ( m_Box.SewingKit > 0 )
                                {

                                        m_From.AddToBackpack( new SewingKit(m_Box.SewingKit) );
                                        m_Box.SewingKit = ( 0 );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                        }
                        if ( info.ButtonID == 3 )
                        {
                                if ( m_Box.Saw > 0 )
                                {

                                        m_From.AddToBackpack( new Saw(m_Box.Saw) );
                                        m_Box.Saw = ( 0 );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                        }
                        if ( info.ButtonID == 4 )
                        {
                                if ( m_Box.TinkerTools > 0 )
                                {
                                        m_From.AddToBackpack( new TinkerTools(m_Box.TinkerTools) );
                                        m_Box.TinkerTools = ( 0 );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                        }
                        if ( info.ButtonID == 5 )
                        {
                                if ( m_Box.ScribesPen > 0 )
                                {

                                        m_From.AddToBackpack( new ScribesPen(m_Box.ScribesPen) );
                                        m_Box.ScribesPen = ( 0 );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                        }
                        if ( info.ButtonID == 6 )
                        {
                                if ( m_Box.Tongs > 0 )
                                {

                                        m_From.AddToBackpack( new Tongs(m_Box.Tongs) );
                                        m_Box.Tongs = ( 0 );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                        }
                        if ( info.ButtonID == 7 )
                        {
                                if ( m_Box.SmithHammer > 0 )
                                {
                                        m_From.AddToBackpack( new SmithHammer(m_Box.SmithHammer) );
                                        m_Box.SmithHammer = ( 0 );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                        }
                        if ( info.ButtonID == 8 )
                        {
                                if ( m_Box.MalletAndChisel > 0 )
                                {
                                        m_From.AddToBackpack( new MalletAndChisel(m_Box.MalletAndChisel) );
                                        m_Box.MalletAndChisel = ( 0 );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                        }
                        if ( info.ButtonID == 9 )
                        {
                                if ( m_Box.FletcherTools > 0 )
                                {
                                        m_From.AddToBackpack( new FletcherTools(m_Box.FletcherTools) );
                                        m_Box.FletcherTools = ( 0 );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                        }
                        if ( info.ButtonID == 10 )
                        {
                                if ( m_Box.MortarPestle > 0 )
                                {
                                        m_From.AddToBackpack( new MortarPestle(m_Box.MortarPestle) );
                                        m_Box.MortarPestle = ( 0 );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                        }
                        if ( info.ButtonID == 11 )
                        {
                                if ( m_Box.Hatchet > 0 )
                                {
					Hatchet hatchet = new Hatchet();
					hatchet.UsesRemaining = m_Box.Hatchet;
					hatchet.ShowUsesRemaining = true;
                                        m_From.AddToBackpack( hatchet );
                                        m_Box.Hatchet = 0;
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box , 2) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
                                }
                        }
			if ( info.ButtonID == 12 )
                        {
                                if ( m_Box.Axe > 0 )
                                {
					Axe axe = new Axe();
					axe.UsesRemaining = m_Box.Axe;
					axe.ShowUsesRemaining = true;
                                        m_From.AddToBackpack( axe );
                                        m_Box.Axe = 0;
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
                                }
                        }

               /*         if ( info.ButtonID == 13 )
                        {
                                if ( m_Box.Extra1 > 0 )
                                {
					Extra1 heraklescursedaxe = new Extra1();
					heraklescursedaxe.UsesRemaining = m_Box.Extra1;
					heraklescursedaxe.ShowUsesRemaining = true;
                                        m_From.AddToBackpack( heraklescursedaxe );
                                        m_Box.Extra1 = 0;
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
                                }
                        } */
			if ( info.ButtonID == 14 )
                        {
                                if ( m_Box.GargoylesPickaxe > 0 )
                                {
					GargoylesPickaxe gargoylespickaxe = new GargoylesPickaxe();
					gargoylespickaxe.UsesRemaining = m_Box.GargoylesPickaxe;
					gargoylespickaxe.ShowUsesRemaining = true;
                                        m_From.AddToBackpack( gargoylespickaxe );
                                        m_Box.GargoylesPickaxe = 0;
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
                                }
                        }

			if ( info.ButtonID == 15 )
                        {
                                if ( m_Box.PowderOfTemperament > 0 )
                                {
					PowderOfTemperament powderoftemperament = new PowderOfTemperament();
					powderoftemperament.UsesRemaining = m_Box.PowderOfTemperament;
					powderoftemperament.ShowUsesRemaining = true;
                                        m_From.AddToBackpack( powderoftemperament );
                                        m_Box.PowderOfTemperament = 0;
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
                                }
                        }

			if ( info.ButtonID == 21 )
                        {
                                if ( m_Box.ProspectorsTool > 0 )
                                {
					ProspectorsTool prospectorstool = new ProspectorsTool();
					prospectorstool.UsesRemaining = m_Box.ProspectorsTool;
					prospectorstool.ShowUsesRemaining = true;
                                        m_From.AddToBackpack( prospectorstool );
                                        m_Box.ProspectorsTool = 0;
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
                                }
                        }

			if ( info.ButtonID == 22 )
                        {
                                if ( m_Box.SturdyShovel > 0 )
                                {
					SturdyShovel sturdyshovel = new SturdyShovel();
					sturdyshovel.UsesRemaining = m_Box.SturdyShovel;
					sturdyshovel.ShowUsesRemaining = true;
                                        m_From.AddToBackpack( sturdyshovel );
                                        m_Box.SturdyShovel = 0;
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
                                }
                        }

			if ( info.ButtonID == 23 )
                        {
                                if ( m_Box.SturdyPickaxe > 0 )
                                {
					SturdyPickaxe sturdypickaxe = new SturdyPickaxe();
					sturdypickaxe.UsesRemaining = m_Box.SturdyPickaxe;
					sturdypickaxe.ShowUsesRemaining = true;
                                        m_From.AddToBackpack( sturdypickaxe );
                                        m_Box.SturdyPickaxe = 0;
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
                                }
                        }

			if ( info.ButtonID == 24 )
                        {
                                if ( m_Box.Pickaxe > 0 )
                                {
					Pickaxe pickaxe = new Pickaxe();
					pickaxe.UsesRemaining = m_Box.Pickaxe;
					pickaxe.ShowUsesRemaining = true;
                                        m_From.AddToBackpack( pickaxe );
                                        m_Box.Pickaxe = 0;
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 2 ) );
                                }
                        }

		/*	if ( info.ButtonID == 25 )
                        {
                                if ( m_Box.Extra2 > 0 )
                                {
					Extra2 marblecraftingchisels = new Extra2();
					marblecraftingchisels.UsesRemaining = m_Box.Extra2;
					marblecraftingchisels.ShowUsesRemaining = true;
                                        m_From.AddToBackpack( marblecraftingchisels );
                                        m_Box.Extra2 = 0;
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                                else
                                {
                                        m_From.SendMessage( "You do not have any of that tool!" );
                                        m_From.SendGump( new ToolBoxGump( m_From, m_Box, 1 ) );
                                }
                        } */


                        if ( info.ButtonID == 30 )
                        {
//aa**				m_From.SendGump( new ToolBoxGump( m_From, m_Box ) );
                                m_Box.BeginCombine( m_From, 1 );
                        }
                }
        }

}

namespace Server.Items
{
        public class ToolBoxTarget : Target
        {
                private ToolBox m_Box;

                public ToolBoxTarget( ToolBox box ) : base( 18, false, TargetFlags.None )
                {
                        m_Box = box;
                }

                protected override void OnTarget( Mobile from, object targeted )
                {
                        if ( m_Box.Deleted )
                                return;

                        m_Box.EndCombine( from, targeted );
                }
        }
}
