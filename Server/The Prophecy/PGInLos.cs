using Server;
using Server.Network;
using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
	public class PgInLOSScript
	{
		public static void PgInLOS(Mobile from, bool noSpeechLOS)
		{
			NewPgInLOS(from, noSpeechLOS);
			return;
			#region OLD
			/*
			if (from.PgVis == null)
				from.PgVis = new List<Mobile>();

			if (from.AccessLevel == AccessLevel.Player)
			{
				for (int i = 0; i < from.PgVis.Count; i++)
				{
					Mobile m = from.PgVis[i] as Mobile;

					if (m.PgVis == null)
						m.PgVis = new List<Mobile>();

					if ((m.Map != from.Map) || !Utility.InUpdateRange(new Point2D(m.X, m.Y), new Point2D(from.X, from.Y)))
					{
						if (m.PgVis.Contains(from))
							m.PgVis.Remove(from);

						if (from.PgVis.Contains(m))
							from.PgVis.Remove(m);
					}
				}
				if (from.Map != null)
				{
					IPooledEnumerable eable = from.Map.GetMobilesInRange(from.Location);
					foreach (Mobile m in eable)
					{
						if (m.PgVis == null)
							m.PgVis = new List<Mobile>();

						if (!noSpeechLOS && !m.InLOS(from))
						{
							if (!from.PgVis.Contains(m))
							{
								from.PgVis.Add(m);
							}
							if (!m.PgVis.Contains(from))
							{
								m.PgVis.Add(from);
							}
							Packet p1 = from.RemovePacket;
							Packet p2 = m.RemovePacket;
							m.Send(p1);
							from.Send(p2);
							//Console.WriteLine("PG IN LOS");
						}
						else if (from.PgVis.Contains(m))
						{
							from.PgVis.Remove(m);
							if (m.PgVis.Contains(from))
							{
								m.PgVis.Remove(from);
							}
							if (!from.Hidden)
							{
								m.Send(new MobileIncoming(m, from));
							}
							if (!m.Hidden)
							{
								from.Send(new MobileIncoming(from, m));
							}
							if (from.IsDeadBondedPet)
							{
								m.Send(new BondedStatus(0, from.Serial, 1));
							}
							if (m.IsDeadBondedPet)
							{
								from.Send(new BondedStatus(0, m.Serial, 1));
							}
						}
					}
					eable.Free();
				}
			}*/
			#endregion
		}

		public static void NewPgInLOS(Mobile from, bool noSpeechLOS)
		{
			if (from.PgVis == null)
				from.PgVis = new List<Mobile>();

			if (from.Map == Map.Felucca && from.AccessLevel == AccessLevel.Player)
			{
				//ALSO CREATURES ARE AFFECTED
				//Console.WriteLine("CHECKRANGE");
				for (int i = 0; i < from.PgVis.Count; ++i)//Check all Currently Visible Mobiles
				{
					Mobile m = from.PgVis[i];

					//IF not in Range Removes each other
					if ((m.Map != from.Map) || !Utility.InUpdateRange(m.Location, from.Location))
					{
						//Sanity check
						if (m.PgVis == null)
							m.PgVis = new List<Mobile>();
						if (m.PgVis.Contains(from))
							m.PgVis.Remove(from);

						if (from.PgVis.Contains(m))
							from.PgVis.Remove(m);
					}
				}
				//Console.WriteLine("from Player " + from.Player);
				IPooledEnumerable eable = from.Map.GetMobilesInRange(from.Location);
				foreach (Mobile m in eable)
				{
					if (m == from)
						continue;

					if (from.InLOS(m) || (!from.IsTPPlayer && from.NetState != null))//WE CAN SEE M
					{
						//Console.WriteLine("m WE CAN SEE " + m.Player);
						//Check if he was not in our PGVIS
						if (!from.PgVis.Contains(m) && from.CanSee(m))
						{
							from.PgVis.Add(m);
							if (from.NetState != null)//From is a player and need to see new things
								Mobile.SendNewMobileIncoming(from.NetState, m);
						}
						//SanityCheck
						if (m.PgVis == null)
							m.PgVis = new List<Mobile>();
						//Check if he has us in PGVIS
						if (!m.PgVis.Contains(from) && m.CanSee(from))
						{
							m.PgVis.Add(from);
							if (m.NetState != null)//M is a player and need to see new things
								Mobile.SendNewMobileIncoming(m.NetState, from);
						}
					}
					else // WE CANNOT SEE M
					{
						//Console.WriteLine("m WE CANNOT SEE " + m.Player);
						if (from.PgVis.Contains(m))
						{
							from.PgVis.Remove(m);
							from.Send(m.RemovePacket);//We remove who we don't see
						}
						//SanityCheck
						if (m.PgVis == null)
							m.PgVis = new List<Mobile>();
						if (m.PgVis.Contains(from))
						{
							m.PgVis.Remove(from);
							if (m.NetState != null)
								m.Send(from.RemovePacket);//We remove us to who doesn't see us
						}
					}
				}
				eable.Free();
			}
		}
	}
}