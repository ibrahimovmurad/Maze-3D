using UnityEngine;
///The base interactable class
public abstract class Interactable : MonoBehaviour
{
    public string Message;
    public void BaseInteract() { 
        Interact();
    }
    protected virtual void Interact() { }
}