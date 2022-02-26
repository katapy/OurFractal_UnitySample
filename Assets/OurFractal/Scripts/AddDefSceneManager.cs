using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OurFractal
{
    /// <summary>
    /// "OurFractalAddDef" Scene manager.
    /// </summary>
    public class AddDefSceneManager : MonoBehaviour
    {
        [SerializeField]
        private Button applyButton;

        [SerializeField]
        private InputField groupTagInput;

        [SerializeField]
        private InputField elementTagInput;

        [SerializeField]
        private InputField nameInput;

        [SerializeField]
        private Dropdown dataTypeDropdown;

        [SerializeField]
        private InputField expInput;

        // Start is called before the first frame update
        void Start()
        {
            applyButton.onClick.AddListener(OnClickApplyButton);
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// On Click Apply Button.
        /// </summary>
        private void OnClickApplyButton()
        {
            try
            {
                AddDef();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        }

        /// <summary>
        /// Add definition
        /// </summary>
        private void AddDef()
        {
            uint tag = uint.Parse(groupTagInput.text + elementTagInput.text,
                System.Globalization.NumberStyles.HexNumber);
            string name = nameInput.text;
            DataType dataType = DataType.Int;
            switch (dataTypeDropdown.value)
            {
                case 1:
                    dataType = DataType.Int;
                    break;
                case 2:
                    dataType = DataType.Float;
                    break;
                case 3:
                    dataType = DataType.String;
                    break;
            }
            string exp = expInput.text;

            var manager = GameObject.Find(OurFractalGameManager.goName).GetComponent<OurFractalGameManager>().Manager;
            manager.AddDef(tag, name, dataType, false);
            manager.GetDefinition(tag).Explanation = exp;
            manager.WriteDef();

            Debug.Log("Success to add definition");
            UnityEngine.SceneManagement.SceneManager.LoadScene("OurFractalDefList");
        }
    }
}