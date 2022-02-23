using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OurFractal
{
    public class DefListSceneManager : MonoBehaviour
    {
        [SerializeField]
        private Button sampleButton;

        // Start is called before the first frame update
        void Start()
        {
            using (var manager = new OurFractal.OurFractalManager("./files/mfd_test01", "table_test", "data_test"))
            {
                Debug.Log("add def: " + manager.AddDef(0x00100010, "test", OurFractal.DataType.Int, false));
                foreach (var tag in manager.DefList)
                {
                    var clone = Instantiate(sampleButton, sampleButton.transform.parent);
                    clone.gameObject.SetActive(true);
                    var def = manager.GetDefinition(uint.Parse(tag, System.Globalization.NumberStyles.HexNumber));
                    clone.GetComponentInChildren<Text>().text
                        = def.ShowTag() + "\n\t" + def.Name;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}