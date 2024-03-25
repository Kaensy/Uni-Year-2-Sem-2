using System;
using System.Configuration;
using System.Data;
using System.Reflection;
using log4net;
using Npgsql;

namespace MPP_CSharp.Repository;

public class DBUtil
{
    private static DBUtil? _DBUtil;
    private static string? _connectionString;
    private NpgsqlConnection? _instance;

    private static readonly ILog Logger =
        LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? typeof(DBUtil));

    public static DBUtil Instance
    {
        get
        {
            if (_DBUtil != null) return _DBUtil;

            _connectionString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;
            _DBUtil = new DBUtil();
            return _DBUtil;
        }

        private set => _DBUtil = value;
    }

    private DBUtil()
    {
    }


    private NpgsqlConnection? GetNewConnection()
    {
        Logger.Debug("Getting new connection");

        NpgsqlConnection? con = null;
        try
        {
            Logger.Debug("Creating new connection");
            con = new NpgsqlConnection(_connectionString);
            con.Open();
        }
        catch (Exception e)
        {
            Logger.Error(e);
        }

        Logger.Debug("Returning new connection");
        return con;
    }

    public NpgsqlConnection? GetConnection()
    {
        Logger.Debug("Getting connection");

        try
        {
            if (_instance == null || _instance.State == ConnectionState.Closed)
                _instance = GetNewConnection();
        }
        catch (Exception e)
        {
            Logger.Error(e);
        }

        Logger.Debug("Returning connection");
        return _instance;
    }

    public void CloseConnection()
    {
        Logger.Debug("Closing connection");
        try
        {
            if (_instance != null && _instance.State != ConnectionState.Closed)
                _instance.Close();
        }
        catch (Exception e)
        {
            Logger.Error(e);
        }
    }
}