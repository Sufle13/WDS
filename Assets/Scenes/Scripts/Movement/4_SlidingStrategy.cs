namespace CharacterEngine.Physics.Strategies
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    class SlidingStrategy : Strategy 
    {

        public float Speed = 20;
        public float Acceleration = 50;


        protected override bool IsActive(CharacterPhysicsController character) 
        {
            return character.IsSliding;
        }

        protected override Vector3 GetDesiredVelocity(CharacterPhysicsController character) 
        {
            return GetSlopeDirection( character.Ground ) * Speed;
        }

        protected override float GetAcceleration()
        {
            return Acceleration;
        }


        // Helpers
        private static Vector3 GetSlopeDirection(ControllerColliderHit ground)
        {
            return Vector3.ProjectOnPlane( Vector3.down, ground.normal ); // magnitude == 0 - horizontal ground. magnitude == 1 - vertical wall. 
        }


    }
}