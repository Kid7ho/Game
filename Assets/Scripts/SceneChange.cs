﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void Scene_Change(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
