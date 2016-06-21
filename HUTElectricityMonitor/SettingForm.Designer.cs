/*
 * Created by SharpDevelop.
 * User: nickdoth
 * Date: 2016/6/15
 * Time: 17:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace HUTElectricityMonitor
{
	partial class SettingForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ComboBox comboBoxMonitor;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label selectedNo;
		private System.Windows.Forms.Label lbstat;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.comboBoxMonitor = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.selectedNo = new System.Windows.Forms.Label();
			this.lbstat = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(108, 177);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "确定";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.BtnOkClick);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(189, 177);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// comboBoxMonitor
			// 
			this.comboBoxMonitor.FormattingEnabled = true;
			this.comboBoxMonitor.Location = new System.Drawing.Point(12, 61);
			this.comboBoxMonitor.Name = "comboBoxMonitor";
			this.comboBoxMonitor.Size = new System.Drawing.Size(225, 20);
			this.comboBoxMonitor.TabIndex = 2;
			this.comboBoxMonitor.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxMonitorSelectedIndexChanged);
			this.comboBoxMonitor.TextUpdate += new System.EventHandler(this.ComboBoxMonitorSelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "选择电表：";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 100);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 4;
			this.label2.Text = "当前电表编号：";
			// 
			// selectedNo
			// 
			this.selectedNo.Location = new System.Drawing.Point(13, 127);
			this.selectedNo.Name = "selectedNo";
			this.selectedNo.Size = new System.Drawing.Size(100, 23);
			this.selectedNo.TabIndex = 5;
			this.selectedNo.Text = "-";
			// 
			// lbstat
			// 
			this.lbstat.Location = new System.Drawing.Point(12, 182);
			this.lbstat.Name = "lbstat";
			this.lbstat.Size = new System.Drawing.Size(81, 18);
			this.lbstat.TabIndex = 6;
			this.lbstat.Text = "***";
			// 
			// SettingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
			this.ClientSize = new System.Drawing.Size(287, 214);
			this.Controls.Add(this.lbstat);
			this.Controls.Add(this.selectedNo);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBoxMonitor);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "设置";
			this.ResumeLayout(false);

		}
	}
}
