using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager 
{
    private static Dictionary<string, IDBVariable> _dbVariables = new Dictionary<string, IDBVariable>();

    private static Database _database;
    private static Database Database
    {
        get
        {
            if (_database == null)
            {
                _database = ScriptableObject.CreateInstance<Database>();
            }
            return _database;
        }
    }

    public static bool HasKey(IDBVariable dBVariable, string key)
    {
        TrackVariable(dBVariable, key);
        return Database.HasKey(key);
    }

    public static void SetInt(IDBVariable dBVariable, string key, int value)
    {
        TrackVariable(dBVariable, key);
        Database.SetInt(key, value);
    }

    public static int GetInt(IDBVariable dBVariable, string key)
    {
        TrackVariable(dBVariable, key);
        return Database.GetInt(key);
    }

    public static void SetString(IDBVariable dBVariable, string key, string value)
    {
        TrackVariable(dBVariable, key);
        Database.SetString(key, value);
    }

    public static string GetString(IDBVariable dBVariable, string key)
    {
        TrackVariable(dBVariable, key);
        return Database.GetString(key);
    }

    private static void TrackVariable(IDBVariable dBVariable, string key)
    {
        if (dBVariable != null && !_dbVariables.ContainsKey(key))
        {
            _dbVariables.Add(key, dBVariable);
        }
    }
}
