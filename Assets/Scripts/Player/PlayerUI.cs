using UnityEngine;
using TMPro;
///Class that gives user an UI message while the game is running
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Message;
    public void NewMessage(string promptMessage)
    {
        Message.text = promptMessage;
    }
}
