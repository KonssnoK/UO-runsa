/***************************************************************************
 *                               ItemBounds.cs
 *                            -------------------
 *   begin                : May 1, 2002
 *   copyright            : (C) The RunUO Software Team
 *   email                : info@runuo.com
 *
 *   $Id$
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
using System.IO;

namespace Server
{
	public class ItemBounds
	{
		private static Rectangle2D[] m_Bounds;

		public static Rectangle2D[] Table
		{
			get
			{
				return m_Bounds;
			}
		}

		static ItemBounds()
		{
			if (File.Exists("Data/Binary/Bounds_latest.bin"))
			{
				using (FileStream fs = new FileStream("Data/Binary/Bounds_latest.bin", FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					BinaryReader bin = new BinaryReader( fs );

					#region SA
					m_Bounds = new Rectangle2D[0x8000];

					for (int i = 0; i < 0x8000; ++i)
					{
						int xMin = bin.ReadInt16();
						int yMin = bin.ReadInt16();
						int xMax = bin.ReadInt16();
						int yMax = bin.ReadInt16();

						m_Bounds[i].Set( xMin, yMin, (xMax - xMin) + 1, (yMax - yMin) + 1 );
					}
					#endregion

					bin.Close();
				}
			}
			else
			{
				Console.WriteLine("Warning: Data/Binary/Bounds_latest.bin does not exist");

				#region SA
				m_Bounds = new Rectangle2D[0x8000];
				#endregion

				#region SA
				bool useUltimaDll = false;

				if (useUltimaDll)
				{
					m_Bounds = new Rectangle2D[0x8000];

					using (FileStream fs = new FileStream("Data/Binary/Bounds_ToRename.bin", FileMode.CreateNew, FileAccess.Write, FileShare.Write))
					{
						using (BinaryWriter b = new BinaryWriter(fs))
						{
							for (int i = 0; i < 0x8000; ++i)
							{
								int xMin, yMin, xMax, yMax;
								Item.Measure(Item.GetBitmap(i), out xMin, out yMin, out xMax, out yMax);
								//m_Bounds[i].Set(xMin, yMin, (xMax - xMin) + 1, (yMax - yMin) + 1);

								b.Write((ushort)xMin);
								b.Write((ushort)yMin);
								b.Write((ushort)xMax);
								b.Write((ushort)yMax);
							}
						}
					}
				}
				#endregion
			}
		}
	}
}