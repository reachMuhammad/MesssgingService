using System.Collections.Generic;
using UnityEngine;

public class GridViewController : BaseUIViewController<GridViewRefs>, IGridCard
{
    private Vector2 _gridSize;
    private float _tileSize = 0;
    private Dictionary<int, RectTransform> _tilesDict;

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

        CalculateCardSize();
        SpawnGrid();
        GenerateCards();
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
            cardView.Initialize(this, _ViewRefs.CardsConfigs.CardsData[cardData.Value]);
            cardView.SetCardSize(cardSize);
        }
    }

    void IGridCard.CardSelected(int cardId)
    {
    }
}
