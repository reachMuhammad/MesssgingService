using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GridViewController : BaseUIViewController<GridViewRefs>, IGridCard
{
    private Vector2 _gridSize;
    private float _tileSize = 0;
    private Dictionary<int, RectTransform> _tilesDict;
    private Dictionary<int, CardView> _cardsDict;
    private SelectedCardData selectedCardData;
    private float _initialCardDisplayTime = 2;

    private void Start()
    {
        Show();
    }

    public override void RegisterEvents()
    {
    }

    public override void UnregisterEvents()
    {
    }

    public override void Show(object model = null)
    {
        base.Show(model);

        _gridSize.x = 4;
        _gridSize.y = 4;

        _tilesDict = new Dictionary<int, RectTransform>();
        _cardsDict = new Dictionary<int, CardView>();

        selectedCardData.CardId = -1;
        selectedCardData.TileId = -1;

        _ViewRefs.ComboViewController.Initialize();

        CalculateCardSize();
        SpawnGrid();
        GenerateCards();
        GameStart();
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

        if (cardId == selectedCardData.CardId)
        {
            CorrectCardsSelection(selectedCardData.TileId, tileId);
        }
        else
        {
            WrongSelection(selectedCardData.TileId, tileId);
        }

        selectedCardData.CardId = -1;
    }

    private void CorrectCardsSelection(int firstCardTileId, int secondCardTileId)
    {
        if (!_cardsDict.ContainsKey(firstCardTileId) || !_cardsDict.ContainsKey(secondCardTileId))
            return;

        GameEvents.DoFireCorrectSelection();

        DOVirtual.DelayedCall(1, () =>
        {
            _cardsDict[firstCardTileId].Destroy();
            _cardsDict[secondCardTileId].Destroy();
            _cardsDict.Remove(firstCardTileId);
            _cardsDict.Remove(secondCardTileId);
        });
    }

    private void WrongSelection(int firstCardTileId, int secondCardTileId)
    {
        GameEvents.DoFireWrongSelection();

        DOVirtual.DelayedCall(1, () =>
        {
            _cardsDict[firstCardTileId].HideCard();
            _cardsDict[secondCardTileId].HideCard();
        });
    }

}

public struct SelectedCardData
{
    public int CardId;
    public int TileId;
}
