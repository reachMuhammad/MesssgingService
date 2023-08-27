using System;

public static class EventInvoker
{
    public static void InvokeEvent(Action evt)
    {
        if (!CanInvokeEvent(evt)) return;
        evt();
    }

    public static void InvokeEvent<T>(Action<T> evt, T param)
    {
        if (!CanInvokeEvent(evt)) return;
        evt(param);
    }

    public static void InvokeEvent<T1, T2>(Action<T1, T2> evt, T1 param1, T2 param2)
    {
        if (!CanInvokeEvent(evt)) return;
        evt(param1, param2);
    }

    public static void InvokeEvent<T1, T2, T3>(Action<T1, T2, T3> evt, T1 param1, T2 param2, T3 param3)
    {
        if (!CanInvokeEvent(evt)) return;

        evt(param1, param2, param3);
    }

    private static bool CanInvokeEvent(Delegate evt)
    {
        if (evt == null) return false;

        return true;
    }
}