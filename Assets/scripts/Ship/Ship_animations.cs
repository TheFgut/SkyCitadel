using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Ship : MonoBehaviour
{

    [System.Serializable]
    private class MoveAnimation
    {

        //move anims
        [SerializeField] private float rotationSpeed;

        private bool leftLoock;
        public bool isRotating { get; private set; }

        private Ship ship;
        private Transform transform;
        public void Init(Ship ship)
        {
            this.ship = ship;
            transform = ship.transform;
        }

        private Coroutine rotAnimRoutine;
        private Quaternion[] rotData = new Quaternion[2] { Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 180, 0) };
        public void AnimateByMovement(Vector3 movement)
        {
            float horizontalAxis = movement.x;
            if (rotAnimRoutine != null)
            {
                ship.StopCoroutine(rotAnimRoutine);
            }
            //вращает корабль влево/вправо в зависимости от направления полета
            if (horizontalAxis < 0 && leftLoock == false)
            {
                ship.StartCoroutine(rotateAnim(transform.rotation, rotData[1]));
                leftLoock = true;
            }
            else if (horizontalAxis > 0 && leftLoock == true)
            {
                ship.StartCoroutine(rotateAnim(transform.rotation, rotData[0]));
                leftLoock = false;
            }
        }


        private IEnumerator rotateAnim(Quaternion startRot, Quaternion endRot)
        {
            isRotating = true;

            float duration = Mathf.Abs(startRot.eulerAngles.y - endRot.eulerAngles.y) / rotationSpeed;
            float coef = 0;

            do
            {
                transform.rotation = Quaternion.Lerp(startRot, endRot, coef);
                coef += Time.deltaTime / duration;
                yield return new WaitForEndOfFrame();
            } while (coef < 1);

            isRotating = false;
        }
    }
}
