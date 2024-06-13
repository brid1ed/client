using System.Collections;
using System.Collections.Generic;
using Entity;
using Player;
using UnityEngine;

namespace Entity {
    public enum PlayerDirection {
        Back = -1,
        Front = 0,
        Left = 1,
        Right = 2,
    }
    public class PlayerController : BaseEntity {
        [Tooltip("인풋 시스템")]
        public PlayerInput input;
        [Header("렌더링 정보")]
        SpriteRenderer render;
        [Tooltip("캐릭터 보는 방향")]
        public PlayerDirection direction = PlayerDirection.Front;
        
        void Start() {
            render = GetComponent<SpriteRenderer>();
            input = new();
        }


        void Update()
        {
            Movement();
        }


        public void Movement()
        {
            Vector2 moveVec = new Vector2(input.GetAxisHorizontal(), input.GetAxisVertical()).normalized;

            if (moveVec[0] > 0)
                direction = PlayerDirection.Right;
            else if (moveVec[0] < 0)
                direction = PlayerDirection.Left;

            if (moveVec[1] > 0)
                direction = PlayerDirection.Back;
            else if (moveVec[1] < 0)
                direction = PlayerDirection.Front;

            if (moveVec[0] != 0 || moveVec[1] != 0) {
                Move(moveVec * speed * Time.deltaTime);
            }
        }

        public void Move(Vector2 moveVec) {
            transform.Translate(moveVec);
        }

        public void TryRoll()
        {

        }




    }
}