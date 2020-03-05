using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZenvaVR
{
    public class Target : MonoBehaviour
    {
        [SerializeField]private int scoreToGive;
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Ball"))
            {
                Player.instance.AddScore(scoreToGive);
                Destroy(collision.gameObject, 1.0f);
            }
        }
    }
}
