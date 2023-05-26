using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBulletScript : MonoBehaviour
{
    public float speed = 3;
    public float damage = 20f;
    Transform player;
    Transform boss;
    private bool attackplayer = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  
        boss = GameObject.FindGameObjectWithTag("Boss").transform;    
    }

    void Update()
    {
        if(attackplayer){
            transform.position = Vector2.MoveTowards(transform.position,player.position,speed*Time.deltaTime);
        }else if (attackplayer ==false){
            transform.position = Vector2.MoveTowards(transform.position,boss.position,speed*Time.deltaTime);
            speed = 5;
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            other.GetComponent<PlayerHealth>().ReceiveDamage(damage);
            Destroy(gameObject);
        }else if(other.CompareTag("Boss") && attackplayer==false){
            other.GetComponent<EnemyFollowPlayer>().ReceiveDamage(30f);
            Destroy(gameObject);
        }
    }

    public void attackboss(){
        attackplayer=false;
    }
}
