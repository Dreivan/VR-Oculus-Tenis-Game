using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using TMPro;

namespace ZenvaVR
{
    public class GameManager : MonoBehaviour
    {
        //UI Button and round Timer
        [SerializeField] private GameObject newRoundButton;
        [SerializeField] private TextMeshProUGUI roundTimeText;

        [SerializeField] float roundDuration = 30.0f;
        private float curruntRoundTime;
        private bool roundInProgres;

        public float ballShootFrequency = 1.0f;
        private float lastBallShootTime;

        private BallShooter ballShooter;

        

        private void Awake()
        {
            ballShooter = FindObjectOfType<BallShooter>();
            if(ballShooter == null) { Debug.LogError("BallShooter missing in this scene"); }
        }
        void Update()
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
                BeginNewRound();

            if (roundInProgres)
            {
                curruntRoundTime -= Time.deltaTime;

                roundTimeText.text = Mathf.RoundToInt(curruntRoundTime).ToString();

                //are we going to shoot a ball this frame, thats what we are checking
                if(Time.time - lastBallShootTime >= ballShootFrequency)
                {
                    lastBallShootTime = Time.time;
                    ballShooter.ShootBall();
                }

                if (curruntRoundTime <= 0.0f)
                    EndRound();
            }
        }


        public void BeginNewRound()
        {
            Player.instance.AddScore(-Player.instance.GetCurrScore());
            curruntRoundTime = roundDuration;
            roundInProgres = true;
            newRoundButton.SetActive(false);
        }

        void EndRound()
        {
            roundInProgres = false;
            curruntRoundTime = 0.0f;
            newRoundButton.SetActive(true);

            //set leaderboard
            LeaderboardManager.instance.WriteLeaderboardEntry(Player.instance.GetCurrScore());
        }

    }
}