/*
 * Created by SharpDevelop.
 * User: nickdoth
 * Date: 2016/6/20
 * Time: 10:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Xml.Serialization;

namespace HUTElectricityMonitor
{
	/// <summary>
	/// Description of Settings.
	/// </summary>
	[Serializable]
	public class Settings
	{
		public static Settings Instance = new Settings();
		public static string Path = Environment.CurrentDirectory + @"\HUTElectricity.xml";
		
		static public void Save()
		{
			
			FileStream setFile;
			var xs = new XmlSerializer(typeof(Settings));
			
			if (!File.Exists(Path))
			{
				setFile = File.Create(Path);
			}
			else
			{
				setFile = File.OpenWrite(Path);
			}
			
			xs.Serialize(setFile, Instance);
			setFile.Close();
			Client.Instance.UpdateState();
		}
		
		static public void Load()
		{
			if (!File.Exists(Path))
			{
				Save();
				return;
			}
			var setfile = File.OpenRead(Path);
			var xs = new XmlSerializer(typeof(Settings));
			Instance = xs.Deserialize(setfile) as Settings;
			setfile.Close();
			Client.Instance.UpdateState();
		}
		
		public int MonitorNo = 0;
		public string MonitorName = "-";
	}
}
