using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class GameField : MonoBehaviour
{
    public static Map map { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        map = GenEmptyMap(10, 10, 10);
    }

    public Map GenEmptyMap(int sizeX, int SizeY,float sectorSize = 30)
    {    
        Sector[,] field = new Sector[sizeX, SizeY];
        for (int X = 0; X < sizeX; X++)
        {
            for (int Y = 0; Y < SizeY; Y++)
            {
                field[X, Y] = new Sector(sizeX,SizeY);
            }
        }

        Map map = new Map(field, sectorSize);

        return map;
    }


    public class Map
    {
        public Map(Sector[,] newField, float sectSize)
        {
            sectorSize = sectSize;
            field = newField;
            XSize = field.GetLength(0);
            maxXDist = sectorSize * XSize;
            YSize = field.GetLength(1);
            maxYDist = sectorSize * YSize;
            objFinder = new ObjectFinder(this);
        }

        public ObjectFinder objFinder { get; private set; }

        protected Sector[,] field;

        public int XSize { get; private set; }
        public int YSize { get; private set; }
    

        protected float sectorSize = 30;
        public LineRenderer sectorLine;

        private float maxXDist;
        private float maxYDist;

        public int[] initializeObjectAndReturnItsCoords(SectorBelonging Object)
        {
            int[] coords = getCoordsByPos(Object.transform.position);
            field[coords[0], coords[1]].AddObject(Object);
            return coords;
        }

        public void RemoveObject(SectorBelonging Object)
        {
            int[] cords = Object.coordinates;
            int id = Object.sectorId;
            field[cords[0], cords[1]].RemoveFromSector(id);
        }

        public int[] updateSectorCoordinates(SectorBelonging sectorB)
        {
            Vector3 pos = sectorB.transform.position;
            int X = 0;
            int Y = 0;
            if (0 <= pos.x && pos.x <= maxXDist)
            {
                X = (int)(pos.x / sectorSize);
            }
            if (0 <= pos.y && pos.y <= maxYDist)
            {
                Y = (int)(pos.y / sectorSize);
            }

            int[] oldCoord = new int[2] { sectorB.coordinates[0], sectorB.coordinates[1] };
            if (X != oldCoord[0] || Y != oldCoord[1])
            {
                field[oldCoord[0], oldCoord[1]].RemoveFromSector(sectorB.sectorId);
                field[X, Y].AddObject(sectorB);
            }
            return new int[2] {X, Y};
        }

        protected int[] getCoordsByPos(Vector3 pos)
        {
            int X = 0;
            int Y = 0;
            if (0 <= pos.x && pos.x <= maxXDist)
            {
                X = (int)(pos.x / sectorSize);
            }
            if (0 <= pos.y && pos.y <= maxYDist)
            {
                Y = (int)(pos.y / sectorSize);
            }
            return new int[] {X, Y};
        }

        void GenSectorsGraphics()
        {
            sectorLine = new GameObject().AddComponent<LineRenderer>();
            int XLenght = field.GetLength(0);
            int YLenght = field.GetLength(1);
            int Ycorrector = (XLenght * 3 + 1);
            Vector3[] Lines = new Vector3[Ycorrector + YLenght * 3 + 1];
            for (int i = 0; i < XLenght;i++)
            {
                int sectC = i * 3;
                Lines[sectC] = new Vector3(i * sectorSize, 0);
                Lines[sectC + 1] = new Vector3(i * sectorSize, YLenght * sectorSize);
                Lines[sectC + 2] = new Vector3((i + 1) * sectorSize, YLenght * sectorSize);
            }
            
            Ycorrector -= 1;
            for (int i = 0; i < YLenght; i++)
            {
                int sectC = Ycorrector + i * 3;
                
                Lines[sectC] = new Vector3(XLenght * sectorSize, i * sectorSize);
                Lines[sectC + 1] = new Vector3(0, i * sectorSize);
                Lines[sectC + 2] = new Vector3(0, (i + 1) * sectorSize);
            }

            sectorLine.widthMultiplier = 0.1f;
            sectorLine.positionCount = Lines.Length;
            sectorLine.SetPositions(Lines);
        }


        public class ObjectFinder
        {
            private Map map;
            public ObjectFinder(Map map)
            {
                this.map = map;
               
            }

            public SectorBelonging[] getObjectsAroundPoint(Vector3 position, float radius)
            {
                LinkedList<SectorBelonging> listOfObjects = new LinkedList<SectorBelonging>();

                //сделать оптимизацию добавления всех объектов в сектoрах которые полностью попадают в радиус
                int[] objCoords = map.getCoordsByPos(position);
                int sectorSearchRadius = (int)Mathf.Ceil( ((radius)/ map.sectorSize) + 0.5f);

                int minX = objCoords[0] - sectorSearchRadius;
                minX = minX < 0 ? 0 : minX;
                int maxX = objCoords[0] + sectorSearchRadius;
                maxX = maxX >= map.XSize ? map.XSize : maxX;
                int minY = objCoords[1] - sectorSearchRadius;
                minY = minY < 0 ? 0 : minY;
                int maxY = objCoords[1] + sectorSearchRadius;
                maxY = maxY >= map.YSize ? map.YSize : maxY;

                for (int X = minX; X < maxX; X ++)
                {
                    for (int Y = minY; Y < maxY; Y++)
                    {
                        SectorBelonging[] content = map.field[X, Y].GetObjectsInSectorArr();
                        if(content.Length != 0)
                        {
                            listOfObjects.AddRange(content);
                        }
                    }
                }

                return listOfObjects.ToArray();
            }


            public SectorBelonging[] getObjectsOfTeamAroundPoint(Vector3 position, float radius, int[] searchedTeamIDs)
            {
                List<SectorBelonging> listOfEnemies = new List<SectorBelonging>();
                SectorBelonging[] allObjects = getObjectsAroundPoint(position, radius);
                foreach(SectorBelonging obj in allObjects)
                {   
                    if (obj.teamBelonging != null && searchedTeamIDs.Contains(obj.teamBelonging.team))
                    {
                        listOfEnemies.Add(obj);
                    }
                }
                return listOfEnemies.ToArray();
            }


        }
    }



}
