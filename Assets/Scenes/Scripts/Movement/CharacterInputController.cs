namespace CharacterEngine.Physics 
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CharacterInputController : MonoBehaviour 
    {

        private CharacterPhysicsController character;
        private float jumpPressedTime = -100;

        void Awake() 
        {
            character = GetComponent<CharacterPhysicsController>();
        }

        void Update()
        {
            character.InputMoveDirection = GetMoveDirection();
            character.IsInputAcceleration = IsAccelerationPressed();
            character.IsInputJump = IsJumpPressed();
            character.IsInputCrouch = IsCrouchPressed();
        }

        private Vector3 GetMoveDirection() 
        {
            var h = Input.GetAxis( "Horizontal" );
            var v = Input.GetAxis( "Vertical" );
            var dir = Vector3.ClampMagnitude( new Vector3( h, 0, v ), 1 );

            return transform.TransformDirection( dir ); // transform to character space
        }

        private static bool IsAccelerationPressed() 
        {
            return Input.GetKey( KeyCode.LeftShift );
        }

        private static bool IsCrouchPressed()
        {
            return Input.GetKey( KeyCode.LeftControl );
        }

        private bool IsJumpPressed()
        {
            if (Input.GetButtonDown( "Jump" )) jumpPressedTime = Time.time;
            if (!Input.GetButton( "Jump" )) jumpPressedTime = -100;

            return Time.time - jumpPressedTime <= 0.2f; // button was pressed in last 0.2 secounds
        }
    }
}