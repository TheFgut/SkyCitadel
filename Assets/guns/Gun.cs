using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Gun
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private float reloadingTime;

    private MonoBehaviour turret;
    public void Init(MonoBehaviour turret)
    {
        this.turret = turret;
    }



    public void Fire(Projectile projectilePrefab)
    {
        if (isReloading) return;

        ReleaseProjectile(projectilePrefab);

        Reload();
    }

    public void ReleaseProjectile(Projectile projectilePrefab)
    {
        GameObject project = Object.Instantiate(projectilePrefab.gameObject);
        project.transform.position = muzzle.transform.position;
        Projectile p = project.GetComponent<Projectile>();
        p.ProjectileOrientation(muzzle.transform.rotation);
    }

    private Coroutine currentRoutine;

    public bool isReloading { get; private set; }

    public void Reload()
    {
        isReloading = true;
        if(currentRoutine != null)
        {
            turret.StopCoroutine(currentRoutine);
            currentRoutine = null;
        }
        currentRoutine = turret.StartCoroutine(reloadingRoutine());
    }

    private IEnumerator reloadingRoutine()
    {
        float timeToReload = reloadingTime;
        do
        {
            yield return new WaitForEndOfFrame();
            timeToReload -= Time.deltaTime;
        } while (timeToReload > 0);
        isReloading = false; 
        currentRoutine = null;
        yield break;
    }
}
