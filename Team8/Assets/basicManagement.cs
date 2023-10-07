using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class basicManagement : MonoBehaviour
{
    public static basicManagement basemanagement;
    public GameObject projectile;
    private GameObject pivot;

    public bool enableTextBox;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private Image boxForText;
    public float letterDelay;
    [SerializeField] private float sentenceDelay;
    private int index;

    public string lastBug;
    
    

    private void Awake()
    {
        basemanagement = this;
        pivot = GameObject.FindGameObjectWithTag("pivot");
    }

    private void Update()
    {
        if (enableTextBox == true)
        {
            textBox.enabled = true;
            boxForText.enabled = true;
        }
        else
        {
            textBox.enabled = false;
            boxForText.enabled = false;
        }
    }
    
    public void DialogChunk(bool returnToStageManager, params string[] chunkSet)
    {
        StopAllCoroutines();
        enableTextBox = true;
        StartCoroutine(DisplayChunksWithDelay(chunkSet, returnToStageManager));
    }

    private IEnumerator DisplayChunksWithDelay(string[] chunkSet, bool returnToStageManager)
    {
        foreach (string chunk in chunkSet)
        {
            yield return StartCoroutine(DisplayTextLetterByLetter(chunk));
            // Pause for a moment before displaying the next chunk
            yield return new WaitForSeconds(sentenceDelay);
        }

        enableTextBox = false;
        if (returnToStageManager == true)
        {
            stageManager.stateManagement.managementSignal = true;
        }
    }

    private IEnumerator DisplayTextLetterByLetter(string text)
    {
        textBox.text = ""; // Clear the text box

        foreach (char letter in text)
        {
            textBox.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }
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
