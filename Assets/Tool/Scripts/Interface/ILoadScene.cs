using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneTool
{
    /// <summary>
    /// This interface is used for scene loaded function.
    /// </summary>
    interface ILoadScene
    {
        /// <summary>
        /// Called when scene loaded.
        /// </summary>
        /// <param name="obj"></param>
        public void SceneLoaded(object[] obj);
    }
}