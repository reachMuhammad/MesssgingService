using UnityEngine;

public class GameOverViewController : BaseUIViewController<GameOverViewRefs>
{
    public override void RegisterEvents()
    {
        _ViewRefs.RestartGameButton.onClick.AddListener(RestartGame);
    }

    public override void UnregisterEvents()
    {
        _ViewRefs.RestartGameButton.onClick.RemoveListener(RestartGame);
    }

    public override void Show(object model = null)
    {
        base.Show(model);
    }

    private void RestartGame()
    {
        SoundsController.PlaySound(SoundType.ButtonClick);
        GameEvents.DoFireCloseView(Views.GameOverView);
        GameEvents.DoFireShowView(Views.MainMenuView);
    }

    public override void Close()
    {
        base.Close();
    }

}
