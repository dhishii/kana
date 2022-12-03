using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wLoadScene : MonoBehaviour
{
    public void LoadScene(int sceneByIndex)
    {
        SceneManager.LoadScene(sceneByIndex);
    }
}
