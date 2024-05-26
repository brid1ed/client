using System;
using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace Manager
{
    public class EntityManager : BaseManager {

        public List<BaseEntity> entities;

        public override bool Init() {
            entities = new List<BaseEntity>();
            
            // error check
            if (entities == null) return true;
            
            return false;
        }
        
        
        
        
        #region entity 추가, 삭제

        public void AddEntity(BaseEntity entity) { entities.Add(entity); }

        public GameObject AddObject(string name, GameObject parent = null) {
            GameObject game_object = new GameObject(name);
            if (parent != null) game_object.transform.parent = parent.transform;
            return Instantiate(game_object);
            // BaseEntity entity = new BaseEntity();
        }
        

        #endregion
        
        
    }
}