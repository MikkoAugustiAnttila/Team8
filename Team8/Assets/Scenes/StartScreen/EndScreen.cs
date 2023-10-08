using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI thanks;
    
    // Start is called before the first frame update
    void Start()
    {
        thanks.fontSize = 15;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        while (thanks.fontSize < 115)
        {
            thanks.fontSize++;
        }
    }
}
