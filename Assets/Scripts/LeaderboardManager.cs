using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Platform;
using Oculus.Platform.Models;
using System;

namespace ZenvaVR
{
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField]private string leaderboardName = "Highest Score";
        private LeaderboardEntry[] leaderboard;

        [SerializeField]private GameObject[] leaderboardContainers;

        public static LeaderboardManager instance;

        void Awake()
        {
            instance = this;
        }

        public void UpdateLeaderboard()
        {
            Leaderboards.GetEntries(leaderboardName, 10, LeaderboardFilterType.None, LeaderboardStartAt.Top).OnComplete(OnGetLeaderboardEntries);

        }

        void OnGetLeaderboardEntries(Message<LeaderboardEntryList> message)
        {
            leaderboard = new LeaderboardEntry[message.Data.Count];

            for(int x = 0; x <leaderboard.Length; x++)
            {
                leaderboard[x] = message.Data[x];
            }

            for(int x = 0; x < leaderboardContainers.Length; x++)
            {
                if(leaderboard.Length > x)
                {
                    leaderboardContainers[x].SetActive(true);

                    leaderboardContainers[x].transform.Find("PlayerNameText").GetComponent<TextMeshProUGUI>().text = leaderboard[x].User.OculusID;
                    leaderboardContainers[x].transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = leaderboard[x].Score.ToString();
                }
                else
                {
                    leaderboardContainers[x].SetActive(false);
                }
            }
        }

        public void WriteLeaderboardEntry (int score)
        {
            Leaderboards.WriteEntry(leaderboardName, (long)score);
        }
    }
}