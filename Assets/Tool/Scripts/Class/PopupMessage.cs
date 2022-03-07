using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMessage : MonoBehaviour
{
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
                break;
            case MessageType.error:
                clone.GetComponentInChildren<Text>().color = Color.red;
                break;

        }
        Debug.LogWarning(message);
    }

    private void DeleteMessage()
    {
        if (clone != null)
        {
            Destroy(clone);
        }
    }
}
