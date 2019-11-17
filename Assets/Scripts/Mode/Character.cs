using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class Character : MonoBehaviour
    {
        private float hp = 100.0f;
        
        public float Hp {private set;get;}

        private CharacterController m_characterController;

        protected virtual void Awake() 
        {
            m_characterController = GetComponentInChildren<CharacterController>();
        }

        
    }
}

