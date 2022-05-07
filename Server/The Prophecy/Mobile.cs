using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Network;

namespace Server
{
	public partial class Mobile : IEntity, IHued, IComparable<Mobile>, ISerializable
	{
		// mod elmi
		public List<Mobile> PgVis;

		/// <summary>
		/// True if Player is NewPlayerMobile
		/// </summary>
		[CommandProperty(AccessLevel.GameMaster)]
		public virtual bool IsTPPlayer
		{
			get { return false; }
		}

		private bool m_Frastornato;
		[CommandProperty(AccessLevel.GameMaster)]
		public bool Frastornato
		{
			get { return m_Frastornato; }
			set
			{
				if (value != m_Frastornato)
				{
					m_Frastornato = value;
					if (m_Frastornato)
					{
						Timer.DelayCall(TimeSpan.FromSeconds(20), new TimerCallback(FrastornatoEnd));
					}
					InvalidateProperties();
				}
			}
		}

		private void FrastornatoEnd()
		{
			if (!Deleted)
				Frastornato = false;
		}


		#region SA
		private RunningFlags m_RunningFlags = RunningFlags.None;
		protected bool GetRunningFlag(RunningFlags flag)
		{
			return ((m_RunningFlags & flag) != 0);
		}
		protected void SetRunningFlag(RunningFlags flag, bool on)
		{
			m_RunningFlags = (on ? m_RunningFlags | flag : m_RunningFlags & ~flag);
		}

		public void SwitchSpeedControl(RunningFlags flag, bool active)
		{
			if (GetRunningFlag(flag) == active)
				return;//Flag is already at that status, no mod needed

			SetRunningFlag(flag, active);

			if (active)
			{
				Packet p = SpeedHandler.GetSpeedType(flag);//Override current SpeedControl
				if (p == null)
					return;
				if (p == SpeedControl.MountSpeed && GetWalkingFlag())//We cannot override any WALKING Malus
				{
					//Console.WriteLine("Malus not overridden");
					return;
				}
				else
					Send(p);
				return;
			}
			else
			{
				bool restore = false;

				RunningFlags torestore = RunningFlags.None;
				foreach (RunningFlags f in Enum.GetValues(typeof(RunningFlags)))
				{
					if (f == RunningFlags.None)
						continue;
					if (GetRunningFlag(f))
					{
						torestore = f;
						//Console.WriteLine(f.ToString());
						restore = true;
						break;
					}
				}

				if (restore)
				{
					//Console.WriteLine("Passive restoring");
					Packet p = SpeedHandler.GetSpeedType(torestore);//Restore any overridden SpeedControl
					if (p != null)
					{
						if (p == SpeedControl.MountSpeed && GetWalkingFlag())//Check if there are Malus Controls
							Send(SpeedControl.WalkSpeed);
						else
							Send(p);
					}
				}
				else
					Send(SpeedControl.Disable);
			}
		}

		public bool GetWalkingFlag()
		{
			foreach (RunningFlags f in Enum.GetValues(typeof(RunningFlags)))
			{
				if (f == RunningFlags.None)
					continue;
				if (GetRunningFlag(f) && SpeedHandler.GetSpeedType(f) == SpeedControl.WalkSpeed)
					return true;
			}
			return false;
		}
		#endregion

		public Type PossessionType = null;
	}
}