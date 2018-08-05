namespace SQLiteWrapper
{
	partial class f_ErrorWindow
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
			this.lbl_MessageTitle = new System.Windows.Forms.TextBox();
			this.pb_MessageTypeImage = new System.Windows.Forms.PictureBox();
			this.tb_MessageBody = new System.Windows.Forms.TextBox();
			this.btn_Copy = new System.Windows.Forms.Button();
			this.btn_Close = new System.Windows.Forms.Button();
			this.lbl_MessageTypeString = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pb_MessageTypeImage)).BeginInit();
			this.SuspendLayout();
			// 
			// lbl_MessageTitle
			// 
			this.lbl_MessageTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lbl_MessageTitle.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.lbl_MessageTitle.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_MessageTitle.Location = new System.Drawing.Point(118, 12);
			this.lbl_MessageTitle.Multiline = true;
			this.lbl_MessageTitle.Name = "lbl_MessageTitle";
			this.lbl_MessageTitle.ReadOnly = true;
			this.lbl_MessageTitle.Size = new System.Drawing.Size(455, 100);
			this.lbl_MessageTitle.TabIndex = 0;
			this.lbl_MessageTitle.Text = "Message!";
			// 
			// pb_MessageTypeImage
			// 
			this.pb_MessageTypeImage.Location = new System.Drawing.Point(12, 12);
			this.pb_MessageTypeImage.Name = "pb_MessageTypeImage";
			this.pb_MessageTypeImage.Size = new System.Drawing.Size(100, 100);
			this.pb_MessageTypeImage.TabIndex = 1;
			this.pb_MessageTypeImage.TabStop = false;
			// 
			// tb_MessageBody
			// 
			this.tb_MessageBody.AcceptsReturn = true;
			this.tb_MessageBody.AcceptsTab = true;
			this.tb_MessageBody.HideSelection = false;
			this.tb_MessageBody.Location = new System.Drawing.Point(12, 138);
			this.tb_MessageBody.Multiline = true;
			this.tb_MessageBody.Name = "tb_MessageBody";
			this.tb_MessageBody.ReadOnly = true;
			this.tb_MessageBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tb_MessageBody.Size = new System.Drawing.Size(561, 219);
			this.tb_MessageBody.TabIndex = 2;
			this.tb_MessageBody.TabStop = false;
			// 
			// btn_Copy
			// 
			this.btn_Copy.Location = new System.Drawing.Point(12, 363);
			this.btn_Copy.Name = "btn_Copy";
			this.btn_Copy.Size = new System.Drawing.Size(223, 23);
			this.btn_Copy.TabIndex = 3;
			this.btn_Copy.Text = "Copy Message to Clipboard";
			this.btn_Copy.UseVisualStyleBackColor = true;
			this.btn_Copy.Click += new System.EventHandler(this.btn_Copy_Click);
			// 
			// btn_Close
			// 
			this.btn_Close.Location = new System.Drawing.Point(350, 363);
			this.btn_Close.Name = "btn_Close";
			this.btn_Close.Size = new System.Drawing.Size(223, 23);
			this.btn_Close.TabIndex = 4;
			this.btn_Close.Text = "Close Message Window";
			this.btn_Close.UseVisualStyleBackColor = true;
			this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
			// 
			// lbl_MessageTypeString
			// 
			this.lbl_MessageTypeString.AutoSize = true;
			this.lbl_MessageTypeString.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lbl_MessageTypeString.Location = new System.Drawing.Point(12, 115);
			this.lbl_MessageTypeString.Name = "lbl_MessageTypeString";
			this.lbl_MessageTypeString.Size = new System.Drawing.Size(42, 14);
			this.lbl_MessageTypeString.TabIndex = 5;
			this.lbl_MessageTypeString.Text = "ERROR";
			// 
			// f_ErrorWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(585, 390);
			this.Controls.Add(this.lbl_MessageTypeString);
			this.Controls.Add(this.btn_Close);
			this.Controls.Add(this.btn_Copy);
			this.Controls.Add(this.tb_MessageBody);
			this.Controls.Add(this.pb_MessageTypeImage);
			this.Controls.Add(this.lbl_MessageTitle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "f_ErrorWindow";
			this.Text = "Message!";
			((System.ComponentModel.ISupportInitialize)(this.pb_MessageTypeImage)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox lbl_MessageTitle;
		private System.Windows.Forms.PictureBox pb_MessageTypeImage;
		private System.Windows.Forms.TextBox tb_MessageBody;
		private System.Windows.Forms.Button btn_Copy;
		private System.Windows.Forms.Button btn_Close;
		private System.Windows.Forms.Label lbl_MessageTypeString;
	}
}