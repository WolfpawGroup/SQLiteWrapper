using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLiteWrapper
{
	public partial class f_ErrorWindow : Form
	{
		public string		_messageHeader		{ get; set; } = "Message!";
		public string		_messageTitle		{ get; set; } = "Message!";
		public string		_messageBody		{ get; set; } = "";
		public bool			_useGlossyImages	{ get; set; } = false;
		public bool			_showOnlyException	{ get; set; } = true;
		public messageType	_messageType		{ get; set; } = messageType.Error;
		public Exception	_exception			{ get; set; } = null;
		DateTime			_messageTime;
		
		public f_ErrorWindow()
		{
			InitializeComponent();
			Load += F_ErrorWindow_Load;
		}

		private void F_ErrorWindow_Load(object sender, EventArgs e)
		{
			_messageTime = DateTime.Now;
			Bitmap bmp = new Bitmap(100,100);
			string str = "";
			Color c = Color.Red;

			switch (_messageType)
			{
				case messageType.Error:
					str = "ERROR";
					c = Color.Red;
					bmp = _useGlossyImages ? Properties.Resource.err_1 : Properties.Resource.err_0;
					break;

				case messageType.Warning:
					str = "WARNING";
					c = Color.Orange;
					bmp = _useGlossyImages ? Properties.Resource.warn_1 : Properties.Resource.warn_0;
					break;
					
				case messageType.Log:
					str = "LOG";
					c = Color.Blue;
					bmp = _useGlossyImages ? Properties.Resource.log_0 : Properties.Resource.log_1_gloss;
					break;

				default:
				case messageType.Information:
					str = "INFO";
					c = Color.Green;
					bmp = _useGlossyImages ? Properties.Resource.info_1 : Properties.Resource.info_0;
					break;
			}

			lbl_MessageTypeString.ForeColor = c;
			lbl_MessageTypeString.Text = str;
			pb_MessageTypeImage.Image = bmp;

			if (!_showOnlyException || _exception == null)
			{
				lbl_MessageTitle.Text = _messageTitle;
				tb_MessageBody.Text = _messageBody;
				Text = _messageHeader;

				if (_exception != null)
				{
					tb_MessageBody.AppendText(string.Format("\r\n\r\n==========Exception text==========\r\n\r\n{0}", _exception.ToString()));
				}
			}
			else
			{
				lbl_MessageTitle.Text = _exception.Message;
				tb_MessageBody.Text = _exception.ToString();
				Text = _messageHeader = _exception.Message;
			}
			

			
		}

		private void btn_Copy_Click(object sender, EventArgs e)
		{
			this.Invoke(new gettext(getText));
		}

		public delegate void gettext();
		public void getText()
		{
			Clipboard.SetText(
				string.Format("{0}\r\n\r\n============{1}==========\r\n\r\n{2}",
				_messageTitle,
				_messageTime.ToShortDateString() + " " + _messageTime.ToLongTimeString(),
				tb_MessageBody.Text));

			Text = _messageHeader + " - Copied to clipboard!";
		}

		private void btn_Close_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
