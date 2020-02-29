namespace CharacterEngine.Physics.Strategies 
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [System.Serializable]
    class GroundMovementStrategy : Strategy
    {

        public float CrouchSpeed = 3;
        public float WalkSpeed = 6;
        public float RunSpeed = 9;
        public float Acceleration = 20;


        protected override bool IsActive(CharacterPhysicsController character)
        {
            return character.IsGrounded && !character.IsSliding;
        }

        protected override Vector3 GetDesiredVelocity(CharacterPhysicsController character)
        {
            var speed = GetSpeed( character );
            speed *= GetDirectionSpeedFactor( character.InputMoveDirection, character.LookDirection );
            speed *= GetGroundSpeedFactor( character.Capsule );
            speed *= GetFrictionSpeedFactor( character.Capsule, character.Hits, character.InputMoveDirection );

            var velocity = character.InputMoveDirection * speed;
            return AdjustVelocityToGround( character.Ground, velocity );
        }

        protected override float GetAcceleration()
        {
            return Acceleration;
        }


        // Helpers/Speed
        private float GetSpeed(CharacterPhysicsController character) 
        {
            if (character.IsInputCrouch) return CrouchSpeed;
            if (character.IsInputAcceleration) return RunSpeed;
            return WalkSpeed;
        }

        internal static float GetDirectionSpeedFactor(Vector3 moveDirection, Vector3 lookDirection)
        {
            // when character is moving sideways or backwards then speed is slower
            var dot = Vector3.Dot( lookDirection, moveDirection.normalized ); // -1 - moving backwards, 0 - moving sideways, 1 - moving forwards.
            var t = Mathf.InverseLerp( -1, 1, dot ); // from [-1, 1] to [0, 1]
            return Mathf.Lerp( 0.8f, 1, t ); // [0.8, 1] backward, sideways, forward
        }

        internal static float GetGroundSpeedFactor(CharacterController capsule) {
            // when character is moving up then speed is slower
            // when character is moving down then speed is faster
            var angle = Mathf.Asin( capsule.velocity.normalized.y ) * Mathf.Rad2Deg; // angle > 0 - moving up, angle < 0 - moving down.
            var t = Mathf.InverseLerp( -90, 90, angle ); // [-90, 90] to [0, 1]
            return Mathf.Lerp( 2, 0, t ); // [0, 1] to [2, 0]
        }

        private static float GetFrictionSpeedFactor(CharacterController capsule, IEnumerable<ControllerColliderHit> hits, Vector3 moveDirection)
        {
            var result = 1f;
            foreach (var item in hits.Where( i => HasFriction( capsule, i ) ))
            {
                result = Mathf.Min( result, GetFriction( item, moveDirection ) );
            }
            return result * result;
        }
        private static bool HasFriction(CharacterController capsule, ControllerColliderHit hit)
        {
            return !CharacterPhysicsController.IsBelowPart( capsule, hit.point.y );
        }
        private static float GetFriction(ControllerColliderHit hit, Vector3 moveDirection) 
        {
            // 0 - max friction, character is going in wall
            // 1 - min friction
            return 1 - Vector3.Dot( moveDirection.normalized, -GetXZ( hit.normal ).normalized );
        }


        // Helpers/Velocity
        internal static Vector3 AdjustVelocityToGround(ControllerColliderHit ground, Vector3 velocity) 
        {
            // adjust velocity direction along ground
            // it prevents detachment from the ground when moving down
            return Vector3.ProjectOnPlane( velocity, ground.normal ).normalized * velocity.magnitude;
        }


        // Helpers/Misc
        private static Vector3 GetXZ(Vector3 v)
        {
            return new Vector3( v.x, 0, v.z );
        }


    }
}