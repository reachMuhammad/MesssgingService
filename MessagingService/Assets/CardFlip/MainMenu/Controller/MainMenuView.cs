public class MainMenuView : BaseUIViewController<MainMenuViewRefs>, IMainMenuGameMode
{
    private GameMode _gameMode;

    //private void Start()
    //{
    //    Show();

    //    RegisterEvents();
    //}

    public override void RegisterEvents()
    {
        _ViewRefs.StartGameButton.onClick.AddListener(StartGame);
        _ViewRefs.ResumeGameButton.onClick.AddListener(ResumeGame);
    }

    public override void UnregisterEvents()
    {
        _ViewRefs.StartGameButton.onClick.RemoveListener(StartGame);
        _ViewRefs.ResumeGameButton.onClick.RemoveListener(ResumeGame);
    }

    public override void Show(object model = null)
    {
        base.Show(model);

        _ViewRefs.ResumeGameButton.gameObject.SetActive(_ViewRefs.DBGameStateData.GetValue() != "");

        for (int i = 0; i < _ViewRefs.GameModeItems.Length; i++)
        {
            _ViewRefs.GameModeItems[i].Initialize(this);
        }
    }

    private void StartGame()
    {
        GameEvents.DoFireCloseView(Views.MainMenuView);
        GameEvents.DoFireShowView(Views.GamePlayGridView, new GridInitialData() { GameMode = _gameMode, isResumeGame = false });
    }

    private void ResumeGame()
    {
        GameEvents.DoFireCloseView(Views.MainMenuView);
        //GameEvents.DoFireResumeBattle();
        GameEvents.DoFireShowView(Views.GamePlayGridView, new GridInitialData() { GameMode = _gameMode, isResumeGame = true });
    }

    public override void Close()
    {
        base.Close();
    }

    void IMainMenuGameMode.GameModeChanged(GameMode gameMode)
    {
        for (int i = 0; i < _ViewRefs.GameModeItems.Length; i++)
        {
            _ViewRefs.GameModeItems[i].SetState(false);
        }

        _gameMode = gameMode;
    }
}

public struct GridInitialData
{
    public GameMode GameMode;
    public bool isResumeGame;
}