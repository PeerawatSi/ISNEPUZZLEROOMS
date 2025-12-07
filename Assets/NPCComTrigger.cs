using UnityEngine;

public class NPCComTrigger : MonoBehaviour
{
    public ComacrhDialogue dialogue;

    private void OnMouseDown()
    {
        dialogue.OpenPanel();
    }
}
