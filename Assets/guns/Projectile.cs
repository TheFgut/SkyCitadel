using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float Gravity;

    public float gravity { get {  return Gravity; } }
    [SerializeField] private float flySpeed;
    public float flySpd { get { return flySpeed; } }

    [SerializeField] private float lifeTime;

    [SerializeField] private Damage[] damages;

    private Vector3 speed;

    public void ProjectileOrientation(Quaternion rotation)
    {
        speed = math.rotVector(new Vector3(flySpeed, 0, 0), rotation);
        transform.rotation = rotation;
    }

    // Update is called once per frame
    void Update()
    {
        speed += new Vector3(0, -Gravity, 0) * Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }

        CastCollisionDetectingRay(speed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        transform.position += speed * Time.deltaTime;
    }

    private void CastCollisionDetectingRay(Vector3 movement)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement, movement.magnitude);
        if(hit.collider != null && hit.collider.tag == "DamageReciever")
        {
            LifeModule lifeModule = hit.collider.gameObject.GetComponentInParent<LifeModule>();
            lifeModule.ApplyDamage(hit.collider, damages);
            ContactDestroy();
        }
    }


    private void ContactDestroy()
    {
        Destroy(gameObject);
    }

}
