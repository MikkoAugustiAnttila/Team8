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
    private Coroutine displayTextLetterByLetterCoroutine;
    public GameObject projectile;
    [SerializeField] private GameObject pivot;

    public bool enableTextBox;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private Image boxForText;
    public float letterDelay;
    [SerializeField] private float sentenceDelay;
    private int index;
    [SerializeField] private AudioClip textAudio;
    public AudioSource BGM;
    private AudioClip nextClip;


    public string lastBug;
    public int progression = 1;
    
    

    private void Awake()
    {
        basemanagement = this;
        pivot = GameObject.FindGameObjectWithTag("pivot");
    }
    public void Start()
    {
        
        
        
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
        if (displayTextLetterByLetterCoroutine != null)
        {
            StopCoroutine(displayTextLetterByLetterCoroutine);
        }

        textBox.text = "";
        enableTextBox = true;
        displayChunksCoroutine = StartCoroutine(DisplayChunksWithDelay(chunkSet, returnToStageManager));
    }

    private IEnumerator DisplayChunksWithDelay(string[] chunkSet, bool returnToStageManager)
    {
        foreach (string chunk in chunkSet)
        {
            // Stop the previous coroutine to give precedence to the new chunk
            if (displayTextLetterByLetterCoroutine != null)
            {
                StopCoroutine(displayTextLetterByLetterCoroutine);
            }

            displayTextLetterByLetterCoroutine = StartCoroutine(DisplayTextLetterByLetter(chunk));

            // Wait for the current chunk to be fully displayed
            yield return displayTextLetterByLetterCoroutine;

            // Wait for seconds after fully displaying the current chunk
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
        //This comment is here to allow commit
        textBox.text = "";

        int letterCount = 0;

        foreach (char letter in text)
        {
            textBox.text += letter;

            if (letterCount % 3 == 0)
            {
                SoundManager.soundManagement.playSound(textAudio);
            }

            letterCount++;

            yield return new WaitForSeconds(letterDelay);
        }
    }

    public void createProjectile()
    {
        if (stageManager.stateManagement.shotsLeft > 0)
        {
            GameObject newProjectile = Instantiate(projectile, pivot.transform.position, quaternion.identity);
            newProjectile.transform.parent = null;
        }
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
        BackgroundMusicChaner();


    }
    public void BackgroundMusicChaner()
    {
        if (SceneManager.GetActiveScene().name == "StartScreen" || SceneManager.GetActiveScene().name == "LevelChanger")
        {
            nextClip = SoundManager.soundManagement.BackgroundMusic[0];

        }
        else
        {
            nextClip = SoundManager.soundManagement.BackgroundMusic[1];

        }



        if (nextClip != BGM.clip)
        {
            BGM.Stop();
            BGM.clip= nextClip;
            BGM.Play();
            //checks which scene is played and sets audio correctly
            
        }
        
    }

}
