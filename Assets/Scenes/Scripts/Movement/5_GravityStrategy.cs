namespace CharacterEngine.Physics.Strategies 
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    class GravityStrategy : Strategy 
    {

        public float Gravity = 30;
        public float MaxFallSpeed = 20;


        protected override bool IsActive(CharacterPhysicsController character)
        {
            return true;
        }

        protected override Vector3 GetDesiredVelocity(CharacterPhysicsController character) 
        {
            return Vector3.down * MaxFallSpeed;
        }

        protected override float GetAcceleration()
        {
            return Gravity;
        }


    }
}