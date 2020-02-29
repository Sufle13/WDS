namespace CharacterEngine.Physics.Strategies 
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class Strategy
    {
        public virtual void Update(CharacterPhysicsController character)
        {
            character.Velocity = Update( character, character.Velocity );
        }

        protected virtual Vector3 Update(CharacterPhysicsController character, Vector3 velocity)
        {
            if (IsActive( character )) 
            {
                var desiredVelocity = GetDesiredVelocity( character );
                return Vector3.MoveTowards( velocity, desiredVelocity, GetAcceleration() * Time.deltaTime );
            }
            return velocity;
        }

        protected virtual bool IsActive(CharacterPhysicsController character) 
        {
            return true;
        }

        protected virtual Vector3 GetDesiredVelocity(CharacterPhysicsController character) 
        {
            return Vector3.zero;
        }

        protected virtual float GetAcceleration()
        {
            return 0;
        }
    }
}