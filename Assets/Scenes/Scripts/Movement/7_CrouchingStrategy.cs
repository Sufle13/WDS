namespace CharacterEngine.Physics.Strategies 
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    class CrouchingStrategy : Strategy 
    {

        public Vector3 CrouchScale = new Vector3( 1, 0.5f, 1 );


        public override void Update(CharacterPhysicsController character)
        {
            var transform = character.transform;
            var desiredScale = GetDesiredScale( character );
            transform.localScale = Vector3.MoveTowards( transform.localScale, desiredScale, 5 * Time.deltaTime );
        }


        private Vector3 GetDesiredScale(CharacterPhysicsController character)
        {
            var targetScale = GetCrouchScale( character );

            if (!character.IsInputCrouch && character.IsCrouching)
            {
                if (CheckCollision( character.transform, character.Capsule )) targetScale = CrouchScale; // character can not get up
            }

            return targetScale;
        }


        // Helpers
        private Vector3 GetCrouchScale(CharacterPhysicsController character) 
        {
            if (character.IsInputCrouch) return CrouchScale;
            return Vector3.one;
        }

        internal static bool IsCrouching(Transform transform) 
        {
            return transform.localScale.y != 1f;
        }

        private static bool CheckCollision(Transform transform, CharacterController capsule)
        {
            var oldScale = transform.localScale;
            transform.localScale = Vector3.one;
            var hasCollision = CheckCollision_( transform, capsule );
            transform.localScale = oldScale;
            return hasCollision;
        }

        private static bool CheckCollision_(Transform transform, CharacterController capsule)
        {
            var sphere1 = transform.TransformPoint( capsule.center - Vector3.up * (capsule.height / 2f - capsule.radius) );
            var sphere2 = transform.TransformPoint( capsule.center + Vector3.up * (capsule.height / 2f - capsule.radius) );

            capsule.enabled = false;
            var hasCollision = Physics.CheckCapsule( sphere1, sphere2, capsule.radius * 0.99f );
            capsule.enabled = true;
            return hasCollision;
        }


    }
}