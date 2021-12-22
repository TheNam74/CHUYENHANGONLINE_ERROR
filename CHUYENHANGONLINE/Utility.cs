using System;
using System.Data.SqlClient;

namespace CHUYENHANGONLINE
{
    public static class Utility
    {

        public static DateTime? SafeGetDate(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDateTime(colIndex);
            return null;
        }
        public static String SafeGetString(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return "";
        }
        public static int? SafeGetInt(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            return null;
        }
        public static double SafeGetDouble(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDouble(colIndex);
            return 0;
        }

    }
}
//test git