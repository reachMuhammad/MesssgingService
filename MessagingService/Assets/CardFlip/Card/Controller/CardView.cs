using UnityEngine;

public class CardView : MonoBehaviour
{
   [SerializeField] private CardViewRefs _cardViewRefs;

    private IGridCard _gridCardHandler;
    private int _cardId;
    private bool _isRevealed;

    public void Initialize(IGridCard gridCard, int cardId)
    {
        _cardId = cardId;
        _gridCardHandler = gridCard;

        RegisterEvents();

        HideCard();
    }

    private void RegisterEvents()
    {
        _cardViewRefs.CardButton.onClick.AddListener(CardClicked);
    }

    private void CardClicked()
    {
        ShowCard();
    }

    private void ShowCard()
    {
        if (_isRevealed)
            return;

        _isRevealed = true;

        _cardViewRefs.CardFront.gameObject.SetActive(true);
        _cardViewRefs.CardBack.gameObject.SetActive(false);

        _gridCardHandler.CardSelected(_cardId);
    }

    private void HideCard()
    {
        _isRevealed = false;

        _cardViewRefs.CardFront.gameObject.SetActive(false);
        _cardViewRefs.CardBack.gameObject.SetActive(true);
    }
}
