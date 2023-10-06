using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class openManagerScene : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("ManagerScene", LoadSceneMode.Additive);
    }
}
