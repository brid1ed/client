using System;
using UnityEngine;

namespace Manager
{
    public class GameManager: MonoBehaviour {

        public static void GameQuit() {
#if UNITY_EDITOR
UnityEditor.EditorApplication.isPlaying = false;
UnityEditor.EditorApplication.ExitPlaymode();
#else
Application.Quit();
#endif
        }
    }
}