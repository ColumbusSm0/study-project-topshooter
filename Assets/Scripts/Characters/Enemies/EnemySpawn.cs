using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public GameObject Enemy;
    [SerializeField] private int maxZombiesAlive;
    [SerializeField] private int ZombiesAlive;

    private GameObject Player;
    [SerializeField] private float Timer = 0;
    public float TimerGoal = 2;

    public LayerMask EnemiesLayer;

    private float spawnRange = 3;

    public float minDistanceFromPlayer;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag(Tags.Player);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,Player.transform.position) > minDistanceFromPlayer)
        {
            Timer += Time.deltaTime;

            if(Timer >= TimerGoal && ZombiesAlive < maxZombiesAlive)
            {
                StartCoroutine(SpawnNewZombie());
                Timer = 0;
            }
       } 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(transform.position,spawnRange);
    }

    IEnumerator SpawnNewZombie ()
    {
        Vector3 spawnPosition = RamdomizePos();
        Collider[] colliders = Physics.OverlapSphere(spawnPosition,1,EnemiesLayer);

        while(colliders.Length > 0)
        {
            spawnPosition = RamdomizePos();
            colliders = Physics.OverlapSphere(spawnPosition,1,EnemiesLayer);
            yield return null;
        }

        GameObject instance = Instantiate(Enemy, spawnPosition, transform.rotation);
        instance.GetComponent<EnemyControl>().ZombieDied += UpdateZombieCount;
        ZombiesAlive += 1;
    }

    void UpdateZombieCount(GameObject zombie)
    {
        ZombiesAlive -= 1;
        zombie.GetComponent<EnemyControl>().ZombieDied -= UpdateZombieCount;
    }

    Vector3 RamdomizePos()
    {
        Vector3 position = Random.insideUnitSphere * spawnRange;
        position += transform.position;
        position.y = transform.position.y;

        return position;
    }
}
