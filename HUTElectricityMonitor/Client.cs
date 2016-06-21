/*
 * Created by SharpDevelop.
 * User: nickdoth
 * Date: 2016/6/16
 * Time: 9:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using Rainmeter;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;


namespace HUTElectricityMonitor
{
	/// <summary>
	/// Description of Setting.
	/// </summary>
	public sealed class Client
	{
		private static Client instance = new Client();

		public static Client Instance {
			get {
//				if (instance == null) instance = new Client();
				return instance;
			}
		}
		
		public static void Start()
		{
			Settings.Load();
			if (Settings.Instance.MonitorNo == 0)
			{
				var setForm = new SettingForm();
				setForm.ShowDialog();
			}
			
			var t = new System.Timers.Timer(1800000);
			t.Elapsed += (sender, e) => 
			{
				API.Log(API.LogType.Notice, "Updating per half hour...");
				Client.Instance.UpdateState();
			};
			t.AutoReset = true;
			t.Enabled = true;
		}
		
		private int no = 492;
		
		public int No {
			get {
				return no;
			}
			set {
				no = value;
			}
		}
		
		public double Balance {get; set;}
		public double PowerRate {get; set;}
		public string MonitorName {get; set;}
		
		public bool IsPending = false;
		private NotifyIcon CliIcon;
		
		private Client()
		{
		}
		
		
		public void UpdateState()
		{
			No = Settings.Instance.MonitorNo;
			UpdateElectricityData();
		}
		
		public void UpdateElectricityData()
		{
			string url = "http://172.24.224.91:8068/XSCK/DN_E_Meter.aspx?LX=Student&&&TitleStr=&BH=" + No;
			var cli = new WebClient();
//			this.lbstat.Text = "Updating";
			
//			cli.DownloadStringCompleted += new DownloadStringCompletedEventHandler(OnDataRetrived);
//			cli.DownloadStringAsync(new Uri(url));
			cli.DownloadDataCompleted += new DownloadDataCompletedEventHandler(OnDataRetrived);
			IsPending = true;
			cli.DownloadDataAsync(new Uri(url));
			
		}
		
		void OnDataRetrived(object sender, DownloadDataCompletedEventArgs e)
		{

			IsPending = false;
			if (e.Error != null)
			{
				API.Log(API.LogType.Error, "Failed to get data: " + e.Error.Message);
				return;
			}
			
			var rePowerData = new Regex(@"剩余金额:([\.\d]+),剩余度数:([\.\d]+),", RegexOptions.ECMAScript);
			var reMoName = new Regex(@"<label class=""dxeBase_Office2003Olive"" id=""ctl00_ContentPlaceHolder1_ASPxRoundPanel1_AZDZ"">(.*)</label>");
			
			try
			{
				var res = Encoding.UTF8.GetString(e.Result);
				var maPowerData = rePowerData.Match(res);
				Rainmeter.API.Log(API.LogType.Notice, maPowerData.Groups.Count.ToString());
				Rainmeter.API.Log(API.LogType.Notice, maPowerData.Groups[2].Value);
			
			
				// 金额，千瓦时
				Balance = double.Parse(maPowerData.Groups[1].Value);
				PowerRate = double.Parse(maPowerData.Groups[2].Value);
				// 电表名
				MonitorName = reMoName.Match(res).Groups[1].Value;
				
				if (PowerRate < 5.0)
				{
					Rainmeter.API.Log(API.LogType.Error, "您的可用电量低于5度，请及时充值");
					ShowTooltip();
				}
			}
			catch (Exception ex)
			{
				API.Log(API.LogType.Error, "Failed to get data: " + ex.Message);
			}
			
		}
		
		void ShowTooltip()
		{
			if (CliIcon == null)
			{
				CliIcon = new NotifyIcon();
				CliIcon.Icon = new Icon(Environment.CurrentDirectory + "\\1.ico");
		        CliIcon.Text = "电费提示";
		        CliIcon.BalloonTipClosed += (sender, e) =>
		        {
		        	CliIcon.Visible = false;
		        };
			}
		        
	        CliIcon.Visible = true;
	        
	        CliIcon.ShowBalloonTip(100, "您的电费余额不足", "您的可用电量低于5度，请及时充值", ToolTipIcon.Warning);
		}
	}
	
//	public static class Util
//	{
//		public static Timer SetInterval(int interval, Func<int> cb)
//		{
//			var t = new Timer(interval);
//			t.Elapsed += new ElapsedEventHandler();
//			t.AutoReset = true;
//			t.Enabled = true;
//		}
//	}
}
