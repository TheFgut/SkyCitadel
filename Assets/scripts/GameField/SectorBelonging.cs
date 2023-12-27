using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorBelonging
{
    public int sectorId { get; private set; }

    public void setSectorId(int id)
    {
        sectorId = id;
    }

    public Transform transform {  get; private set; }

    public void Init(Transform transform)
    {
        this.transform = transform;
        coordinates = GameField.map.initializeObjectAndReturnItsCoords(this);
    }


    public int[] coordinates { get; private set; } = new int[2] { 0, 0 };

    public void updateSectorByPos()
    {
        coordinates = GameField.map.updateSectorCoordinates(this);
    }




    public TeamBelonging teamBelonging { get; private set; }

    public void setTeamBelonging(TeamBelonging teamBelonging)
    {
        this.teamBelonging = teamBelonging;
    }

    public IAtackTarget attackTarget { get; private set; }

    public void setIAtackTarget(IAtackTarget attackTarget)
    {
        this.attackTarget = attackTarget;
    }

    public void RemoveFromWeb()
    {
        GameField.map.RemoveObject(this);
    }
}