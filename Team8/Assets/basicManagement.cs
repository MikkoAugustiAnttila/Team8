using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class basicManagement : MonoBehaviour
{
    public static basicManagement basemanagement;
    public GameObject projectile;
    private GameObject pivot;
    

    private void Awake()
    {
        basemanagement = this;
        pivot = GameObject.FindGameObjectWithTag("pivot");
    }

    public void createProjectile()
    {
        GameObject newProjectile = Instantiate(projectile, pivot.transform.position, quaternion.identity);
        newProjectile.transform.parent = null;
    }

    public void ChangeToScene(string name)
    {
        Debug.Log(name);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
        StartCoroutine("delay", name);
    }

    IEnumerator delay(string name)
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
        Debug.Log(SceneManager.GetSceneByName(name));
    }
}
