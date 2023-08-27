using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    [SerializeField] private GamePlayRefs _gamePlayRefs;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        RegisterEvents();

        _gamePlayRefs.UIViewController.Show();

        GameStart();
    }

    private void RegisterEvents()
    {

    }

    private void GameStart()
    {
        GameEvents.DoFireShowView(Views.MainMenuView);
    }
}

public enum GameMode
{
    Easy = 0,
    Medium,
    Hard,
    VeryHard,
    ExtremeHard
}