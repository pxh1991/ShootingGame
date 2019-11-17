using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting.Content
{
    public class Character : MonoBehaviour
    {
        private float hp = 100.0f;
        
        public float Hp {private set;get;}

        public float hit = 5f;

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

        public virtual void OnTriggerEnter(Collider other) 
        {
            if(other == null)
                return;
            var colliderGameobject = other.gameObject;
            if(gameObject.layer == 8)
            {
                if(colliderGameobject.layer == 8)
                {
                    m_animator.SetAnimator(CharacterAnimator.moveLeftCode);
                }
            }
            if(gameObject.layer == 9)
            {
                if(colliderGameobject.layer == 8)
                {
                    var character = other.GetComponentInParent<Character>();
                    if(character == null)
                        return;
                    OnAttack(character.hit);
                }
            }
        }

        public virtual void OnTriggerExit(Collider other) 
        {
            
        }

       
        public virtual void OnTriggerStay(Collider other)
        {
            
        }
    }
}

