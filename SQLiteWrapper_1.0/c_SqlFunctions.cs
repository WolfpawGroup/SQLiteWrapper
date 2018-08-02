using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Data.SQLite;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace SQLiteWrapper
{
	public class c_SqlFunctions
	{
		/// <summary>SQLite Connection to be used with the functions</summary>
		public SQLiteConnection sqlc { get; set; }
		private List<int> connectedList = new List<int> { 1, 4, 8 };

		/// <summary>Creates the database file if it doesn't already exist</summary>
		/// <param name="dbname">Name/path of the file to be created</param>
		/// <returns>true if file was created</returns>
		public bool createDbFile(string dbname)
		{
			if(dbname == "") { dbname = "db.sqlite"; }

			if (File.Exists(dbname)) { return true; }

			try
			{
				File.Create(dbname).Close();
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return false;
			}

			return true;
		}
		/// <summary>Creates the database file if it doesn't already exist with the default "db.sqlite" name</summary>
		/// <returns>true if file was created</returns>
		public bool createDbFile() { return createDbFile(""); }

		public bool createInnerConnection(string dbname)
		{
			if (dbname == "") { dbname = "db.sqlite"; }

			try
			{
				sqlc = connectToDB(dbname);
				return true;
			}
			catch
			{
				return false;
			}
		}
		public bool createInnerConnection()
		{
			return createInnerConnection("");
		}

		/// <summary>Creates and opens connection with DB file</summary>
		/// <param name="dbname">Name/path of the file to be connected to</param>
		/// <returns>SQLiteConnection if connected, Null if not</returns>
		public SQLiteConnection connectToDB(string dbname)
		{
			SQLiteConnection sql = null;
			if (dbname == "") { dbname = "db.sqlite"; }

			try
			{
				sql = new SQLiteConnection("Data Source=" + dbname + ";Version=3;");
				sql.Open();
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

			return sql;
		}

		/// <summary>Creates and opens connection with DB file with the default "db.sqlite" name</summary>
		/// <returns>SQLiteConnection if connected, Null if not</returns>
		public SQLiteConnection connectToDB() { return connectToDB(""); }

		/// <summary>Checks the current sqlc connection if it exists and is not closed, connecting or broken</summary>
		/// <returns>true if connection open, false if doesn't exist or not open</returns>
		public bool checkDbConnected()
		{
			return sqlc == null ? false : (connectedList.Contains((int)sqlc.State) ? true : false);
		}

		/// <summary>Checks if a table can be found in the DB already</summary>
		/// <param name="tableName">Name of the table to find</param>
		/// <returns>true if table exists, false if table doesn't exist, null if error</returns>
		public bool? checkTableExists(string tableName)
		{
			if (!checkDbConnected()) { Console.WriteLine("ERROR:SQLC → SQL Connection NULL or not open"); return null; }

			SQLiteCommand sqlk = new SQLiteCommand(string.Format("SELECT count(*) FROM sqlite_master WHERE type='table' AND name='{0}';", tableName), sqlc);
			int i = 0;
			try
			{
				int.TryParse(sqlk.ExecuteScalar().ToString(), out i);
			}
			catch(Exception ex)
			{
				Console.WriteLine("ERROR:SQLC → SQL query returned error: \r\n" + ex.ToString());
				return null;
			}

			return i > 0;
		}

		/// <summary> Modular table creator function | Creates a new named table if that table doesn't yet exist in the database | Uses modular column definition for reusability</summary>
		/// <param name="tableName">Name of the table to be created</param>
		/// <param name="columns">Array of columns to be added in the newly created table. Type: SQLiteColumn</param>
		/// <returns>true if new table created</returns>
		public bool createTable(string tableName, SQLiteColumn[] columns)
		{
			if (!checkDbConnected()) { Console.WriteLine("ERROR:SQLC → SQL Connection NULL or not open"); return false; }
			object exist = checkTableExists(tableName);
			if (exist == null || (bool)exist) { return false; }

			StringBuilder tableBuilder = new StringBuilder("CREATE TABLE");
			tableBuilder.AppendFormat(" {0} \r\n" + 
											"(", tableName);

			foreach (SQLiteColumn c in columns)
			{
				tableBuilder.AppendFormat(" {0} ", c.getColumnDLL());
				if(!columns.Last<SQLiteColumn>().Equals(c) && c.tableComment == null) { tableBuilder.Append(", \r\n"); }
			}

			tableBuilder.Append("\r\n); ");

			if (runNonQuery(tableBuilder.ToString()) == true) { return true; } else { return false; }
		}

		/// <summary> Modular table creator function | Creates a new named table if that table doesn't yet exist in the database | Uses modular column definition for reusability</summary>
		/// <param name="tableName">Name of the table to be created</param>
		/// <param name="columns">List of columns to be added in the newly created table. Type: SQLiteColumn</param>
		/// <returns>true if new table created</returns>
		public bool createTable(string tableName, List<SQLiteColumn> columns)
		{
			SQLiteColumn[] cols = columns.ToArray();
			return createTable(tableName, cols);
		}

		/// <summary>Wrapper for the SQLiteCommand.ExecuteNonQuery function</summary>
		/// <param name="query">Query string to be executed</param>
		/// <returns>true if execution success, false if error</returns>
		public bool runNonQuery(string query)
		{
			if (!checkDbConnected()) { Console.WriteLine("ERROR:SQLC → SQL Connection NULL or not open"); return false; }

			SQLiteCommand sqlk = new  SQLiteCommand(query, sqlc);
			int i = -1;

			try
			{
				i = sqlk.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Console.WriteLine("ERROR:SQLC → SQL query returned error: \r\n" + ex.ToString());
				return false;
			}
			
			return i > -1;
		}

	}

	public class SQLiteSchema
	{

	}

	public class SQLiteTable
	{
		public string tableName { get; set; }
		private List<SQLiteColumn> columns = new List<SQLiteColumn>();
		private int numOfPrimaryKeys = 0;
		private List<string> columnNames = new List<string>();

		public SQLiteTable(string TableName)
		{
			if(TableName == null || TableName == "")
			{ throw new Exception("The table name is not a valid value!",new ArgumentException()); }

			tableName = TableName;
		}

		public bool addColumn(SQLiteColumn sqlCol)
		{
			if ((!sqlCol.pimaryKey || numOfPrimaryKeys == 0) && !columnNames.Contains(sqlCol.columnName)) { columns.Add(sqlCol); columnNames.Add(sqlCol.columnName); if (sqlCol.pimaryKey) { numOfPrimaryKeys++; } return true; }
			else
			{
				if (sqlCol.pimaryKey && numOfPrimaryKeys > 0)
				{
					Console.WriteLine(string.Format("Primary key already exists in `{0}`!", tableName));
				}
				else if (columnNames.Contains(sqlCol.columnName))
				{
					Console.WriteLine(string.Format("Column `{0}` already exists in `{1}`!", sqlCol.columnName, tableName));
				}
				return false;
			}
		}

		public bool addColumns(List<SQLiteColumn> sqlCols)
		{
			int primaryKeys = 0;
			bool sameNames = false;
			List<string> colNames = new List<string>();

			foreach (SQLiteColumn s in sqlCols)
			{
				if (s.pimaryKey)
				{
					primaryKeys++; if (primaryKeys > 1)
					{ Console.WriteLine(string.Format("Primary key already exists in `{0}`!", tableName)); break; }
				}

				if (colNames.Contains(s.columnName) || columnNames.Contains(s.columnName))
				{ sameNames = true; Console.WriteLine(string.Format("Column `{0}` already exists in `{1}`!", s.columnName, tableName)); break; }
				else
				{ colNames.Add(s.columnName); }
			}

			if (sameNames || primaryKeys > 1) { return false; }
			else { columns.AddRange(sqlCols); columnNames.AddRange(colNames); return true; }
		}
	}

	/// <summary>SQLiteColumn struct, contains all data needed for a basic column definition</summary>
	public struct SQLiteColumn
	{
		/// <summary>Name of column</summary>
		public string columnName { get; set; }

		/// <summary>Datatype of column Type: SQLiteDataType</summary>
		public SQLiteDataType dataType { get; set; }

		/// <summary>True if the column is Primary key</summary>
		public bool pimaryKey { get; set; }

		/// <summary>True if the column is Foreign key</summary>
		public bool foreignKey { get; set; }
		/// <summary>Table name of foreign table</summary>
		public string foreignTable { get; set; }
		/// <summary>Column name of foreign column</summary>
		public string foreignColumn { get; set; }

		/// <summary>True if the column is AutoIncremented</summary>
		public bool autoIncrement { get; set; }

		/// <summary>True if column has comment</summary>
		private bool hasColumnComment;
		/// <summary>Private container for Column Comment</summary>
		private string _columnComment;
		/// <summary>Public container for Column Comment</summary>
		public string columnComment { get { return _columnComment; } set { _columnComment = value; hasColumnComment = value.Length > 0; } }

		/// <summary>True if column is additional line</summary>
		private bool isAdditionalData;
		/// <summary>Private container for Additional Data</summary>
		private string _additionalData;
		/// <summary>Public container for Additional Data</summary>
		public string additionalData { get { return _additionalData; } set { _additionalData = value; isAdditionalData = value.Length > 0; } }

		/// <summary>True if column is table comment</summary>
		private bool isTableComment;
		/// <summary>Private container for Table Comment</summary>
		private string _tableComment;
		/// <summary>Public container for Table Comment</summary>
		public string tableComment { get { return _tableComment; } set { _tableComment = value; isTableComment = value.Length > 0; } }

		/// <summary>Used to write out the PrimaryKey String</summary> <returns>PRIMARY KEY if true empty string otherwise</returns>
		public string pKey() { return pimaryKey ? " PRIMARY KEY " : ""; }

		/// <summary>Used to write out the SecondaryKey String</summary> <returns>SECONDARY KEY if true empty string otherwise</returns>
		public string fKey() { return foreignKey ? " FOREIGN KEY " : ""; }

		/// <summary>Used to write out the SecondaryKey String</summary> <returns>SECONDARY KEY if true empty string otherwise</returns>
		public string aInc() { return autoIncrement ? " AUTOINCREMENT " : ""; }

		/// <summary>Used to write out any additional lines for table creation</summary> <returns>AddData String if true empty string otherwise</returns>
		public string aDat() { return isAdditionalData ? string.Format(" {0} ", _additionalData) : ""; }

		/// <summary>Used to write out the column comment if exists</summary> <returns>Comment String if true empty string otherwise</returns>
		public string cCom() { return hasColumnComment ? string.Format(" /* {0} */ ", _columnComment) : ""; }

		/// <summary>Used to write out the table comment if exists</summary> <returns>Comment String if true empty string otherwise</returns>
		public string tCom() { return isTableComment ? string.Format(" /* {0} */ ", _tableComment) : ""; }

		/// <summary>Used to write the datatype of the column based on dataType value</summary> <returns>Datatype String</returns>
		public string type()
		{
			string ret = "";
			switch (dataType)
			{
				case SQLiteDataType.NONE:
					ret = "";
					break;

				case SQLiteDataType.INTEGER:
					ret = " INTEGER ";
					break;

				case SQLiteDataType.NUMERIC:
					ret = " NUMERIC ";
					break;

				case SQLiteDataType.REAL:
					ret = " REAL ";
					break;

				case SQLiteDataType.TEXT:
					ret = " TEXT ";
					break;

				default:
					ret = " TEXT ";
					break;
			}
			return ret;
		}

		/// <summary>
		/// Gets the DLL for the current column in text format
		/// </summary>
		/// <returns>DLL of column</returns>
		public string getColumnDLL()
		{
			if (isTableComment)
			{
				return string.Format("\r\n /* {0} */ \r\n", _tableComment);
			}
			else if (isAdditionalData)
			{
				return string.Format(" {0} ", _additionalData);
			}
			else
			{
				return string.Format(" {0} {1} {2} {3} ", columnName, type(), pKey() + fKey() + aInc(), cCom());
			}
			
		}
	}

	/// <summary>Currently supported datatypes | these are the main data container types | Additional types may be added</summary>
	public enum SQLiteDataType
	{
		NONE,
		INTEGER,
		TEXT,
		REAL,
		NUMERIC,

		INT,
		TINYINT,
		SMALLINT,
		MEDIUMINT,
		BIGINT,
		UNSIGNED_BIG_INT,
		INT2,
		INT8,
		CHARACTER,
		VARCHAR,
		VARYING_CHARACTER,
		NCHAR,
		NATIVE_CHARACTER,
		NVARCHAR,
		CLOB,
		DOUBLE,
		DOUBLE_PRECISION,
		FLOAT,
		DECIMAL,
		BOOLEAN,
		DATE,
		DATETIME
	}

}
