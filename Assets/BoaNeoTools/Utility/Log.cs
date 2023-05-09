using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BoaNeoTools.Utility
{
	public static class Log
	{
		private static object _logLock = new();
		private static Dictionary<string, StringBuilder> _logs = new();
		
		[Conditional("DEBUG")]
		public static void Debug(object message)
		{
			UnityEngine.Debug.Log(message);
		}

		[Conditional("DEBUG")]
		public static void AppendLine(string id, object message, int indent = 0)
		{
			Append(id,message,indent,true);
		}
		[Conditional("DEBUG")]
		public static void Append(string id, object message, int indent=0, bool endline=false)
		{
			lock (_logLock)
			{
				if (!_logs.TryGetValue(id, out StringBuilder sb))
				{
					sb = new StringBuilder();
					_logs[id] = sb;
				}

				if (indent > 0)
				{
					string spc = "\t";
					while (--indent > 0)
						spc += "\t";
					message = $"{spc}{message}";
				}
				if(endline)
					sb.AppendLine(message.ToString());
				else
					sb.Append(message);
			}
		}

		[Conditional("DEBUG")]
		public static void Flush(string id)
		{
			lock (_logLock)
			{
				if (_logs.TryGetValue(id, out StringBuilder sb))
				{
					Debug(sb.ToString());
					_logs.Remove(id);
				}
			}
		}

		public static long ElapsedTime(long t0=0, string expl=null)
		{
			long t = Stopwatch.GetTimestamp();
			if(expl!=null)
				Debug($"{expl} in {1000*(t - t0)/Stopwatch.Frequency}ms");
			return t;
		}	
		public static void Warn(object message)
		{
			UnityEngine.Debug.LogWarning(message);
		}
		public static void Error(object message)
		{
			UnityEngine.Debug.LogError(message);
			UnityEngine.Debug.Break();
		}
	}
}