using UnityEngine;
///Class that makes player be able to interact with the interactables
public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask mask;
    private InputManager inputManager;
    private PlayerUI playerUI;
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        inputManager = GetComponent<InputManager>();
        playerUI = GetComponent<PlayerUI>();
    }
    void Update()
    {
        //If the player aims at an interactable, UI message is replaced with that interactable's message, otherwise it is replaced with an empty string 
        playerUI.NewMessage("");
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() is Interactable interactable)
            {
                playerUI.NewMessage(interactable.Message);
                //If the player interacts with an interactable, interactable's action is performed
                if (inputManager.OnFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
