using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Newtonsoft.Json;
using UnityEngine;

public class GridViewController : BaseUIViewController<GridViewRefs>, IGridCard
{
    private Vector2 _gridSize;
    private float _tileSize = 0;
    private Dictionary<int, RectTransform> _tilesDict;
    private Dictionary<int, CardView> _cardsDict;
    private SelectedCardData selectedCardData;
    private float _initialCardDisplayTime = 2;
    private int _matchesCount;
    private int _turnsCount;
    private GameState _gameState;

    public override void RegisterEvents()
    {
        _ViewRefs.HomeButton.onClick.AddListener(HomeButtonClicked);
    }

    public override void UnregisterEvents()
    {
        _ViewRefs.HomeButton.onClick.RemoveListener(HomeButtonClicked);
        _ViewRefs.ComboViewController.Close();
    }

    public override void Show(object model = null)
    {
        base.Show(model);

        var gridInitialData = (GridInitialData)model;

        _gameState.GameMode = gridInitialData.GameMode;

        _tilesDict = new Dictionary<int, RectTransform>();
        _cardsDict = new Dictionary<int, CardView>();
        _gameState.GridState = new Dictionary<int, int>();
        _ViewRefs.ComboViewController.Initialize();

        selectedCardData.CardId = -1;
        selectedCardData.TileId = -1;

        LoadState(gridInitialData.isResumeGame);
        GameStart();
    }

    private void LoadState(bool isResumeGame)
    {
        if (isResumeGame)
        {
            _gameState = JsonConvert.DeserializeObject<GameState>(_ViewRefs.DBGameStateData.GetValue());
            _gridSize = _ViewRefs.GridViewConfigs.GridGameModeData.FirstOrDefault(x => x.GameMode == _gameState.GameMode).GridSize;
            CalculateCardSize();
            SpawnGrid();
            SpawnCards(_gameState.GridState);

            _matchesCount = _gameState.MatchesCount;
            _turnsCount = _gameState.TurnsCount;

            SetMatchesCount();
            SetTurnsCount();
        }
        else
        {
            _gridSize = _ViewRefs.GridViewConfigs.GridGameModeData.FirstOrDefault(x => x.GameMode == _gameState.GameMode).GridSize;

            CalculateCardSize();
            SpawnGrid();
            GenerateCards();
        }
    }

    public void CalculateCardSize()
    {
        var cardHeight = _ViewRefs.GridRectTransform.sizeDelta.y / _gridSize.x;
        var cardWidth = _ViewRefs.GridRectTransform.sizeDelta.x / _gridSize.y;

        _tileSize = cardWidth < cardHeight ? cardWidth : cardHeight;
    }

    private void SpawnGrid()
    {
        int tileId = 0;

        for (int row = 0; row < _gridSize.x; row++)
        {
            for (int column = 0; column < _gridSize.y; column++)
            {
                var tile = Instantiate(_ViewRefs.Tile, _ViewRefs.CardsContainer);

                tile.sizeDelta = new Vector2(_tileSize, _tileSize);

                tile.transform.localPosition = new Vector2(column * _tileSize, row * _tileSize);

                _tilesDict.Add(tileId, tile);

                tileId++;
            }
        }

        var gridWidth = _gridSize.y * _tileSize;
        var gridHeight = _gridSize.x * _tileSize;

        _ViewRefs.CardsContainer.localPosition = new Vector2(-gridWidth / 2 + _tileSize / 2, -gridHeight / 2 + _tileSize / 2);
    }

    private void GenerateCards()
    {
        var tilesCount = _gridSize.x * _gridSize.y;
        var cardsToSpawn = tilesCount / 2;

        List<int> tilesList = new List<int>();
        Dictionary<int, int> cardsData = new Dictionary<int, int>();


        for (int i = 0; i < tilesCount; i++)
        {
            tilesList.Add(i);
        }

        for (int i = 0; i < cardsToSpawn; i++)
        {
            var cardId = i % _ViewRefs.CardsConfigs.CardsData.Length;

            var tileId = Random.Range(0, tilesList.Count);
            cardsData.Add(tilesList[tileId], cardId);
            tilesList.RemoveAt(tileId);

            var tileId2 = Random.Range(0, tilesList.Count);
            cardsData.Add(tilesList[tileId2], cardId);
            tilesList.RemoveAt(tileId2);
        }

        SpawnCards(cardsData);

        _gameState.GridState = cardsData;
        SaveGameState();
    }

