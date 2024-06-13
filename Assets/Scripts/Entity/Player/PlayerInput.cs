using System;
using System.Collections.Generic;
using System.IO;
using Manager.DesignPattern;
using UnityEngine;

namespace Player {
    public enum InputPriority {
        None = 0,
        Horizontal = 1,
        Vertical = 2,
    }
    [Serializable]
    public class PlayerInput {
        [Tooltip("현재 입력 우선 순위")]
        [SerializeField]
        InputPriority priority = InputPriority.None;
        Dictionary<string, KeyCode> keyBinding = new(){
            { "up", KeyCode.W },
            { "down", KeyCode.S },
            { "right", KeyCode.D },
            { "left", KeyCode.A },
            { "jump", KeyCode.Space },
        };
        [Tooltip("대각선 이동 허용 여부")]
        [SerializeField]
        bool diagonal; //대각선 이동

        public void SetBinding(string where, KeyCode key) {
            keyBinding[where] = key;
        }

        public void SetBindings(Dictionary<string, KeyCode> bindings) {
            keyBinding = bindings;
        }
        
        void axisPriority(KeyCode key1, KeyCode key2, InputPriority pr) {
            if (
                Input.GetKeyDown(key1) || 
                Input.GetKeyDown(key2) || 
                priority == InputPriority.None
            ) {
                priority = pr;
            }
        }

        public float GetAxisVertical() {
            float i = GetAxisRaw(keyBinding["down"], keyBinding["up"]);

            axisPriority(keyBinding["down"], keyBinding["up"], InputPriority.Vertical);

            if (priority == InputPriority.Vertical) {
                if (i == 0)
                    priority = InputPriority.None;
            } else {
                if (!diagonal)
                    return 0;
            }

            return i;
        }

        public float GetAxisHorizontal() {
            float i = GetAxisRaw(keyBinding["left"], keyBinding["right"]);

            axisPriority(keyBinding["left"], keyBinding["right"], InputPriority.Horizontal);

            if (priority == InputPriority.Horizontal) {
                if (i == 0)
                    priority = InputPriority.None;
            } else {
                if (!diagonal)
                    return 0;
            }

            return i;
        }

        public static float GetAxisRaw(KeyCode key1, KeyCode key2) {
            float dir = 0f;

            if (Time.timeScale == 0f) return dir;
            if (Input.GetKey(key1))
            {
                dir = -1f;
            }

            if (Input.GetKey(key2))
            {
                dir = 1f;
            }

            return dir;
        }
    }
}