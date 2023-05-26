using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField]
    private float health = 100f;
    private bool canHeal = true ;
    private PlayerInput mPlayerInput;

    public Image barradeVida;

    public void Start()
    {
        mPlayerInput = GetComponent<PlayerInput>();    
    }

    public void Update(){
        barradeVida.fillAmount = health/100f;
    }

    public void OnHealth(InputValue value){
        if (value.isPressed)
        {
            if (canHeal){
                if (health<=50f && health>0f){
                    health = health+50f;
                }else if(health>50f){
                    health = 100f;
                }
            }
            
        }
    }
    public void ReceiveDamage(float damage){
        health = health - damage;
        
    }
}
