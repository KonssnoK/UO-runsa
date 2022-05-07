/***************************************************************************
 *                            GumpHtmlLocalized.cs
 *                            -------------------
 *   begin                : May 1, 2002
 *   copyright            : (C) The RunUO Software Team
 *   email                : info@runuo.com
 *
 *   $Id: GumpHtmlLocalized.cs 4 2006-06-15 04:28:39Z mark $
 *
 ***************************************************************************/

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 2 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/

using System;
using Server.Network;

namespace Server.Gumps
{
	public enum KRGumpHtmlLocalizedType
	{
		Plain,
		Color,
		Args
	}

	public class KRGumpHtmlLocalized : GumpEntry
	{
		private int m_X, m_Y;
		private int m_Width, m_Height;
		private int m_Number;
		private string m_Args;
		private int m_Color;
		private bool m_Background, m_Scrollbar;

		private KRGumpHtmlLocalizedType m_Type;

		public int X
		{
			get
			{
				return m_X;
			}
			set
			{
				Delta( ref m_X, value );
			}
		}

		public int Y
		{
			get
			{
				return m_Y;
			}
			set
			{
				Delta( ref m_Y, value );
			}
		}

		public int Width
		{
			get
			{
				return m_Width;
			}
			set
			{
				Delta( ref m_Width, value );
			}
		}

		public int Height
		{
			get
			{
				return m_Height;
			}
			set
			{
				Delta( ref m_Height, value );
			}
		}

		public int Number
		{
			get
			{
				return m_Number;
			}
			set
			{
				Delta( ref m_Number, value );
			}
		}

		public string Args
		{
			get
			{
				return m_Args;
			}
			set
			{
				Delta( ref m_Args, value );
			}
		}

		public int Color
		{
			get
			{
				return m_Color;
			}
			set
			{
				Delta( ref m_Color, value );
			}
		}

		public bool Background
		{
			get
			{
				return m_Background;
			}
			set
			{
				Delta( ref m_Background, value );
			}
		}

		public bool Scrollbar
		{
			get
			{
				return m_Scrollbar;
			}
			set
			{
				Delta( ref m_Scrollbar, value );
			}
		}

		public KRGumpHtmlLocalizedType Type
		{
			get
			{
				return m_Type;
			}
			set
			{
				if ( m_Type != value )
				{
					m_Type = value;

					if ( Parent != null )
						Parent.Invalidate();
				}
			}
		}

		public KRGumpHtmlLocalized( int x, int y, int width, int height, int number, bool background, bool scrollbar )
		{
			m_X = x;
			m_Y = y;
			m_Width = width;
			m_Height = height;
			m_Number = number;
			m_Background = background;
			m_Scrollbar = scrollbar;

			m_Type = KRGumpHtmlLocalizedType.Plain;
		}

		public KRGumpHtmlLocalized( int x, int y, int width, int height, int number, int color, bool background, bool scrollbar )
		{
			m_X = x;
			m_Y = y;
			m_Width = width;
			m_Height = height;
			m_Number = number;
			m_Color = color;
			m_Background = background;
			m_Scrollbar = scrollbar;

			m_Type = KRGumpHtmlLocalizedType.Color;
		}

		public KRGumpHtmlLocalized( int x, int y, int width, int height, int number, string args, int color, bool background, bool scrollbar )
		{
			// Are multiple arguments unsupported? And what about non ASCII arguments?

			m_X = x;
			m_Y = y;
			m_Width = width;
			m_Height = height;
			m_Number = number;
			m_Args = args;
			m_Color = color;
			m_Background = background;
			m_Scrollbar = scrollbar;

			m_Type = KRGumpHtmlLocalizedType.Args;
		}

		public override string Compile()
		{
			switch ( m_Type )
			{
				case KRGumpHtmlLocalizedType.Plain:
					return String.Format( "{{ kr_xmfhtmlgump {0} {1} {2} {3} {4} {5} {6} }}", m_X, m_Y, m_Width, m_Height, m_Number, m_Background ? 1 : 0, m_Scrollbar ? 1 : 0 );

				case KRGumpHtmlLocalizedType.Color:
					return String.Format( "{{ kr_xmfhtmlgumpcolor {0} {1} {2} {3} {4} {5} {6} {7} }}", m_X, m_Y, m_Width, m_Height, m_Number, m_Background ? 1 : 0, m_Scrollbar ? 1 : 0, m_Color );

				default: // KRGumpHtmlLocalizedType.Args
					return String.Format( "{{ kr_xmfhtmltok {0} {1} {2} {3} {4} {5} {6} {7} @{8}@ }}", m_X, m_Y, m_Width, m_Height, m_Background ? 1 : 0, m_Scrollbar ? 1 : 0, m_Color, m_Number, m_Args );
			}
		}

		private static byte[] m_LayoutNamePlain = Gump.StringToBuffer( "kr_xmfhtmlgump" );
		private static byte[] m_LayoutNameColor = Gump.StringToBuffer( "kr_xmfhtmlgumpcolor" );
		private static byte[] m_LayoutNameArgs = Gump.StringToBuffer( "kr_xmfhtmltok" );

		public override void AppendTo( IGumpWriter disp )
		{
			switch ( m_Type )
			{
				case KRGumpHtmlLocalizedType.Plain:
				{
					disp.AppendLayout( m_LayoutNamePlain );

					disp.AppendLayout( m_X );
					disp.AppendLayout( m_Y );
					disp.AppendLayout( m_Width );
					disp.AppendLayout( m_Height );
					disp.AppendLayout( m_Number );
					disp.AppendLayout( m_Background );
					disp.AppendLayout( m_Scrollbar );

					break;
				}

				case KRGumpHtmlLocalizedType.Color:
				{
					disp.AppendLayout( m_LayoutNameColor );

					disp.AppendLayout( m_X );
					disp.AppendLayout( m_Y );
					disp.AppendLayout( m_Width );
					disp.AppendLayout( m_Height );
					disp.AppendLayout( m_Number );
					disp.AppendLayout( m_Background );
					disp.AppendLayout( m_Scrollbar );
					disp.AppendLayout( m_Color );

					break;
				}

				case KRGumpHtmlLocalizedType.Args:
				{
					disp.AppendLayout( m_LayoutNameArgs );

					disp.AppendLayout( m_X );
					disp.AppendLayout( m_Y );
					disp.AppendLayout( m_Width );
					disp.AppendLayout( m_Height );
					disp.AppendLayout( m_Background );
					disp.AppendLayout( m_Scrollbar );
					disp.AppendLayout( m_Color );
					disp.AppendLayout( m_Number );
					disp.AppendLayout( m_Args );

					break;
				}
			}
		}
	}
}