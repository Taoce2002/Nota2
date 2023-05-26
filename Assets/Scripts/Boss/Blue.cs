using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue : MonoBehaviour
{
    public float speed = 2f;
    Transform player;
    public float hpEnemy  = 1f; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;    
    }

    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position,player.position,speed*Time.deltaTime);
        if(hpEnemy < 0f){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            Debug.Log("X");
            other.GetComponent<PlayerMovement>().Slow();
            Destroy(gameObject);
        }
    }

    public void ReceiveDamage(float damage){
        hpEnemy = hpEnemy - damage;
    }
}
