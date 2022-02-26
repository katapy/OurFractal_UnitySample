using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///Input Field Controller which restricted hex number and 4 digits.
/// </summary>
public class TagInputController : MonoBehaviour
{
    private InputField inputField;

    string beforeValue = "";

    // Start is called before the first frame update
    void Start()
    {
        inputField = this.GetComponent<InputField>();

        inputField.onValueChanged.AddListener(delegate { OnValueChanged(); });
        inputField.onEndEdit.AddListener(delegate { OnEndEdit(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValueChanged()
    {
        /// Tag must 4 digits and able to represent a hexadecimal number.
        if (string.IsNullOrWhiteSpace(inputField.text))
        {
            beforeValue = inputField.text;
        }
        else if (inputField.text.Length > 4)
        {
            inputField.text = beforeValue;
        }
        else if (!uint.TryParse(inputField.text, System.Globalization.NumberStyles.HexNumber, null, out uint _))
        {
            inputField.text = beforeValue;
        }
        else
        {
            beforeValue = inputField.text;
        }
    }

    private void OnEndEdit()
    {
        // if digit less 4, add 0.
        if (!string.IsNullOrWhiteSpace(inputField.text))
        {
            inputField.text = inputField.text.PadLeft(4, '0');
        }
    }
}
