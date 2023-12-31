using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeingHit : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite _brokenSprite;
    int numberOfHit = 0;
    // Start is called before the first frame update
    void Start()
    {
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
            spriteRenderer.sprite = _brokenSprite; 
            numberOfHit++;
            Debug.Log("hit");
            if (numberOfHit == 2)
            {
                Destroy(gameObject);
            }
        }
    }
}
