using System;

public static class GameEvents
{
    private static Action<Views, object> _onShowView;
    private static Action<Views> _onCloseView;
    private static Action _onCorrectSelection;
    private static Action _onWrongSelection;
    private static Action _onGameStart;

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

    public static event Action OnCorrectSelection
    {
        add
        {
            _onCorrectSelection += value;
        }
        remove
        {
            _onCorrectSelection -= value;
        }
    }

    public static event Action OnWrongSelection
    {
        add
        {
            _onWrongSelection += value;
        }
        remove
        {
            _onWrongSelection -= value;
        }
    }

    public static event Action OnGameStart
    {
        add
        {
            _onGameStart += value;
        }
        remove
        {
            _onGameStart -= value;
        }
    }

    public static void DoFireGameStart()
    {
        EventInvoker.InvokeEvent(_onGameStart);
    }

    public static void DoFireShowView(Views view, object model = null)
    {
        EventInvoker.InvokeEvent(_onShowView, view, model);
    }

    public static void DoFireCloseView(Views view)
    {
        EventInvoker.InvokeEvent(_onCloseView, view);
    }

    public static void DoFireCorrectSelection()
    {
        EventInvoker.InvokeEvent(_onCorrectSelection);
    }

    public static void DoFireWrongSelection()
    {
        EventInvoker.InvokeEvent(_onWrongSelection);
    }
}