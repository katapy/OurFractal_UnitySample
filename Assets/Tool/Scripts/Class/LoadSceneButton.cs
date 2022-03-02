using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SceneTool
{
    /// <summary>
    /// Load Scene.
    /// </summary>
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField]
        private string sceneName;

        /// <summary>
        /// Pass the value of the object to the next scene.
        /// </summary>
        public object[] SceneObj;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        // Update is called once per frame
        void Update()
        {

        }

        // OnClick is called on click.
        private void OnClick()
        {
            DontDestroyOnLoad(transform.root);
            SceneManager.sceneLoaded += GameSceneLoaded;
            SceneManager.LoadScene(sceneName);
        }

        // OnClick is called after game scene loaded.
        private void GameSceneLoaded(Scene next, LoadSceneMode mode)
        {
            // Get ILoadScene component in all of next scene.
            foreach (var go in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                ILoadScene[] loadScenes = go.GetComponents<ILoadScene>();
                if (loadScenes == null)
                    continue;
                foreach(var loadScene in loadScenes)
                {
                    loadScene.SceneLoaded(SceneObj);
                }
            }
            SceneManager.sceneLoaded -= GameSceneLoaded;
            Destroy(transform.root.gameObject);
        }
    }
}
