using UnityEngine;


    [CreateAssetMenu(fileName = "Int", menuName = "Variables/Int")]
    public class Int : ScriptableObject
    {
        [SerializeField] protected int Value;
        [SerializeField] protected int DefaultValue;
        [SerializeField] protected bool ResetToDefaultOnPlay = true;

        private void OnEnable()
        {
            if (ResetToDefaultOnPlay)
            {
                Value = DefaultValue;
            }
        }

        public virtual void SetValue(int value)
        {
            Value = value;
        }

        public virtual void SetValue(Int value)
        {
            Value = value.Value;
        }

        public virtual int GetValue()
        {
            return Value;
        }

        public virtual void SetDefaultValue(int value) => DefaultValue = value;

        public virtual void SetDefaultValue(Int value) => DefaultValue = value.GetValue();

        public virtual void ResetToDefaultValue() => SetValue(DefaultValue);

        public static implicit operator int(Int integer)
        {
            return integer.Value;
        }
    }
