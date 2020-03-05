using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using System;
using TMPro;

namespace ZenvaVR
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        private int score;        
        public static Player instance;

        void Awake()
        {
            instance = this;   
        }
        void Start()
        {
            //check if the game is pirated by the user if so close it
            Core.AsyncInitialize();
            Entitlements.IsUserEntitledToApplication().OnComplete(OnUserIsEntitledToApplication);  
        }

        //if server detects Error the app will close
        void OnUserIsEntitledToApplication(Message message)
        {
            if(message.IsError)
            {
                UnityEngine.Application.Quit();
            }
        }


        public void AddScore (int amount)
        {
            score += amount;
            scoreText.text = score.ToString();
        }

        public int GetCurrScore()
        {
            return score;
        }
    }
}

