using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    /// <summary>
    /// Swith buttun is True/False.
    /// </summary>
    public bool Bool {get; private set;}

    [SerializeField]
    private Color trueColor;

    [SerializeField]
    private Color falseColor;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnClick is called on click.
    void OnClick()
    {
        Bool = !Bool;
        switch (Bool)
        {
            case true:
                GetComponent<Image>().color = trueColor;
                break;
            case false:
                GetComponent<Image>().color = falseColor;
                break;
        }
    }
}
