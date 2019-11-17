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

        public static int idleCode = idle.GetHashCode();
        public static int attackCode = attack.GetHashCode();
        public static int moveLeftCode = moveLeft.GetHashCode();
        public static int moveRightCode = moveRight.GetHashCode();
        public static int hurtCode = hurt.GetHashCode();
        public static int moveForwardCode = moveForward.GetHashCode();
        public static int moveBackCode = moveBack.GetHashCode();
        public static int turnRightCode = turnRight.GetHashCode();
        public static int turnLeftCode = turnLeft.GetHashCode();

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
        }

        public virtual void OnAttack()
        {
            SetAnimator(hurtCode);
        }
    }

}
