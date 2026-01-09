using UnityEngine;

public class BoardInteract : MonoBehaviour
{
    public GameObject treeUIPanel;

    void OnMouseDown()
    {
        treeUIPanel.SetActive(true);
    }
}
