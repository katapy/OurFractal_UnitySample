using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OurFractal
{
    public class OurFractalGameManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            string path = Application.temporaryCachePath + "/files/mfd_test01";
            Debug.Log("Our Fractal data path: " + path);
            using (var manager = new OurFractal.OurFractalManager(path, "table_test", "data_test"))
            {
                Debug.Log("add def: " + manager.AddDef(0x00100010, "test", OurFractal.DataType.Int, false));
                foreach (var tag in manager.DefList)
                {
                    Debug.Log("tag: " + tag);
                }

                using (OurFractal.Definition def = manager.GetDefinition(0x00100010))
                {
                    Debug.Log(def.Tag.ToString("X"));

                    Debug.Log("name: " + def.Name);
                    def.Explanation = "exp test";
                    Debug.Log("exp: " + def.Explanation);
                }

                manager.WriteDef();
                manager.ReadDef();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
