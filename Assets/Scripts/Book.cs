using UnityEngine;
using TMPro;

public class Book : MonoBehaviour
{
    public int number;
    public bool canClick = false;

    public TextMeshPro numberText;

    void Start()
    {
        if (numberText != null)
            numberText.gameObject.SetActive(false);
    }

    public void SetNumber(int n)
    {
        number = n;

        if (numberText != null)
        {
            numberText.text = number.ToString();
        }
    }

    void OnMouseDown()
    {
        if (!canClick) return;

        if (numberText != null)
            numberText.gameObject.SetActive(true);

        Debug.Log("คลิกหนังสือ เลข = " + number);
    }
}
