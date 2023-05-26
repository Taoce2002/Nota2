using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBat : MonoBehaviour
{
    public float speed = 2;
    public float hpEnemy  = 1;  
    public float damage = 10f;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,player.position,speed*Time.deltaTime);
        if(hpEnemy < 0f){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            other.GetComponent<PlayerHealth>().ReceiveDamage(damage);
            Destroy(gameObject);
        }
    }
    public void ReceiveDamage(float damage){
        hpEnemy = hpEnemy - damage;
    }
}
