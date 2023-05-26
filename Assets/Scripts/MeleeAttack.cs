using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    [SerializeField]
    private float damageMelee = 60f;

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy")){
            other.GetComponent<EnemyController>().ReceiveDamage(damageMelee);
        }
        else if(other.CompareTag("Boss")){
            other.GetComponent<EnemyFollowPlayer>().ReceiveDamage(30f);
        }else if(other.CompareTag("BulletBoss")){
            other.GetComponent<HomingBulletScript>().attackboss();
        }else if(other.CompareTag("Bat")){
            other.GetComponent<ScriptBat>().ReceiveDamage(damageMelee);
        }else if(other.CompareTag("Blue")){
            other.GetComponent<Blue>().ReceiveDamage(damageMelee);
        }
    }
}
