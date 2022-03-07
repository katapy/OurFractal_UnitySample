using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Show popup message
/// </summary>
public class PopupMessage : MonoBehaviour
{
    /// <summary>
    /// Message type(message, waring, error)
    /// </summary>
    public enum MessageType
    {
        message,
        waring,
        error
    }

    [SerializeField]
    private Canvas popupCanvas;

    private GameObject clone = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clone != null && Input.GetMouseButtonDown(0))
        {
            DeleteMessage();
        }
    }

    /// <summary>
    /// Show popup message
    /// </summary>
    /// <param name="message"> desplayed message</param>
    /// <param name="messageType"> message type </param>
    public void ShowPopup(string message, MessageType messageType)
    {
        clone = Instantiate(popupCanvas.gameObject);
        clone.GetComponentInChildren<Text>().text = message;
        switch (messageType)
        {
            case MessageType.message:
                break;
            case MessageType.waring:
                clone.GetComponentInChildren<Text>().color = Color.yellow;
                Debug.LogWarning(message);
                break;
            case MessageType.error:
                clone.GetComponentInChildren<Text>().color = Color.red;
                Debug.LogError(message);
                break;

        }
    }

    /// <summary>
    /// Delete message.
    /// </summary>
    private void DeleteMessage()
    {
        if (clone != null)
        {
            Destroy(clone);
        }
    }
}
