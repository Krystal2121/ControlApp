using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace SqlHelper
{
    public class SqlHelp
    {
        public void NoReturn(string sqlcode, string ConnectionStr, out string errorstr)
        {
            try
            {
                OleDbConnection conn = new OleDbConnection(ConnectionStr);
                OleDbCommand comm = new OleDbCommand(sqlcode, conn);
                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                errorstr = "";
            }
            catch (Exception ex)
            {
                errorstr = ex.ToString();
            }
        }
        public int RowsAffect(string sqlcode, string ConnectionStr, out string errorstr)
        {
            int ret = 0;
            try
            {
                OleDbConnection conn = new OleDbConnection(ConnectionStr);
                OleDbCommand comm = new OleDbCommand(sqlcode, conn);
                conn.Open();
                ret=comm.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                errorstr = "";
            }
            catch (Exception ex)
            {
                errorstr = ex.ToString();
            }
            return ret;
        }
        public string StringRet(string sqlcode, string ConnectionStr, out string errorstr)
        {
            string returnstr = "";
            DataSet retds = new DataSet();
            try
            {
                OleDbConnection conn = new OleDbConnection(ConnectionStr);
                OleDbDataAdapter da = new OleDbDataAdapter(sqlcode, conn);
                conn.Open();
                da.Fill(retds);
                conn.Close();
                conn.Dispose();
                errorstr = "";
                try
                {
                    if (retds.Tables[0].Rows.Count > 0)
                    {
                        returnstr = retds.Tables[0].Rows[0][0].ToString();
                    }
                }
                catch (Exception ex)
                {
                    errorstr = ConnectionStr + ":" + ex.ToString();
                }
            }
            catch (Exception ex)
            {
                errorstr = ConnectionStr + ":" + ex.ToString();
            }

            return returnstr;
        }
        public int RowCOunt(string sqlcode, string ConnectionStr, out string errorstr)
        {
            int returnint=-1;
            DataSet retds = new DataSet();
            try
            {
                OleDbConnection conn = new OleDbConnection(ConnectionStr);
                OleDbDataAdapter da = new OleDbDataAdapter(sqlcode,conn);                
                conn.Open();
                da.Fill(retds);
                conn.Close();
                conn.Dispose();
                errorstr = "";
            }
            catch (Exception ex)
            {
                errorstr = ex.ToString();
            }
            returnint = retds.Tables[0].Rows.Count ;
            return returnint;
        }
        public DataSet DatasetRet(string sqlcode, string ConnectionStr, out string errorstr)
        {
            DataSet retds = new DataSet();
            try
            {
                OleDbConnection conn = new OleDbConnection(ConnectionStr);
                OleDbDataAdapter da = new OleDbDataAdapter(sqlcode, conn);
                conn.Open();
                da.Fill(retds);
                conn.Close();
                conn.Dispose();
                errorstr = "";
            }
            catch (Exception ex)
            {
                errorstr = ConnectionStr + ":" + ex.ToString();
            }
            return retds;
        }
    }
}
