using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OurFractal
{
    /// <summary>
    /// Our Fractal Game Manager.
    /// </summary>
    public class OurFractalGameManager : MonoBehaviour
    {
        /// <summary>
        /// Game object name;
        /// </summary>
        public static string goName = "OurFractalManager";

        private CompositeDisposable compositeDisposable = null;
        private OurFractalManager manager = null;

        /// <summary>
        ///  Our Fractal Manager.
        ///  This property is automatically destroyed
        ///  when the gameobject is destroyed.
        ///  Therefore, do not call despose.
        /// </summary>
        public OurFractalManager Manager
        {
            get
            {
                return manager;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
            compositeDisposable = new CompositeDisposable();
            string path = Application.dataPath + "/files/mfd_test01";
            manager = new OurFractalManager(path, "table_test", "data_test");
            compositeDisposable.Add(manager);
        }

        /// <summary>
        /// This function call when app is started.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void StartOurFractal()
        {
            Debug.Log("Welcome to OurFractal");

            var go = new GameObject(goName);
            go.AddComponent<OurFractalGameManager>();
        }

        /// <summary>
        /// Dispose when destroy.
        /// </summary>
        void OnDestroy()
        {
            Debug.Log("Destoy");
            compositeDisposable.Clear();
        }
    }
}
