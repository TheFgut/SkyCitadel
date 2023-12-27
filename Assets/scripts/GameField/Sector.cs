using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector
{
    //инициализаторы
    public Sector(int X, int Y)
    {
        posX = X;
        posY = Y;
    }

    private List<SectorBelonging> objectsInSector = new List<SectorBelonging>();

    public SectorBelonging[] objects { get { return objectsInSector.ToArray(); } }

    //параметры
    public int posX;
    public int posY;


    public void RemoveFromSector(int Id)
    {
        objectsInSector.Remove(objectsInSector[Id]);
        for (int i = Id; i < objectsInSector.Count; i++)
        {
            objectsInSector[i].setSectorId(i);
        }
    }

    public void AddObject(SectorBelonging ship)
    {
        ship.setSectorId(objectsInSector.Count);
        objectsInSector.Add(ship);
    }

    public SectorBelonging[] GetObjectsInSectorArr()
    {
        return objectsInSector.ToArray();
    }
}