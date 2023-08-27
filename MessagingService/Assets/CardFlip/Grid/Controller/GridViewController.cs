using UnityEngine;

public class GridViewController : BaseUIViewController<GridViewRefs>, IGridCard
{
    public override void RegisterEvents()
    {
    }

    public override void UnregisterEvents()
    {
    }

    public override void Show(object model = null)
    {
        base.Show(model);
    }

    void IGridCard.CardSelected(int cardId)
    {
    }
}
