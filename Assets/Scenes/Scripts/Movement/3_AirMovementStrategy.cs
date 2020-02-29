namespace CharacterEngine.Physics.Strategies
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    class AirMovementStrategy : Strategy
    {

        public float CrouchSpeed = 3;
        public float WalkSpeed = 6;
        public float RunSpeed = 9;
        public float Acceleration = 10;


        protected override bool IsActive(CharacterPhysicsController character)
        {
            return character.IsUngrounded;
        }

        protected override Vector3 GetDesiredVelocity(CharacterPhysicsController character)
        {
            var speed = GetSpeed( character );
            speed *= GroundMovementStrategy.GetDirectionSpeedFactor( character.InputMoveDirection, character.LookDirection );

            var velocity = character.InputMoveDirection * speed;
            velocity.y = character.Velocity.y;
            return velocity;
        }

        protected override float GetAcceleration() {
            return Acceleration;
        }


        // Helpers
        private float GetSpeed(CharacterPhysicsController character) {
            if (character.IsInputCrouch) return CrouchSpeed;
            if (character.IsInputAcceleration) return RunSpeed;
            return WalkSpeed;
        }


    }
}