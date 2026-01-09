using UnityEngine;
using TMPro;

public class NumberButton : MonoBehaviour
{
    public int value;
    public TMP_Text valueText;
    private TreeUIManager ui;

    public void SetValue(int v, TreeUIManager manager)
    {
        value = v;
        ui = manager;
        valueText.text = v.ToString();
    }

    public void OnClick()
    {
        ui.SelectNumber(value);
    }
}
