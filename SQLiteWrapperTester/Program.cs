using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteWrapper;

namespace SQLiteWrapperTester
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			SQLiteTable sqlt = new SQLiteTable("test_table") { eh = new c_ErrorHandler() { loggingLevel = logLevel.level_3, showWindow = true } };
			sqlt.addColumn(new SQLiteColumn { columnName = "id", dataType = SQLiteDataType.INTEGER, pimaryKey = true, autoIncrement = true });
			sqlt.addColumn(new SQLiteColumn { columnName = "artist_name", dataType = SQLiteDataType.TEXT });
			sqlt.addColumn(new SQLiteColumn { columnName = "col1", dataType = SQLiteDataType.CHARACTER, dataLength = 50 });
			sqlt.addColumn(new SQLiteColumn { columnName = "col2", dataType = SQLiteDataType.DATE, foreignKey = true, foreignTable = "test_table_2", foreignColumn = "col44" });
			sqlt.addColumn(new SQLiteColumn { columnName = "col3", dataType = SQLiteDataType.TINYINT });
			sqlt.addColumn(new SQLiteColumn { columnName = "col4", dataType = SQLiteDataType.BOOLEAN, columnComment = "this is a boolean" });

			try
			{
				throw new InsufficientMemoryException();
			}
			catch(Exception ex)
			{
				sqlt.eh.Error(ex.ToString());
			}

			
			Console.Read();
		}
	}
}
