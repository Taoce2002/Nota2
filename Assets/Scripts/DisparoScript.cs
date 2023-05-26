using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoScript : MonoBehaviour
{

    [SerializeField]
    private float velocidad = 10f;
    [SerializeField]
    private float damage = 25f;
    [SerializeField]
    public Rigidbody2D mRb;
    
    private void Start(){

    }

    public void Setup (Vector2 direction){
        mRb.velocity  = direction.normalized*velocidad;
        transform.rotation  = Quaternion.Euler(direction);
    }


    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy")){
            other.GetComponent<EnemyController>().ReceiveDamage(damage);
            Destroy(gameObject);
        }
        else if(other.CompareTag("Boss")){
            other.GetComponent<EnemyFollowPlayer>().ReceiveDamage(15f);
            Destroy(gameObject);
        }
    }

}
