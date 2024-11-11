using UnityEngine;
///Class for the interactable button that is placed at the end of the level and finishes the level when interacted with
public class Button : Interactable
{
    protected override void Interact()
    {
        Menu.SceneDown();
    }
}
