using Manager.DesignPattern;
using UnityEngine;

namespace Manager
{
    public class BaseManager : MonoBehaviour
    {

        public virtual bool Init() {

            return false;
        }
        
        
    }
}