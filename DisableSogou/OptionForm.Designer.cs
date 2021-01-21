
namespace DisableSogou
{
	partial class OptionForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionForm));
			this.chkAutorun = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.chkSilent = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// chkAutorun
			// 
			this.chkAutorun.AutoSize = true;
			this.chkAutorun.Location = new System.Drawing.Point(15, 64);
			this.chkAutorun.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.chkAutorun.Name = "chkAutorun";
			this.chkAutorun.Size = new System.Drawing.Size(99, 21);
			this.chkAutorun.TabIndex = 3;
			this.chkAutorun.Text = "开机自动运行";
			this.chkAutorun.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(128, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "搜狗输入法安装目录：";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(399, 29);
			this.btnBrowse.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(26, 26);
			this.btnBrowse.TabIndex = 2;
			this.btnBrowse.Text = "...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// txtPath
			// 
			this.txtPath.BackColor = System.Drawing.SystemColors.Window;
			this.txtPath.Location = new System.Drawing.Point(15, 31);
			this.txtPath.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.txtPath.Name = "txtPath";
			this.txtPath.ReadOnly = true;
			this.txtPath.Size = new System.Drawing.Size(381, 23);
			this.txtPath.TabIndex = 1;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(107, 108);
			this.btnOK.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(87, 29);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "确定";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(247, 108);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(87, 29);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// chkSilent
			// 
			this.chkSilent.AutoSize = true;
			this.chkSilent.Location = new System.Drawing.Point(139, 64);
			this.chkSilent.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.chkSilent.Name = "chkSilent";
			this.chkSilent.Size = new System.Drawing.Size(99, 21);
			this.chkSilent.TabIndex = 4;
			this.chkSilent.Text = "隐藏程序图标";
			this.chkSilent.UseVisualStyleBackColor = true;
			this.chkSilent.Click += new System.EventHandler(this.chkSilent_Click);
			// 
			// OptionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(442, 156);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.chkSilent);
			this.Controls.Add(this.chkAutorun);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.txtPath);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DisableSogou - 设置";
			this.Load += new System.EventHandler(this.OptionForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox chkAutorun;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox chkSilent;
	}
}