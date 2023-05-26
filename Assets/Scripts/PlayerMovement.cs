using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject bala;

    [SerializeField]
    private float speed = 4f;
    

    private bool estadoEspada = true;
    private bool canAttack = true;
    
    public SpriteRenderer spriteRenderer;
    private Rigidbody2D mRb;
    private Vector3 mDirection = Vector3.zero;
    private Animator mAnimator;
    private PlayerInput mPlayerInput;
    private Transform hitBox;
    private Vector2 lastDirection;
    public Transform PanelPlayer;
    public Transform HeroeArmaUI;

    private void Start()
    {
        mRb = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mPlayerInput = GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastDirection = new Vector2(0,-1);

        hitBox = transform.Find("HitBox");

        ConversationManager.Instance.OnConversationStop += OnConversationStopDelegate;
        
    }

    private void OnConversationStopDelegate()
    {
        PanelPlayer.gameObject.SetActive(true);
        mPlayerInput.SwitchCurrentActionMap("Player");
    }

    private void Update()
    {
        if (mDirection != Vector3.zero)
        {
            mAnimator.SetBool("IsMoving", true);
            mAnimator.SetFloat("Horizontal", mDirection.x);
            mAnimator.SetFloat("Vertical", mDirection.y);
            lastDirection = new Vector2(mDirection.x,mDirection.y);
        }else
        {
            // Quieto
            mAnimator.SetBool("IsMoving", false);
        }
    }

    private void FixedUpdate()
    {
        mRb.MovePosition(
            transform.position + (mDirection * speed * Time.fixedDeltaTime)
        );
    }

    public void OnMove(InputValue value)
    {
        mDirection = value.Get<Vector2>().normalized;
    }

    public void OnNext(InputValue value)
    {
        if (value.isPressed)
        {
            ConversationManager.Instance.NextConversation();
        }
    }

    public void OnCancel(InputValue value)
    {
        if (value.isPressed)
        {
            ConversationManager.Instance.StopConversation();
        }
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {   
            if(canAttack){
                if(estadoEspada){
                    mAnimator.SetTrigger("Attack");
                    hitBox.gameObject.SetActive(true);

                }else{
                    DisparoScript disparo = Instantiate(bala, transform.position,Quaternion.identity).GetComponent<DisparoScript>();
                    disparo.Setup(lastDirection);
                }
                StartCoroutine(TiempoAttack());
            } 
        }
    }

    private IEnumerator TiempoAttack(){
        canAttack = false;
        yield return new WaitForSeconds (0.5f);
        canAttack = true;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        Conversation conversation;
        if (other.transform.TryGetComponent<Conversation>(out conversation))
        {
            mPlayerInput.SwitchCurrentActionMap("Conversation");
            PanelPlayer.gameObject.SetActive(false);
            ConversationManager.Instance.StartConversation(conversation);
        }
    }

    public void DisableHitBox()
    {
        hitBox.gameObject.SetActive(false);
    }

    

    public void OnCambiarArma(InputValue value){
        if (value.isPressed)
        {
            if(estadoEspada){
                estadoEspada = false;
                HeroeArmaUI.Find("TextoArma")
                .GetComponent<TextMeshProUGUI>().text = "Bola de nieve";

                HeroeArmaUI.Find("Sword").gameObject.SetActive(false);
                HeroeArmaUI.Find("Snowball").gameObject.SetActive(true);

           }else{
                estadoEspada = true;

                HeroeArmaUI.Find("TextoArma")
                .GetComponent<TextMeshProUGUI>().text = "Espada de mano";

                HeroeArmaUI.Find("Sword").gameObject.SetActive(true);
                HeroeArmaUI.Find("Snowball").gameObject.SetActive(false);
           }
        }
    }

    public void Slow(){
        StartCoroutine(SlowBlue());
    }
    private IEnumerator SlowBlue(){
        speed = 1.5f;
        Color celeste = new Color(0.5f, 0.7f, 1f);
        spriteRenderer.color = celeste;
        yield return new WaitForSeconds (1.5f);
        speed = 4f;
        spriteRenderer.color = Color.white;
    }
    
}
