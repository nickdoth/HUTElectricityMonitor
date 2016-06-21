// Uncomment these only if you want to export GetString() or ExecuteBang().
#define DLLEXPORT_GETSTRING
#define DLLEXPORT_EXECUTEBANG

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Rainmeter;

// Overview: This is a blank canvas on which to build your plugin.

// Note: Measure.GetString, Plugin.GetString, Measure.ExecuteBang, and
// Plugin.ExecuteBang have been commented out. If you need GetString
// and/or ExecuteBang and you have read what they are used for from the
// SDK docs, uncomment the function(s). Otherwise leave them commented out
// (or get rid of them)!

namespace HUTElectricityMonitor
{
    internal enum MeasureType
    {
    	Balance,
    	PowerRate,
    	MonitorName,
    	MonitorNo
    }
	
	internal class Measure
    {
		private MeasureType measureType;
		
		internal Measure()
        {
        	
        }

        internal void Reload(Rainmeter.API api, ref double maxValue)
        { 
//        	var settingForm = new SettingForm();
//        	settingForm.ShowDialog();
			maxValue = 1000.0;
			var type = api.ReadString("Type", "").ToLowerInvariant();
			switch (type)
			{
				case "balance":
					measureType = MeasureType.Balance;
					break;
				case "powerrate":
					measureType = MeasureType.PowerRate;
					break;
				case "monitorname":
					measureType = MeasureType.MonitorName;
					break;
				case "monitorno":
					measureType = MeasureType.MonitorNo;
					break;
				default:
					Rainmeter.API.Log(API.LogType.Error, "Unknown measure type: " + type);
					break;
			}

        }

        internal double Update()
        {
        	switch (measureType)
        	{
        		case MeasureType.Balance:
        			return Client.Instance.Balance;
        		case MeasureType.PowerRate:
        			return Client.Instance.PowerRate;
        		case MeasureType.MonitorNo:
        			return Settings.Instance.MonitorNo;
			}
        	
        	return 0.0;
        }
        
#if DLLEXPORT_GETSTRING
        internal string GetString()
        {
        	if (measureType == MeasureType.MonitorName)
        	{
        		return Client.Instance.IsPending ? 
        			"正在查询..." : Client.Instance.MonitorName;
        	}
        	else
        	{
        		return Client.Instance.IsPending ? 
        			"-" : Update().ToString();
        	}
        }
#endif
        
#if DLLEXPORT_EXECUTEBANG
        internal void ExecuteBang(string args)
        {
        	var setForm = new SettingForm();
			setForm.ShowDialog();
        }
#endif
    }

    public static class Plugin
    {
#if DLLEXPORT_GETSTRING
        static IntPtr StringBuffer = IntPtr.Zero;
#endif

		static bool IsClientStarted;
        [DllExport]
        public static void Initialize(ref IntPtr data, IntPtr rm)
        {
        	if (!IsClientStarted)
        	{
        		Client.Start();
        		IsClientStarted = true;
        	}
        	data = GCHandle.ToIntPtr(GCHandle.Alloc(new Measure()));
        }

        [DllExport]
        public static void Finalize(IntPtr data)
        {
            GCHandle.FromIntPtr(data).Free();
            
#if DLLEXPORT_GETSTRING
            if (StringBuffer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(StringBuffer);
                StringBuffer = IntPtr.Zero;
            }
#endif
        }

        [DllExport]
        public static void Reload(IntPtr data, IntPtr rm, ref double maxValue)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            measure.Reload(new Rainmeter.API(rm), ref maxValue);
        }

        [DllExport]
        public static double Update(IntPtr data)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            return measure.Update();
//			return 2.51;
        }
        
#if DLLEXPORT_GETSTRING
        [DllExport]
        public static IntPtr GetString(IntPtr data)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            if (StringBuffer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(StringBuffer);
                StringBuffer = IntPtr.Zero;
            }

            string stringValue = measure.GetString();
            if (stringValue != null)
            {
                StringBuffer = Marshal.StringToHGlobalUni(stringValue);
            }

            return StringBuffer;
        }
#endif

#if DLLEXPORT_EXECUTEBANG
        [DllExport]
        public static void ExecuteBang(IntPtr data, IntPtr args)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            measure.ExecuteBang(Marshal.PtrToStringUni(args));
        }
#endif
    }
}