using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OurFractal
{
    /// <summary>
    /// Enitity panel manager.
    /// </summary>
    public class EntityPanalManager : MonoBehaviour
    {
        /// <summary>
        /// Entity tag.
        /// </summary>
        public string Tag;

        private OurFractalManager manager;

        private bool isShowChildren;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        // Awake is called when script loaded.
        private void Awake()
        {
            manager = GameObject.Find(OurFractalGameManager.goName).
                GetComponent<OurFractalGameManager>().Manager;

            GetComponentInChildren<Button>().
                onClick.AddListener(OnClickShowShildrenButton);

            if (string.IsNullOrEmpty(Tag))
            {
                var def = manager.GetDefinition(Tag);
                GetComponentInChildren<Text>().text
                    = $"{def.ShowTag()}\n\t{def.Name}";
            }
        }

        // OnClickShowShildrenButton is called after button clicked.
        private void OnClickShowShildrenButton()
        {
            isShowChildren = !isShowChildren;

            switch (isShowChildren)
            {
                case true:
                    CreateChildrenEntity();
                    break;
                case false:
                    break;
            }
        }

        /// <summary>
        /// Create chidlren enttity.
        /// </summary>
        private void CreateChildrenEntity()
        {
            var children = manager.GetDefinition(Tag).Children;
            if (children == null)
            {
                Debug.LogWarning("Children does not exit in this definition");
                return;
            }
            foreach (var child in children)
            {
                var def = manager.GetDefinition(child);
                var clone = Instantiate(this, transform.parent);
                clone.transform.localPosition += new Vector3(0, -240, 0);
                clone.GetComponent<EntityPanalManager>().Tag = child;
                clone.GetComponentInChildren<Text>().text
                    = $"{def.ShowTag()}\n\r{def.Name}";
            }
        }
    }
}
