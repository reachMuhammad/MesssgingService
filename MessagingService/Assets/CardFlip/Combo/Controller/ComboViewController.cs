using DG.Tweening;
using UnityEngine;

public class ComboViewController : MonoBehaviour
{
    [SerializeField] private ComboViewRefs _comboViewRefs;

    private int _comboCount;
    private Sequence _sequence;

    public void Initialize()
    {
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        GameEvents.OnWrongSelection += OnWrongSelection;
        GameEvents.OnCorrectSelection += OnCorrectSelection;
    }

    private void UnRegisterEvents()
    {
        GameEvents.OnWrongSelection -= OnWrongSelection;
        GameEvents.OnCorrectSelection -= OnCorrectSelection;
    }

    private void OnCorrectSelection()
    {
        _comboCount++;

        CheckCombo();
    }

    private void OnWrongSelection()
    {
        _comboCount = 0;
    }

    private void CheckCombo()
    {
        if (_comboCount >= 2)
        {
            UpdateNumberSprite();
            PlayAnimation();
        }
    }

    private void UpdateNumberSprite()
    {
        var index = Mathf.Clamp(_comboCount, 0, _comboViewRefs.Numbers.Length - 1);
        _comboViewRefs.NumberValueImage.sprite = _comboViewRefs.Numbers[index];
    }

    private void PlayAnimation()
    {
        _comboViewRefs.Container.SetActive(true);

        _sequence?.Kill();
        _sequence = DOTween.Sequence()
            .Append(_comboViewRefs.TextTransform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack))
            .Insert(0.1f, _comboViewRefs.NumberTransform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack))
            .AppendInterval(1)
            .Append(_comboViewRefs.TextTransform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack))
            .Join(_comboViewRefs.NumberTransform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack))
            .OnComplete(AnimationCompleted);
    }

    private void AnimationCompleted()
    {
        _comboViewRefs.Container.SetActive(false);
    }

    public void Close()
    {
        UnRegisterEvents();
    }
}
