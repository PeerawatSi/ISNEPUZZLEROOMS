using UnityEngine;

public class networkTrigger : MonoBehaviour
{
    public NetworkDialogue networkDialogue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnMouseDown()
    {
        networkDialogue.OpenPanel();
    }
}
