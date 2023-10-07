using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenMetal : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite _brokenSprite;
    int numberOfHit = 0;
    //private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(coroutine);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            //Debug.Log("hit"+ numberOfHit);
            spriteRenderer.sprite = _brokenSprite;
            //StartCoroutine(waitfornextshot());
            numberOfHit++;
            
            if (numberOfHit == 2)
            {
                //StartCoroutine(waitfornextshot());
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator waitfornextshot() 
    {
        yield return new WaitForSeconds(1f);
    }
}
