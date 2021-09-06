using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void OnAppStart()
    {
        DatabaseHandler.GetUsers(users =>
        {
            foreach (var user in users)
            {
                Debug.Log($"{user.Value.Name} {user.Value.Score} {user.Value.Date}");
            }
        });
    }
}
