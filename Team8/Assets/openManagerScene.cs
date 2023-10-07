using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class openManagerScene : MonoBehaviour
{
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Management") == null)
        {
            SceneManager.LoadScene("ManagerScene", LoadSceneMode.Additive);
        }
        Destroy(gameObject);
    }
}
