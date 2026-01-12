using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    void OnMouseDown()
    {
        ButtonClickSound.instance.PlayClick();
    }
}
