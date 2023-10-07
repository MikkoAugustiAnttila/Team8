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
    private Coroutine displayChunksCoroutine;
    private Coroutine displayTextCoroutine;
    public GameObject projectile;
    [SerializeField] private GameObject pivot;

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
        if (displayChunksCoroutine != null)
        {
            StopCoroutine(displayChunksCoroutine);
        }
        if (displayTextCoroutine != null)
        {
            StopCoroutine(displayTextCoroutine);
        }

        textBox.text = "";
        enableTextBox = true;
        displayChunksCoroutine = StartCoroutine(DisplayChunksWithDelay(chunkSet, returnToStageManager));
    }

    private IEnumerator DisplayChunksWithDelay(string[] chunkSet, bool returnToStageManager)
    {
        foreach (string chunk in chunkSet)
        {
            if (displayTextCoroutine != null)
            {
                StopCoroutine(displayTextCoroutine);
            }
            displayTextCoroutine = StartCoroutine(DisplayTextLetterByLetter(chunk));

            // Pause for a moment before displaying the next chunk
            yield return new WaitForSeconds(sentenceDelay);
        }

        enableTextBox = false;
        if (returnToStageManager)
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
        // Ensure that "ManagerScene" is loaded and get its index
        int managerSceneIndex = SceneManager.GetSceneByName("ManagerScene").buildIndex;
        if (!SceneManager.GetSceneByBuildIndex(managerSceneIndex).isLoaded)
        {
            SceneManager.LoadScene("ManagerScene", LoadSceneMode.Additive);
        }

        // Unload all scenes except "ManagerScene"
        Scene[] scenes = SceneManager.GetAllScenes();
        foreach (Scene scene in scenes)
        {
            if (scene.buildIndex != managerSceneIndex)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }

        // Load the new scene additively
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        
        // Delay for a short time before setting the new scene as active
        StartCoroutine(SetActiveSceneDelayed(name));
    }

    private IEnumerator SetActiveSceneDelayed(string name)
    {
        yield return new WaitForSeconds(0.1f);
        
        Scene newScene = SceneManager.GetSceneByName(name);
        if (newScene.isLoaded)
        {
            SceneManager.SetActiveScene(newScene);
            pivot = GameObject.FindGameObjectWithTag("pivot");
        }
        else
        {
            Debug.LogError("Scene not loaded: " + name);
        }
    }
}
