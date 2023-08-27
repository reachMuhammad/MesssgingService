using UnityEngine;


[CreateAssetMenu(fileName = "DBString", menuName = "Persistent Variables/String")]
public class DBString : String, IDBVariable
{
    [SerializeField] protected string Key;

    public new void SetValue(string value)
    {
        base.SetValue(value);
        Save();
    }

    public new void SetValue(String value)
    {
        base.SetValue(value);
        Save();
    }

    public void SetKey(string key) => Key = key;

    public virtual void Refresh() => Load();

    private void OnEnable() => Load();

    public string GetKey() => Key;

    public virtual void ApplyChange(string text)
    {
        Value += text;
        Save();
    }

    public virtual void ApplyChange(String text)
    {
        Value += text.GetValue();
        Save();
    }

    public new void SetDefaultValue(string value)
    {
        base.SetDefaultValue(value);
        Save();
    }

    public new void SetDefaultValue(String value)
    {
        base.SetDefaultValue(value);
        Save();
    }

    public override void ResetToDefaultValue()
    {
        base.ResetToDefaultValue();
        Save();
    }

    public virtual void Save() => DatabaseManager.SetString(this, Key, Value);

    public virtual void Load()
    {
        //if (ResetToDefaultOnPlay)
        //{
        //    Value = DefaultValue;
        //    return;
        //}

        if (string.IsNullOrEmpty(Key) || !DatabaseManager.HasKey(this, Key))
        {
            if (ResetToDefaultOnPlay || ResetOnClear)
                Value = DefaultValue;
            else
                Value = "";

            return;
        }

        Value = DatabaseManager.GetString(this, Key);
    }

    object IDBVariable.GetValue()
    {
        return GetValue();
    }

    void IDBVariable.Update(object value)
    {
        if (value is string)
            SetValue((string)value);            
    }
}

