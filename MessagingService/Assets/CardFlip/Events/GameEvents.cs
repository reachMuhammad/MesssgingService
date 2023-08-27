using System;

public static class GameEvents
{
    private static Action<Views, object> _onShowView;
    private static Action<Views> _onCloseView;

    public static event Action<Views, object> OnShowMenu
    {
        add
        {
            _onShowView += value;
        }
        remove
        {
            _onShowView -= value;
        }
    }

    public static event Action<Views> OnCloseMenu
    {
        add
        {
            _onCloseView += value;
        }
        remove
        {
            _onCloseView -= value;
        }
    }

    public static void DoFireShowView(Views view, object model = null)
    {
        EventInvoker.InvokeEvent(_onShowView, view, model);
    }

    public static void DoFireCloseView(Views view)
    {
        EventInvoker.InvokeEvent(_onCloseView, view);
    }
}