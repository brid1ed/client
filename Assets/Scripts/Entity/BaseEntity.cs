using System;
using UnityEngine;

namespace Entity
{
    public class BaseEntity: MonoBehaviour {
        [SerializeField] private float hp = 100f;
        [SerializeField] private float speed = 1f;
        
        public virtual void Init() {
            
        }
        
        
        
        
    }
}