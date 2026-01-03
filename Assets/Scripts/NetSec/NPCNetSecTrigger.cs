using UnityEngine;

public class NPCNetsecTrigger : MonoBehaviour
{
    public NetsecDialogue dialogue;

    private void OnMouseDown()
    {
        dialogue.OpenPanel();
    }
}
