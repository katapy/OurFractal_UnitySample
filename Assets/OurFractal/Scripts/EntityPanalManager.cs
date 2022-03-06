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

        /// <summary>
        /// Nest of entity.
        /// </summary>
        public int Nest;

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

            // Delete same or more than deep enst enntity to avoid to hide enntity.
            for (var i = 0; i < transform.parent.childCount; i++)
            {
                if (transform.parent.GetChild(i).GetComponent<EntityPanalManager>().Nest > Nest)
                {
                    Destroy(transform.parent.GetChild(i).gameObject);
                }
            }

            // Create children.
            for(var i = 0; i < children.Length; i++)
            {
                /// Create child entity.
                var def = manager.GetDefinition(children[i]);
                var clone = Instantiate(this, transform.parent);
                clone.GetComponent<EntityPanalManager>().Tag = children[i];
                clone.GetComponent<EntityPanalManager>().Nest = Nest + 1;
                clone.GetComponentInChildren<Text>().text
                    = $"{def.ShowTag()}\n\r{def.Name}";

                /// Set entity position.
                var posX = transform.localPosition.x + (i - (children.Length - 1) / 2.0f) * GetComponent<RectTransform>().sizeDelta.x;
                var posY = transform.localPosition.y - GetComponent<RectTransform>().sizeDelta.y * 2;
                clone.transform.localPosition = new Vector3(posX, posY);
                clone.transform.SetAsFirstSibling();

                /// Create line which connect parent entity and child entity.
                var line = clone.transform.GetChild(0);
                line.position = Vector3.Lerp(transform.position, clone.transform.position, 0.5f);
                var rot = Mathf.Atan2(transform.localPosition.x - posX, transform.localPosition.y - posY);
                line.rotation = Quaternion.Euler(0, 0, - rot * Mathf.Rad2Deg);
                line.gameObject.SetActive(true);
                line.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(10, Vector3.Distance(transform.localPosition, clone.transform.localPosition));
            }
        }
    }
}