    private void SpawnCards(Dictionary<int, int> cardsData)
    {
        var cardSize = _tileSize * 80 * 0.01f;

        foreach (KeyValuePair<int, int> cardData in cardsData)
        {
            var card = Instantiate(_ViewRefs.CardsConfigs.CardObject, _tilesDict[cardData.Key]);
            var cardView = card.GetComponent<CardView>();
            cardView.Initialize(this, _ViewRefs.CardsConfigs.CardsData[cardData.Value], cardData.Key);
            cardView.SetCardSize(cardSize);
            _cardsDict.Add(cardData.Key, cardView);
        }
    }

    private void GameStart()
    {
        DOVirtual.DelayedCall(_initialCardDisplayTime, () =>
        {
            foreach (KeyValuePair<int, CardView> cardData in _cardsDict)
            {
                cardData.Value.HideCard();
            }
        });
    }

    void IGridCard.CardSelected(int cardId, int tileId)
    {
        if (selectedCardData.CardId == -1)
        {
            selectedCardData.CardId = cardId;
            selectedCardData.TileId = tileId;

            return;
        }

        _turnsCount++;
        SetTurnsCount();

        if (cardId == selectedCardData.CardId)
        {
            CorrectCardsSelection(selectedCardData.TileId, tileId);
        }
        else
        {
            WrongSelection(selectedCardData.TileId, tileId);

            SaveGameState();
        }

        selectedCardData.CardId = -1;
    }

    private void CorrectCardsSelection(int firstCardTileId, int secondCardTileId)
    {
        if (!_cardsDict.ContainsKey(firstCardTileId) || !_cardsDict.ContainsKey(secondCardTileId))
            return;

        GameEvents.DoFireCorrectSelection();

        _matchesCount++;
        SetMatchesCount();

        DOVirtual.DelayedCall(1, () =>
        {
            SoundsController.PlaySound(SoundType.CorrectSelection);

            _cardsDict[firstCardTileId].Destroy();
            _cardsDict[secondCardTileId].Destroy();
            _cardsDict.Remove(firstCardTileId);
            _cardsDict.Remove(secondCardTileId);

            _gameState.GridState.Remove(firstCardTileId);
            _gameState.GridState.Remove(secondCardTileId);

            SaveGameState();
        });

        DOVirtual.DelayedCall(2, () =>
        {
            if (_cardsDict.Count <= 0)
            {
                GameOver();
            }
        });
    }

    private void WrongSelection(int firstCardTileId, int secondCardTileId)
    {
        GameEvents.DoFireWrongSelection();

        DOVirtual.DelayedCall(1, () =>
        {
            SoundsController.PlaySound(SoundType.WrongSelection);
            _cardsDict[firstCardTileId].HideCard();
            _cardsDict[secondCardTileId].HideCard();
        });
    }

    private void GameOver()
    {
        SoundsController.PlaySound(SoundType.GameOver);
        ClearGameState();
        GameEvents.DoFireCloseView(Views.GamePlayGridView);
        GameEvents.DoFireShowView(Views.GameOverView);
    }

    private void HomeButtonClicked()
    {
        GameEvents.DoFireCloseView(Views.GamePlayGridView);
        GameEvents.DoFireShowView(Views.MainMenuView);
    }

    private void SetMatchesCount()
    {
        _ViewRefs.MatchesCount.text = _matchesCount.ToString();
        _gameState.MatchesCount = _matchesCount;
    }

    private void SetTurnsCount()
    {
        _ViewRefs.TurnsCount.text = _turnsCount.ToString();
        _gameState.TurnsCount = _turnsCount;
    }

    private void ClearGameState()
    {
        _ViewRefs.DBGameStateData.SetValue("");
        _ViewRefs.DBGameStateData.Save();
    }

    private void SaveGameState()
    {
        _ViewRefs.DBGameStateData.SetValue(JsonConvert.SerializeObject(_gameState));
        _ViewRefs.DBGameStateData.Save();
    }
}

public struct SelectedCardData
{
    public int CardId;
    public int TileId;
}

public struct GameState
{
    public GameMode GameMode;
    public int MatchesCount;
    public int TurnsCount;
    public Dictionary<int, int> GridState;
}
