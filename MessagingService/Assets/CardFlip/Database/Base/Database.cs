using UnityEngine;
 
public class Database : ScriptableObject
{
    public virtual void Save()
    {
        PlayerPrefs.Save();
    }

    public virtual bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public virtual void Clear()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public virtual void RemoveKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    public virtual void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        Save();
    }

    public virtual int GetInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public virtual void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        Save();
    }

    public virtual string GetString(string key, string defaultValue = "")
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }
}

