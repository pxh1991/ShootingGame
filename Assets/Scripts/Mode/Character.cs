using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting.Content
{
    public class Character : MonoBehaviour
    {
        private float hp = 100.0f;
        
        public float Hp {private set;get;}

        private CharacterController m_characterController;
        private CharacterAnimator m_animator;

        protected virtual void Awake() 
        {
            m_characterController = GetComponentInChildren<CharacterController>();
            m_animator = GetComponentInChildren<CharacterAnimator>();
        }

        public virtual void init()
        {

        }

        public virtual void Reset()
        {

        }

        public virtual void OnAttack(float hitPoint)
        {
            hp -= hitPoint;
            m_animator?.OnAttack();
        }

        

        
    }
}

