using UnityEngine;
using System.Collections;
using System;

namespace RealmGames.Leaderboard
{
    public class Manager<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
    }
}