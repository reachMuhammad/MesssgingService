using UnityEngine;

public abstract class BaseUIViewController : MonoBehaviour
{
    public abstract void Show(object model = null);
    public abstract void Close();
}

public abstract class BaseUIViewController<T> : BaseUIViewController where T : BaseUIViewRefs
{
    [SerializeField] protected T _ViewRefs;

    public abstract void RegisterEvents();
    public abstract void UnregisterEvents();

    public override void Show(object model = null)
    {
        RegisterEvents();
    }

    public override void Close()
    {
        UnregisterEvents();
    }
}
