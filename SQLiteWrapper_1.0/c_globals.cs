using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteWrapper
{
	public static class c_globals
	{
		public static List<SQLiteDataType> typesWithLength = new List<SQLiteDataType> { SQLiteDataType.CHARACTER, SQLiteDataType.VARCHAR, SQLiteDataType.VARYING_CHARACTER, SQLiteDataType.NCHAR, SQLiteDataType.NATIVE_CHARACTER, SQLiteDataType.NVARCHAR };
	}
}
