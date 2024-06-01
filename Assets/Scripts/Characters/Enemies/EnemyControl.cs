using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour, IKillable
{
    public GameObject Player;
    private PlayerControl playerControl;
    private Animator myEnemyAnimator;
    public AudioClip ZombieKill;
    private Vector3 randomPos;
    private Vector3 direction;
    [SerializeField] private float ChaseRadius = 10;
    private CharacterMovement enemyMovement;
    private EnemyAnimationControl enemyAnimationController;
    public CharacterStats myEnemyStats;

    private float idleTimerCount;
    private float randomPosTimer = 4;
    private int randomPosMagnitudeMultiplier = 10;
    private bool isAlive = true;
    private bool isReactingToDamage;

    public event Action<GameObject> ZombieDied;
    public static event Action<Animator, Vector2> DamageEvent;


    #region DebugFunctions
    [Header("Debug Helpers")]
    public bool CanChase;
    public bool ShouldDrawGizmos;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag(Tags.Player);
        playerControl = Player.GetComponent<PlayerControl>();

        enemyMovement = GetComponent<CharacterMovement>();

        enemyAnimationController = GetComponent<EnemyAnimationControl>();
        myEnemyAnimator = GetComponent<Animator>();

        myEnemyStats = GetComponent<CharacterStats>();

        SetRandomZombieSkin();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Player.transform.position);

        if (isAlive && !isReactingToDamage)
        {
            enemyMovement.Rotation(direction);

            enemyAnimationController.MovementAnim(direction.magnitude);

            if (CanChase)
            {
                if (distancia < 2.5)
                {
                    enemyAnimationController.AttackAnim(true);
                }
                else if (distancia < ChaseRadius)
                {
                    chasePlayer();
                }
                else
                {
                    roamingMove();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (ShouldDrawGizmos)
        {
            Gizmos.DrawWireSphere(this.transform.position, ChaseRadius);
        }
    }

    void HitPlayer()
    {
        playerControl.DealDamage(20);
        Debug.Log("HitPlayer on enemy Control Trigered");
    }

    void SetRandomZombieSkin()
    {
        int EnemyRandomizer = UnityEngine.Random.Range(1, 28);
        transform.GetChild(EnemyRandomizer).gameObject.SetActive(true);
    }

    public void DealDamage(int dano)
    {
        myEnemyStats.Life -= dano;
        if (myEnemyStats.Life <= 0)
        {
            Die();
        }
        else
        {
            isReactingToDamage = true;
            Vector3 playerDirection = Player.transform.position - this.transform.position;

            Vector3 localDirectionToBullet = this.transform.InverseTransformDirection(playerDirection);

            Vector2 direction2D = new Vector2(localDirectionToBullet.x, localDirectionToBullet.z).normalized;


            DamageEvent?.Invoke(myEnemyAnimator, direction2D);

        }

    }

    public void HitFinished()
    {
        isReactingToDamage = false;
    }

    public void Heal(int cura)
    {
        myEnemyStats.Life += cura;
        if (myEnemyStats.Life >= myEnemyStats.InitialLife)
        {
            myEnemyStats.Life = myEnemyStats.InitialLife;
        }
    }

    public void Die()
    {
        isAlive = false;
        enemyAnimationController.DeathAnim(true);
        enemyMovement.DisableRB();
        Destroy(gameObject.GetComponent<CapsuleCollider>());
        AudioControl.instancia.PlayOneShot(ZombieKill);
        Destroy(this);
        // Destroy(gameObject);
        ZombieDied?.Invoke(this.gameObject);
    }

    void roamingMove()
    {
        myEnemyStats.Velocity = 4;
        idleTimerCount -= Time.deltaTime;

        if (idleTimerCount <= 0)
        {
            randomPos = RamdomizePosition();
            idleTimerCount += randomPosTimer;
        }

        bool hasReached = Vector3.Distance(transform.position, randomPos) <= 0.07;

        if (hasReached == false)
        {
            direction = randomPos - transform.position;
            enemyMovement.Movement(direction, myEnemyStats.Velocity);
        }
    }

    void chasePlayer()
    {
        myEnemyStats.Velocity = 7;

        direction = Player.transform.position - transform.position;

        enemyMovement.Movement(direction, myEnemyStats.Velocity);

        enemyAnimationController.AttackAnim(false);

    }

    Vector3 RamdomizePosition()
    {
        Vector3 position = UnityEngine.Random.insideUnitSphere * randomPosMagnitudeMultiplier;
        position += transform.position;
        position.y = transform.position.y;

        return position;
    }
}
