using UnityEngine;
using DG.Tweening;

public class CardView : MonoBehaviour
{
   [SerializeField] private CardViewRefs _cardViewRefs;

    private IGridCard _gridCardHandler;
    private int _cardId;
    private int _tileId;
    private bool _isRevealed;

    public void Initialize(IGridCard gridCard, CardData cardData, int tileId)
    {
        _cardId = cardData.CardId;
        _tileId = tileId;
         _cardViewRefs.CardImage.sprite = cardData.CardSprite;
        _cardViewRefs.CardNo.text = cardData.CardId.ToString();
         _gridCardHandler = gridCard;

        RegisterEvents();

        ShowCard();
    }

    public void SetCardSize(float cardSize)
    {
        _cardViewRefs.CardRectTransform.sizeDelta = new Vector2(cardSize, cardSize);
    }

    private void RegisterEvents()
    {
        _cardViewRefs.CardButton.onClick.AddListener(CardClicked);
    }

    private void CardClicked()
    {
        if (_isRevealed)
            return;

        SoundsController.PlaySound(SoundType.CardSelect);

        ShowCard();

        _gridCardHandler.CardSelected(_cardId, _tileId);
    }

    private void ShowCard()
    {
        _isRevealed = true;

        DOTween.Sequence()
            .Append(_cardViewRefs.CardBack.transform.DOScaleX(0, 0.1f))
            .Append(_cardViewRefs.CardFront.transform.DOScaleX(1,0.1f));
    }

    public void HideCard()
    {
        DOTween.Sequence()
            .Append(_cardViewRefs.CardFront.transform.DOScaleX(0, 0.1f))
            .Append(_cardViewRefs.CardBack.transform.DOScaleX(1, 0.1f)).OnComplete(()=> { _isRevealed = false; });
    }

    public void Destroy()
    {
        DOTween.Sequence().Append(_cardViewRefs.transform.DOScale(0, 0.2f).SetEase(Ease.InBack)).OnComplete(() => { Destroy(gameObject); });   
    }
}
