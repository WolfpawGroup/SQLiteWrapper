using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteWrapper
{
	public class c_ErrorHandler
	{
		public bool on { get; set; } = true;
		public bool showWindow { get; set; } = false;
		public logLevel loggingLevel { get; set; } = logLevel.level_0;

		public bool Error(string errorString, Exception _exception, bool showOnlyException)
		{
			if (on && (int)loggingLevel > 0)
			{
				Console.ForegroundColor = ConsoleColor.Red;

				if (_exception == null || !showOnlyException)
				{
					Console.Error.WriteLine("ERROR: " + errorString);
				}

				if (_exception != null)
				{
					Console.Error.WriteLine(_exception.ToString());
				}

				Console.ForegroundColor = ConsoleColor.Gray;

				if (showWindow)
				{
					f_ErrorWindow errW = new f_ErrorWindow()
					{
						_messageType = messageType.Error,
						_messageHeader = "ERROR",
						_messageTitle = "ERROR MESSAGE FROM SYSTEM",
						_messageBody = errorString,
						_exception = _exception,
						_showOnlyException = showOnlyException
					};
					errW.ShowDialog();
				}

				return true;
			}
			else
			{
				return false;
			}
		}

		public bool Warning(string warnstr)
		{
			if (on && (int)loggingLevel > 1)
			{
				Console.WriteLine("WARNING: " + warnstr);

				if (showWindow)
				{
					f_ErrorWindow errW = new f_ErrorWindow();

					//TODO:SHOW ERROR
				}

				return true;
			}
			else
			{
				return false;
			}
		}


	}

	public enum logLevel
	{
		level_0,	//No		logging
		level_1,	//Error		logging
		level_2,	//Warning	logging
		level_3,	//Info		logging
		level_4,    //Info		logging
		level_5,    //Info		logging
		level_6     //Trivial	logging
	}

	public enum messageType
	{
		Error,			//Level_1 or higher		//Errors, important, possibly lethal crashes	//E.g.:Faulty parameters, unexpected null
		Warning,		//Level_2 or higher		//Non lethal warnings							//E.g.:Missing file, missing permission
		Information,	//Level_3 or higher		//Information from important to unimportant		//E.g.:Empty table, empty query
		Log				//Level_6				//Trivial data (verbose)						//E.g.:Starting program, building table...
	}
}
