using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    [SerializeField]
    private string sceneName;

    public void switchToScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
