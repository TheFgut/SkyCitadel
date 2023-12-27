using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Ship : MonoBehaviour, IAtackTarget
{
    


    public float shipRadius;

    [Header("Movement")]
    [SerializeField] private MoveModule movement;
    [SerializeField] private MoveAnimation moveAnimations;
    private LifeModule lifeModule;

    [Header("DeepSettings")]
    [SerializeField] private TeamBelonging teamBelonging;
    private SectorBelonging sectorBelonging = new SectorBelonging();

    void Start()
    {
        movement.Init(this);
        moveAnimations.Init(this);
        sectorBelonging.Init(transform);
        sectorBelonging.setTeamBelonging(teamBelonging);

        sectorBelonging.setIAtackTarget(this);

        lifeModule = GetComponent<LifeModule>();
        lifeModule.onDestroyModule += Die;
    }


    private void SubsribeAllAliveThings()
    {
        movement.onShipMove += moveAnimations.AnimateByMovement;
        movement.onPosChanged += sectorBelonging.updateSectorByPos;


    }

    public bool isAlive { get; private set; } = true;

    public Vector3 position => transform.position;

    private void Die()
    {
        isAlive = false;
        sectorBelonging.RemoveFromWeb();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {

        }
        else
        {

        }

    }






}
