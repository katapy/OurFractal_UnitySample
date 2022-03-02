using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SceneTool;

namespace OurFractal
{
    /// <summary>
    /// "OurFractalDefList" Scene manager.
    /// </summary>
    public class DefListSceneManager : MonoBehaviour
    {
        [SerializeField]
        private Button sampleButton;

        [SerializeField]
        private GameObject bottomPanel;

        // Start is called before the first frame update
        void Start()
        {
            ShowDefList();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShowDefList();
            }
        }

        /// <summary>
        /// Shows a list of definition.
        /// </summary>
        private void ShowDefList()
        {
            var manager = GameObject.Find(OurFractalGameManager.goName).GetComponent<OurFractalGameManager>().Manager;
            if (manager == null)
            {
                return;
            }
            foreach (var tag in manager.DefList)
            {
                var clone = Instantiate(sampleButton, sampleButton.transform.parent);
                clone.gameObject.SetActive(true);
                var def = manager.GetDefinition(uint.Parse(tag, System.Globalization.NumberStyles.HexNumber));
                clone.GetComponentInChildren<Text>().text
                    = def.ShowTag() + "\n\t" + def.Name;
                clone.gameObject.GetComponent<LoadSceneButton>().SceneObj
                    = new object[] { def };
            }
            bottomPanel.transform.SetAsLastSibling();
        }
    }
}