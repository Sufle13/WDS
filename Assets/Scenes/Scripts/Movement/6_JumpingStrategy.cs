namespace CharacterEngine.Physics.Strategies 
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    class JumpingStrategy : Strategy 
    {

        public float BaseJumpHeight = 0.3f;
        public float ExtraJumpHeight = 0.75f;
        [Space]
        public float GroundFactor = 0.1f;
        public float GroundFactorWhenWalking = 0.2f;
        public float GroundFactorWhenRunning = 0.3f;
        [Space]
        public float GroundFactorWhenSliding = 0.6f;
        public float GroundFactorWhenSlidingAndWalking = 0.7f;
        public float GroundFactorWhenSlidingAndRunning = 0.8f;

        private float startJumpTime;

        public bool IsJumping { get; private set; }
        public float JumpingTime => Time.time - startJumpTime;
        public Vector3 JumpDirection { get; private set; }


        protected override Vector3 Update(CharacterPhysicsController character, Vector3 velocity) 
        {
            if (character.IsGrounded && IsJumping)
            {
                IsJumping = false;
            }

            if (character.IsGrounded && character.IsInputJump) 
            {
                startJumpTime = Time.time;
                IsJumping = true;
                JumpDirection = GetJumpDir( character.Ground, GetGroundFactor( character ) );

                velocity.y = 0; // reset gravity
                velocity += JumpDirection * GetJumpForce( character, BaseJumpHeight );
            }

            if (character.IsInputExtraJump && IsExtraJumping( character ))
            {
                velocity += JumpDirection * GetExtraJumpForce( character );
            }

            return velocity;
        }


        // Helpers
        private bool IsExtraJumping(CharacterPhysicsController character)
        {
            return IsJumping && JumpingTime < ExtraJumpHeight / GetJumpForce( character, BaseJumpHeight );
        }

        private float GetGroundFactor(CharacterPhysicsController character)
        {
            // factor == 0 - jump is vertical
            // factor == 1 - jump is in direction of ground normal

            if (!character.IsSliding && !character.IsInputMoving)
                return GroundFactor;
            if (!character.IsSliding && character.IsInputWalking) 
                return GroundFactorWhenWalking;
            if (!character.IsSliding && character.IsInputRunning)
                return GroundFactorWhenRunning;

            if (character.IsSliding && !character.IsInputMoving)
                return GroundFactorWhenSliding;
            if (character.IsSliding && character.IsInputWalking)
                return GroundFactorWhenSlidingAndWalking;
            if (character.IsSliding && character.IsInputRunning)
                return GroundFactorWhenSlidingAndRunning;

            return 0;
        }

        private static Vector3 GetJumpDir(ControllerColliderHit ground, float factor)
        {
            return Vector3.Slerp( Vector3.up, ground.normal, factor );
        }

        private static float GetJumpForce(CharacterPhysicsController character, float jumpHeight) 
        {
            return Mathf.Sqrt( 2 * character.Gravity * jumpHeight );
        }

        private static float GetExtraJumpForce(CharacterPhysicsController character)
        {
            return character.Gravity * Time.deltaTime;
        }


    }
}