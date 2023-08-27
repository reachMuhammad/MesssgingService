using UnityEngine;

[CreateAssetMenu(fileName = "String", menuName = "Variables/String")]
public class String : ScriptableObject
{
    [SerializeField] protected string Value;
    [SerializeField] protected string DefaultValue;
    [SerializeField] protected bool ResetToDefaultOnPlay = true;
    [SerializeField] protected bool ResetOnClear;

    private void OnEnable()
    {
        if (ResetToDefaultOnPlay)
        {
            Value = DefaultValue;
        }
    }

    public virtual void SetValue(string value)
    {
        Value = value;
    }

    public virtual void SetValue(String value)
    {
        Value = value.Value;
    }

    public virtual string GetValue()
    {
        return Value;
    }

    public virtual void SetDefaultValue(string value) => DefaultValue = value;

    public virtual void SetDefaultValue(String value) => DefaultValue = value.GetValue();

    public virtual void ResetToDefaultValue() => SetValue(DefaultValue);

    public static implicit operator string(String value)
    {
        return value.Value;
    }
}