using UnityEngine;
using UnityEngine.UI;

public class GameModeItem : MonoBehaviour
{
    [SerializeField] private GameMode _gameMode;
    [SerializeField] private Button _gameModeButton;
    [SerializeField] private GameObject _selectedState;

    private IMainMenuGameMode _mainMenuGameModeHandler;

    public void Initialize(IMainMenuGameMode mainMenuGameMode)
    {
        _mainMenuGameModeHandler = mainMenuGameMode;

        RegisterEvents();
    }

    private void RegisterEvents()
    {
        _gameModeButton.onClick.AddListener(ModeClicked);
    }

    private void ModeClicked()
    {
        SoundsController.PlaySound(SoundType.ButtonClick);

        _mainMenuGameModeHandler.GameModeChanged(_gameMode);

        SetState(true);
    }

    public void SetState(bool state)
    {
        _selectedState.SetActive(state);
    }
}
