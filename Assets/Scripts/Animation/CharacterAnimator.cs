using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting.Content
{
    public class CharacterAnimator : MonoBehaviour
    {
        private Animator m_animator;

        private const string idle = "idle";
        private const string attack = "attack";
        private const string moveLeft = "moveReft";
        private const string moveRight = "moveRight";
        private const string hurt = "hurt";
        private const string moveForward = "moveForward";
        private const string moveBack = "moveBack";
        protected const string turnRight = "turnRight";
        protected const string turnLeft = "turnLeft";

        public static int idleCode = Animator.StringToHash(idle);
        public static int attackCode = Animator.StringToHash(attack);
        public static int moveLeftCode = Animator.StringToHash(moveLeft);
        public static int moveRightCode = Animator.StringToHash(moveRight);
        public static int hurtCode = Animator.StringToHash(hurt);
        public static int moveForwardCode = Animator.StringToHash(moveForward);
        public static int moveBackCode = Animator.StringToHash(moveBack);
        public static int turnRightCode = Animator.StringToHash(turnRight);
        public static int turnLeftCode = Animator.StringToHash(turnLeft);

        private void Awake() 
        {
            m_animator = GetComponentInChildren<Animator>();
        }

        

        public virtual void SetAnimator(int target)
        {
            if(m_animator == null)
            {
                Debug.Log($"{transform.name}'s animator is null!");
                return;
            }
            m_animator.SetTrigger(target);
            Debug.Log($"{m_animator.name} change animation state");
        }

        public virtual void OnAttack()
        {
            SetAnimator(hurtCode);
        }
    }

}
