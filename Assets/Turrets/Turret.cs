using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;

    [SerializeField] private BallisticAimer aiming;
    [SerializeField] private Gun gun;
    [SerializeField] private TeamBelonging team;

    [SerializeField] private float attackRadius;
    [SerializeField] private int[] enemyTeams;
    // Start is called before the first frame update
    void Start()
    {
        aiming.Init(this);
        gun.Init(this);
        StartCoroutine(destroyAllEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private const float timeBetweenTargetsChecks = 0.5f;
    private IEnumerator destroyAllEnemies()
    {
        do
        {
            //searching for targets
            IAtackTarget target = null;
            do
            {
                SectorBelonging[] enemies = GameField.map.objFinder.getObjectsOfTeamAroundPoint(transform.position, attackRadius, enemyTeams);
                if(enemies.Length != 0)
                {
                    target = getNearestAttackableTarget(enemies);

                    float distance = (target.position - transform.position).magnitude;
                    if (distance > attackRadius)
                    {
                        target = null;
                    }
                }
                yield return new WaitForSeconds(timeBetweenTargetsChecks);
            } while (target == null);

            //attack target
            bool isPossible;
            do
            {

                bool aimed = aiming.Aim(target.position, projectilePrefab.flySpd, projectilePrefab.gravity, out isPossible);
                if(isPossible && aimed && !gun.isReloading)
                {
                    gun.Fire(projectilePrefab);
                }
                yield return new WaitForEndOfFrame();
            } while (target.isAlive || !isPossible);


        } while (true);

    }

    private IAtackTarget getNearestAttackableTarget(SectorBelonging[] objects)
    {
        IAtackTarget nearest = null;
        float minDist = Mathf.Infinity;
        foreach (SectorBelonging obj in objects)
        {
            float distance = (obj.transform.position - transform.position).magnitude;
            if (distance < minDist && obj.attackTarget != null)
            {
                minDist = distance; 
                nearest = obj.attackTarget;
            }
        }
        return nearest;
    }
}
