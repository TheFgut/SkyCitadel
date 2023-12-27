using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Ship : MonoBehaviour
{


    [System.Serializable]
    public class MoveModule
    {
        private Ship ship;
        private Transform transform;

        //movement
        [SerializeField] private float moveSpeed;


        public Vector3 innertion { get; private set; }
        private float inertPower;

        public GameField mapScr {  get; private set; }

        public void Init(Ship ship)
        {
            mapScr = GameObject.Find("Map").GetComponent<GameField>();
            this.ship = ship;
            transform = ship.transform;
        }


        public event shipMovedDeleagte onShipMove;
        public event posChangedDelegate onPosChanged;

        private Coroutine movingProcess;
        public void MoveTo(Vector3 target, reachedTargetDelegate callBack)
        {
            //Vector3 mVect = new Vector3(0, -9.8f, 0);
            //float dist = mVect.magnitude;
            //inertPower = Move(mVect, target, dist);

            if (movingProcess != null)
            {
                ship.StopCoroutine(movingProcess);
            }
            movingProcess = ship.StartCoroutine(movingToTarget(target, callBack));
        }


        private IEnumerator movingToTarget(Vector3 targetPos, reachedTargetDelegate raeched)
        {
            yield return null;

            do
            {
                Vector3 mVect = targetPos - transform.position;
                float dist = mVect.magnitude;
                mVect = mVect.normalized;

                inertPower = Move(mVect, targetPos, dist);
                onShipMove?.Invoke(mVect);

            } while (inertPower != 0);


            movingProcess = null;
        }

        private float Move(Vector3 mVect,Vector3 targetPos,  float dist)
        {

            //tormozhenie
            float tormoznoiPut = innertion.magnitude;
            if (tormoznoiPut + dist < 0.2f)
            {
                innertion = new Vector3();
                return 0;
            }
            if (dist > tormoznoiPut + 0.1f)
            {
                //Vector3 avoidV = GameField.map.CalculateAvoidVector(transform.position, ship, targetPos);
                //if (avoidV.magnitude != 0)
                //{
                //    innertion += avoidV * moveSpeed * Time.deltaTime;//avoiding obstacle
                //}
                //else
                //{
                //    innertion += mVect * moveSpeed * Time.deltaTime;//yskorenie
                //}
            }
            innertion -= innertion * Time.deltaTime;

            transform.position += innertion * Time.deltaTime;
            onPosChanged?.Invoke();
            return tormoznoiPut;
        }

        public delegate void shipMovedDeleagte(Vector3 movement);
        public delegate void posChangedDelegate();
    }




}


public delegate void reachedTargetDelegate(bool success);
