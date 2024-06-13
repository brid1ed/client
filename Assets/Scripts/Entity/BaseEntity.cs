using System;
using UnityEngine;

namespace Entity
{
    public class BaseEntity: MonoBehaviour {
        [Header("엔티티 수치")]
        [Tooltip("체력")]
        [SerializeField]
        protected float hp = 100f;
        [Tooltip("이동속도")]
        [SerializeField]
        protected float speed = 1f;
        
        public virtual void Init() {
            
        }
        
        
        
        
    }
}