using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempWarpToLevel : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene("TestScene");
    }
}
