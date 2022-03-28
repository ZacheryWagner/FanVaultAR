using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonManager : MonoBehaviour
{
    public void SwitchToScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
