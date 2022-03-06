using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SceneTool;
using System;

namespace OurFractal
{
    /// <summary>
    /// "OurFractalAddDef" Scene manager.
    /// </summary>
    public class AddDefSceneManager : MonoBehaviour, ILoadScene
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

        [SerializeField]
        private Toggle isChildOf0Toggle;

        [SerializeField]
        private GameObject ChildrenPanel;

        private bool isUpdate;

        // Start is called before the first frame update
        void Start()
        {
            applyButton.onClick.AddListener(OnClickApplyButton);

            var manager = GameObject.Find(OurFractalGameManager.goName).
                GetComponent<OurFractalGameManager>().Manager;

            // Create select children panel. 
            foreach (var tag in manager.DefList)
            {
                // Exclude (0000,0000) tag.
                if (tag == "00000000")
                {
                    continue;
                }

                var def = manager.GetDefinition(uint.Parse(tag, System.Globalization.NumberStyles.HexNumber));
                var panel = Instantiate(ChildrenPanel, ChildrenPanel.transform.parent);
                panel.name = tag;
                panel.GetComponentInChildren<Text>().text
                    = $"{def.ShowTag()}\n\t{def.Name}";
                panel.SetActive(true);
            }
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
                switch (isUpdate)
                {
                    case true:
                        UpdateDef();
                        break;
                    case false:
                        AddDef();
                        break;
                }
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
            DataType dataType = (DataType)Enum.ToObject(typeof(DataType), dataTypeDropdown.value);
            string exp = expInput.text;

            var manager = GameObject.Find(OurFractalGameManager.goName).
                GetComponent<OurFractalGameManager>().Manager;
            manager.AddDef(tag, name, dataType, false);
            manager.GetDefinition(tag).Explanation = exp;
            if (isChildOf0Toggle.isOn)
            {
                Debug.Log("Add child of 0000_0000");
                manager.GetDefinition(0).AddChildren(manager.GetDefinition(tag));
            }

            foreach (var switchButton in ChildrenPanel.transform.parent.
                GetComponentsInChildren<SwitchButton>())
            {
                if (switchButton.Bool)
                {
                    var child = manager.GetDefinition(switchButton.transform.parent.name);
                    manager.GetDefinition(tag).AddChildren(child);
                }
            }
            manager.WriteDef();

            Debug.Log("Success to add definition");
            UnityEngine.SceneManagement.SceneManager.LoadScene("OurFractalDefList");
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateDef()
        {
            uint tag = uint.Parse(groupTagInput.text + elementTagInput.text,
                System.Globalization.NumberStyles.HexNumber);
            string exp = expInput.text;

            var manager = GameObject.Find(OurFractalGameManager.goName).
                GetComponent<OurFractalGameManager>().Manager;
            manager.GetDefinition(tag).Explanation = exp;
            if (isChildOf0Toggle.isOn && isChildOf0Toggle.interactable)
            {
                manager.GetDefinition(0).AddChildren(manager.GetDefinition(tag));
            }

            foreach (var switchButton in ChildrenPanel.transform.parent.GetComponentsInChildren<SwitchButton>())
            {
                if (switchButton.Bool)
                {
                    var child = manager.GetDefinition(switchButton.transform.parent.name);
                    manager.GetDefinition(tag).AddChildren(child);
                }
            }

            manager.WriteDef();

            Debug.Log("Success to update definition");
            UnityEngine.SceneManagement.SceneManager.LoadScene("OurFractalDefList");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void SceneLoaded(object[] obj)
        {
            Definition def = obj?[0] as Definition;
            if (def != null)
            {
                Debug.Log("scene loaded: " + (obj[0] as Definition).ShowTag());

                StartCoroutine(SetInitial(def));
            }
        }

        /// <summary>
        /// Initial setting.
        /// </summary>
        /// <param name="def"></param>
        /// <returns></returns>
        private IEnumerator SetInitial(Definition def)
        {
            yield return null;

            var tag = def.Tag.ToString("X8");
            groupTagInput.text = tag.Substring(0, 4);
            elementTagInput.text = tag.Substring(4, 4);
            dataTypeDropdown.value = (int)def.DataType;
            nameInput.text = def.Name;
            expInput.text = def.Explanation;
            var manager = GameObject.Find(OurFractalGameManager.goName).
                GetComponent<OurFractalGameManager>().Manager;
            foreach (var child in manager.GetDefinition(0).Children ?? new string[] { })
            {
                if (child == tag)
                {
                    isChildOf0Toggle.isOn = true;
                    break;
                }
            }

            // These value cannot convert.
            groupTagInput.interactable = false;
            elementTagInput.interactable = false;
            dataTypeDropdown.interactable = false;
            nameInput.interactable = false;

            // If you set child once, you cannnot exclude child.
            isChildOf0Toggle.interactable = !isChildOf0Toggle.isOn;
            var parent = ChildrenPanel.transform.parent;
            for (var i = 0; i < parent.childCount; i++)
            {
                if (def.HasChild(parent.GetChild(i).name))
                {
                    parent.GetChild(i).GetComponentInChildren<Button>().interactable = false;
                }
            }

            isUpdate = true;
        }
    }
}