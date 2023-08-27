using UnityEngine;

public class ComboViewController : MonoBehaviour
{
    private int comboCount;

    public void Initialize()
    {
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        GameEvents.OnWrongSelection += OnWrongSelection;
        GameEvents.OnCorrectSelection += OnCorrectSelection;
    }

    private void OnCorrectSelection()
    {
        comboCount++;

        CheckCombo();
    }

    private void OnWrongSelection()
    {
        comboCount = 0;
    }

    private void CheckCombo()
    {
        if (comboCount >= 2)
        {
            //TODO : show combo
        }
    }
}
