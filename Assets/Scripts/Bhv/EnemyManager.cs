using System.Collections;
using UnityEngine;
using Assets.Scripts.Bhv.NewRealisation;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyBase enemyPrefab;
    [SerializeField] private int enemyBasePreloadCount;
    [SerializeField] private int spawnTimer;
    [SerializeField]  private RouteBase route;

    private ObjectPoolBase<EnemyBase> _pool;

    public EnemyBase Preload() => Instantiate(enemyPrefab);
    public void ReturnAction(EnemyBase enemyBase) => enemyBase.gameObject.SetActive(false);
    public void GetAction(EnemyBase enemyBase) => enemyBase.gameObject.SetActive(true);

    private void Awake()
    {
        //_pool = new ObjectPoolBase<EnemyBase>(Preload, GetAction, ReturnAction, enemyBasePreloadCount);
        _pool = new ObjectPoolBase<EnemyBase>(Preload, new RandomSpawnLogic(route).GetAction, ReturnAction, enemyBasePreloadCount);
    }

    private void Spawn()
    {
        EnemyBase enemy = _pool.Get();
        //enemy.transform.position = route.Waypoints[Random.Range(0, route.Waypoints.Length - 1)];
        enemy.OnDie += SetDead;
        enemy.SetRoute(route);
    }

    private void SetDead(EnemyBase enemy)
    {
        _pool.Return(enemy);
        enemy.OnDie -= SetDead;
        StartCoroutine(SpawnCoroutine(spawnTimer));
    }

    private void Start()
    {
        Spawn();        
    }

    private IEnumerator SpawnCoroutine(float timeInSeconds)
    {
        yield return new WaitForSeconds(timeInSeconds);
        Spawn();
    }
 }

public abstract class SpawnLogicBase
{
    public abstract void GetAction(EnemyBase enemyBase);
}

public class RandomSpawnLogic : SpawnLogicBase
{
    private RouteBase route;

    public RandomSpawnLogic(RouteBase route)
    {
        this.route = route;
    }

    public override void GetAction(EnemyBase enemyBase)
    {
        enemyBase.transform.position = route.Waypoints[Random.Range(0, route.Waypoints.Length - 1)];
        enemyBase.gameObject.SetActive(true);
    }
}