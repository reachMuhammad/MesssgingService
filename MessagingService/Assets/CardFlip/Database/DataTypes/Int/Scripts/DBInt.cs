using System;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu(fileName = "DBInt", menuName = "Persistent Variables/Int")]
public class DBInt : Int, IDBVariable
{
    [SerializeField] protected string Key;
                     
    public new void SetValue(int value)
    {
        base.SetValue(value);
        Save();
    }

    public new void SetValue(Int value)
    {
        base.SetValue(value);
        Save();
    }

    public void SetKey(string key) => Key = key;

    public void Refresh() => Load();

    private void OnEnable() => Load();

    public string GetKey() => Key;       

    public virtual void ApplyChange(int amount)
    {
        Value += amount;
        Save();
    }

    public virtual void ApplyChange(Int amount)
    {
        Value += amount.GetValue();
        Save();
    }

    public void Save() => DatabaseManager.SetInt(this, Key, Value);

    public virtual void Load()
    {
        if (string.IsNullOrEmpty(Key) || !DatabaseManager.HasKey(this, Key))
        {
            if (ResetToDefaultOnPlay)
                Value = DefaultValue;
            else
                Value = 0;

            return;
        }

        Value = DatabaseManager.GetInt(this, Key);
    }

    void IDBVariable.Update(object value)
    {
        int integer = Value;

        if (value is int)
            integer = Convert.ToInt32(value, CultureInfo.InvariantCulture);            
        else if (value is long)
            integer = Convert.ToInt32(value, CultureInfo.InvariantCulture);            

        SetValue(integer);
    }

    object IDBVariable.GetValue()
    {
        return GetValue();
    }
}
