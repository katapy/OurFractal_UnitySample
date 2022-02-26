using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Load "OurFractalAddDef" Scene.
/// </summary>
public class LoadAddDefButton : MonoBehaviour
{
    private Button button = null;

    // Start is called before the first frame update
    void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(onClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onClick()
    {
        SceneManager.LoadScene("OurFractalAddDef");
    }
}
