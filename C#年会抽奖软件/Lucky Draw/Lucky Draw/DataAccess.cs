

using System;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;

/// <summary>
/// DataAccess Ϊ���ݿ������ȫ�ֶ����һЩ��
/// </summary>
public class DataAccess
{
    public DataAccess()
    {
    }
    public static bool DataIsChange;

    #region �������ݿ������ַ���
    /// <summary>
    /// ��ȡC/Sϵͳ��Ŀ¼
    /// </summary>
    public static string SysRootPath()
    {
        string resultPath = string.Empty;

        string sysPath = Application.StartupPath;
        if (sysPath.LastIndexOf("bin") > 0)
        {
            resultPath = sysPath.Substring(0, sysPath.LastIndexOf("bin"));
        }
        else
        {
            resultPath = sysPath;
        }

        return resultPath;
    }



    /// <summary>
    /// �������ݿ������ַ���
    /// </summary>
    private static string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Environment.CurrentDirectory + "\\data\\LD.mdb;Persist Security Info=False;Jet OLEDB:Database Password=shiyanexperiment;";
    #endregion

    #region  ִ��SQL��䣬����Boolֵ
    /// <summary>
    /// ִ��SQL��䣬����Boolֵ
    /// </summary>
    /// <param name="sql">Ҫִ�е�SQL���</param>
    /// <returns>����BOOLֵ��TrueΪִ�гɹ�</returns>
    public bool ExecuteSQL(string sql)
    {
        OleDbConnection con = new OleDbConnection(ConnectionString);
        OleDbCommand cmd = new OleDbCommand(sql, con);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            con.Close();
            con.Dispose();
            cmd.Dispose();
        }
    }
    #endregion


    #region ��ȡ�������Ľ����
    /// <summary>
    /// ��ȡ�������Ľ����
    /// </summary>
    /// <param name="sql">SQL���</param>
    /// <param name="paramlist">�����б�</param>
    /// <returns>�����</returns>
    public OleDbDataReader ExecuteReader(string sql)
    {
        OleDbDataReader reader = null;
        OleDbConnection con = new OleDbConnection(ConnectionString);
        OleDbCommand cmd = new OleDbCommand(sql, con);
        try
        {
            con.Open();
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch
        {
        }
        
        return reader;
    }
    #endregion

    #region  ִ��SQL��䣬����DataTable
    /// <summary>
    /// ִ��SQL��䣬����DataTable
    /// </summary>
    /// <param name="sql">Ҫִ�е�SQL���</param>
    /// <returns>����DataTable���͵�ִ�н��</returns>
    public DataTable GetDataTable(string sql)
    {
        DataSet ds = new DataSet();
        OleDbConnection con = new OleDbConnection(ConnectionString);
        OleDbDataAdapter da = new OleDbDataAdapter(sql, con);
        try
        {
            da.Fill(ds, "tb");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {
            con.Close();
            con.Dispose();
            da.Dispose();
        }
        DataTable result = ds.Tables["tb"];
        return result;
    }
    #endregion

    #region  ����Excel������DataTable
    /// <summary>
    /// ����Excel������DataTable
    /// </summary>
    /// <param name="strFilePath">Ҫ�����Excel��</param>
    /// <returns>����DataTable���͵�ִ�н��</returns>
    public DataTable LendInDT(string strFilePath)
    {
        if (strFilePath == null)
        {
            throw new ArgumentNullException("filename string is null!");
        }

        if (strFilePath.Length == 0)
        {
            throw new ArgumentException("filename string is empty!");
        }

        string oleDBConnString = String.Empty;
        oleDBConnString = "Provider=Microsoft.Jet.OLEDB.4.0;";
        oleDBConnString += "Data Source=";
        oleDBConnString += strFilePath;
        oleDBConnString += ";Extended Properties=Excel 8.0;";


        OleDbConnection oleDBConn = null;
        OleDbDataAdapter da = null;
        DataTable m_tableName = new DataTable(); ;
        DataSet ds = new DataSet();
        oleDBConn = new OleDbConnection(oleDBConnString);
        oleDBConn.Open();
        m_tableName = oleDBConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        if (m_tableName != null && m_tableName.Rows.Count > 0)
        {

            m_tableName.TableName = m_tableName.Rows[0]["TABLE_NAME"].ToString();

        }
        string sqlMaster = " SELECT * FROM [" + m_tableName + "]";
        da = new OleDbDataAdapter(sqlMaster, oleDBConn);
        try
        {
            da.Fill(ds, "tb");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {
            oleDBConn.Close();
            oleDBConn.Dispose();
            da.Dispose();
        }
        DataTable result = ds.Tables["tb"];
        return result;

    }
    #endregion
}

