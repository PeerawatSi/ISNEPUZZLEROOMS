using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public IntroDialogue dialogue;

    private void OnMouseDown()
    {
        dialogue.OpenPanel();
    }
}
