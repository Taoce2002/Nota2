using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyFollowPlayer : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 5f;
    [SerializeField]
    private float hpEnemy = 300f;
    private float nextFireTime;
    
    public GameObject blue;
    public GameObject bullet;
    public GameObject enemyBat;
    private Animator mAnimator;
    public GameObject enemy;
    public GameObject bulletParent;
    private Transform player;
    public Transform PanelBoss;
    public Image vidaBossUI;
    private Vector3 mDirection;
    public Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        mDirection = (player.position - transform.position).normalized;
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if(distanceFromPlayer<lineOfSite && distanceFromPlayer>shootingRange){
            transform.position = Vector2.MoveTowards(this.transform.position,player.position,speed*Time.deltaTime);
            activarUI();
        }
        else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time){
            if(hpEnemy<150f){
                StartCoroutine(SpawnEnemy());
                nextFireTime = Time.time + 3f;
            }else{
                Instantiate(bullet,bulletParent.transform.position,Quaternion.identity);
                nextFireTime = Time.time + fireRate;
            }  
            
        }
        if(hpEnemy <= 0f){
            Destroy(gameObject);
        }

        vidaBossUI.fillAmount = hpEnemy/300f;

        if (mDirection != Vector3.zero)
            {
                animator.SetBool("IsMoving", true);
                animator.SetFloat("Horizontal", mDirection.x);
                animator.SetFloat("Vertical", mDirection.y);
            }
        else{
            animator.SetBool("IsMoving", false);
        }
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,lineOfSite);
        Gizmos.DrawWireSphere(transform.position,shootingRange);
    }

    public void ReceiveDamage(float damage){
        hpEnemy = hpEnemy - damage;
        if(hpEnemy>150){
            Instantiate(enemyBat,bulletParent.transform.position,Quaternion.identity);
        }

        fireRate = 2f;
    }
    private void activarUI(){
        PanelBoss.gameObject.SetActive(true);
    }

    private IEnumerator SpawnEnemy(){
        
        Instantiate(enemy,bulletParent.transform.position,Quaternion.identity);
        yield return new WaitForSeconds (1.5f);
        Instantiate(blue,bulletParent.transform.position,Quaternion.identity);
    }
}
