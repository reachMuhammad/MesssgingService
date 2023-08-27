using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIViewController : BaseUIViewController<UIViewRefs>
{
    private readonly Dictionary<Views, BaseUIViewController> _activeViews = new Dictionary<Views, BaseUIViewController>();

    public override void RegisterEvents()
    {
        GameEvents.OnShowMenu += ShowView;
        GameEvents.OnCloseMenu += CloseView;
    }

    public override void UnregisterEvents()
    {
        GameEvents.OnShowMenu -= ShowView;
        GameEvents.OnCloseMenu -= CloseView;
    }

    private void ShowView(Views viewId,object model = null)
    {
        if (_activeViews.ContainsKey(viewId)) return;

        var view = InstantiateView(viewId);
        view.transform.SetAsLastSibling();
        view.Show(model);

        _activeViews.Add(viewId, view);
    }

    private BaseUIViewController InstantiateView(Views viewId)
    {
        var viewToShow = Instantiate(Resources.Load<BaseUIViewController>(viewId.ToString()));
        viewToShow.transform.SetParent(_ViewRefs.ViewContainer);

        var rectTransform = viewToShow.GetComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one * 0.5f;
        rectTransform.offsetMin = rectTransform.offsetMax = Vector2.zero;

        rectTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);

        return viewToShow;
    }

    private void CloseView(Views viewId)
    {
        if (_activeViews.Count == 0 || !_activeViews.ContainsKey(viewId)) return;

        var view = _activeViews[viewId];
        view.Close();

        _activeViews.Remove(viewId);

        Destroy(view.gameObject);
    }
}
