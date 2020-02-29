namespace CharacterEngine.Physics.Strategies
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    class SlopeMovementStrategy : Strategy 
    {

        public float CrouchSpeed = 3;
        public float WalkSpeed = 6;
        public float RunSpeed = 9;
        public float Acceleration = 20;


        protected override bool IsActive(CharacterPhysicsController character) 
        {
            return character.IsSliding;
        }

        protected override Vector3 GetDesiredVelocity(CharacterPhysicsController character) {
            var speed = GetSpeed( character );
            speed *= GroundMovementStrategy.GetDirectionSpeedFactor( character.InputMoveDirection, character.LookDirection );
            speed *= GroundMovementStrategy.GetGroundSpeedFactor( character.Capsule );

            var velocity = character.InputMoveDirection * speed;
            return GroundMovementStrategy.AdjustVelocityToGround( character.Ground, velocity );
        }

        protected override float GetAcceleration()
        {
            return Acceleration;
        }


        // Helpers
        private float GetSpeed(CharacterPhysicsController character) 
        {
            if (character.IsInputCrouch) return CrouchSpeed;
            if (character.IsInputAcceleration) return RunSpeed;
            return WalkSpeed;
        }


    }
}