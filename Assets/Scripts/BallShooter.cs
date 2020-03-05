using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZenvaVR
{
    public class BallShooter : MonoBehaviour
    {
        [SerializeField]private Transform ballSpawnPosition;
        [SerializeField]private GameObject ballPrefab;
        [SerializeField]private float startForce;

        public void ShootBall()
        {
            GameObject ball = Instantiate(ballPrefab, ballSpawnPosition.position, ballSpawnPosition.rotation);
            ball.GetComponent<Rigidbody>().AddForce(ball.transform.up * startForce, ForceMode.Impulse);
            Destroy(ball, 5.0f);
        }
    }
}
