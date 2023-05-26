using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    #region Public Properties
    public float WakeDistance = 5f;
    public float Speed = 2f;
    public float hpEnemy = 100f;
    public float AttackDistance = 0.5f;

    #endregion

    #region Components
    public Transform Player;
    public SpriteRenderer spriteRenderer {private set; get;}
    public Rigidbody2D rb { private set; get; }
    public Animator animator { private set; get; }
    
    public bool AttackingEnd { set; get; } = false;
    public Transform hitBox { private set; get; }

    #endregion

    #region Private Properties
    private FSM<EnemyController> mFSM;

    #endregion

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hitBox = transform.Find("HitBox");
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        // Creo la maquina de estado finita
        mFSM = new FSM<EnemyController>(new Enemy.IdleState(this));
        mFSM.Begin();  // prendo la mquina de estados
    }

    private void FixedUpdate()
    {
        mFSM.Tick(Time.fixedDeltaTime);
    }

    public void SetAttackingEnd()
    {
        AttackingEnd = true;
    }

    private void Update(){
        if(hpEnemy < 0f){
            Destroy(gameObject);
        }
    }

    public void ReceiveDamage(float damage){
        hpEnemy = hpEnemy - damage;
        WakeDistance = 100f;
        Speed = 3.5f;
        spriteRenderer.color = Color.red;
    }
}
