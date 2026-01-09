using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TreeNodeUI : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text valueText;
    public TreeUIManager uiManager;

    private int? currentValue = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        uiManager.OnNodeClicked(this);
    }

    public bool HasValue() => currentValue != null;
    public int? GetValue() => currentValue;

    public void SetValue(int value)
    {
        currentValue = value;
        valueText.text = value.ToString();
    }

    public void Clear()
    {
        currentValue = null;
        valueText.text = "";
    }
}
