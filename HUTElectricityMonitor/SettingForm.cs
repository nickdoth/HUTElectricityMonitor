/*
 * Created by SharpDevelop.
 * User: nickdoth
 * Date: 2016/6/15
 * Time: 17:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.IO;
//using System.Web;
using System.Net;

namespace HUTElectricityMonitor
{
	/// <summary>
	/// Description of SettingForm.
	/// </summary>
	public partial class SettingForm : Form
	{
		public SettingForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			
			
			var dt = new DataTable();
			dt.Columns.Add("no", typeof(int));
			dt.Columns.Add("desp", typeof(string));
			var fetchFile = File.OpenText("D:\\fetch.txt");
			
			while (!fetchFile.EndOfStream)
			{
				var line = fetchFile.ReadLine();
				var csvRow = line.Split(new char[]{','});
				int no;
				if (Int32.TryParse(csvRow[0], out no))
				{
					var dr = dt.NewRow();
					dr[0] = no;
					dr[1] = csvRow[1];
					dt.Rows.Add(dr);
				}
			}
			
			this.comboBoxMonitor.DataSource = dt;	
			this.comboBoxMonitor.DisplayMember = "desp";
			this.comboBoxMonitor.ValueMember = "no";
			this.comboBoxMonitor.SelectedValue = Settings.Instance.MonitorNo;
		}
		
		void ComboBoxMonitorSelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.comboBoxMonitor.SelectedValue != null)
			{
				this.selectedNo.Text = this.comboBoxMonitor.SelectedValue.ToString();
//				UpdateElectricityData();
			}
			else
			{
				this.selectedNo.Text = "-";
			}
//			this.comboBoxMonitor.;
		}
		void BtnOkClick(object sender, EventArgs e)
		{
			if (this.comboBoxMonitor.SelectedValue != null)
			{
				var value = (int) this.comboBoxMonitor.SelectedValue;
				Settings.Instance.MonitorNo = value;
				
//				var dr = (this.comboBoxMonitor.SelectedItem) as DataRow;
//				Settings.Instance.MonitorName = (string)dr[1];
//				Rainmeter.API.Log(Rainmeter.API.LogType.Notice, (string)dr[1]);
				Settings.Save();
			}
			this.Close();
		}
		


	}
}
