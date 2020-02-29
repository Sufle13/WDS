namespace CharacterEngine.Physics 
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using CharacterEngine.Physics.Strategies;

    [AddComponentMenu( "Character/Physics/CharacterPhysicsController" )]
    [RequireComponent( typeof( CharacterController ) )]
    public class CharacterPhysicsController : MonoBehaviour 
    {

        [SerializeField] private bool useFixedUpdate;
        [SerializeField] private GroundMovementStrategy groundMovementStrategy;
        [SerializeField] private SlopeMovementStrategy slopeMovementStrategy;
        [SerializeField] private AirMovementStrategy airMovementStrategy;
        [SerializeField] private GravityStrategy gravityStrategy;
        [SerializeField] private SlidingStrategy slidingStrategy;
        [SerializeField] private JumpingStrategy jumpingStrategy;
        [SerializeField] private CrouchingStrategy crouchingStrategy;

        public CharacterController Capsule { get; private set; }
        public Vector3 Velocity { get; internal set; }
        internal float Gravity => gravityStrategy.Gravity;
        public List<ControllerColliderHit> Hits { get; } = new List<ControllerColliderHit>();
        public ControllerColliderHit Ground { get; private set; }
        public Vector3 LookDirection => transform.forward;

        public bool IsGrounded => Ground != null;
        public bool IsUngrounded => !IsGrounded;
        public bool IsMoving => IsGrounded && IsInputMoving;
        public bool IsWalking => IsGrounded && IsInputWalking;
        public bool IsRunning => IsGrounded && IsInputRunning;
        public bool IsSliding => IsGrounded && IsSlope( Capsule, Ground );
        public bool IsJumping => jumpingStrategy.IsJumping;
        public bool IsCrouching => CrouchingStrategy.IsCrouching( transform );

        public bool IsGroundedFirstFrame { get; private set; }
        public bool IsUngroundedFirstFrame { get; private set; }

        public Vector3 LastGroundedPosition { get; private set; }
        public Vector3 UpperUngroundedPosition { get; private set; }

        public Vector3 UngroundedDelta => transform.position - LastGroundedPosition;
        public Vector3 FallDelta => transform.position - UpperUngroundedPosition;
        public float FallHeight => Mathf.Abs( transform.position.y - UpperUngroundedPosition.y );

        public Vector3 InputMoveDirection { get; set; }
        public bool IsInputAcceleration { get; set; }
        public bool IsInputJump { get; set; }
        public bool IsInputExtraJump { get; set; }
        public bool IsInputCrouch { get; set; }
        public bool IsInputMoving => InputMoveDirection != Vector3.zero;
        public bool IsInputWalking => InputMoveDirection != Vector3.zero && !IsInputAcceleration;
        public bool IsInputRunning => InputMoveDirection != Vector3.zero && IsInputAcceleration;


        private void Reset()
        {
            groundMovementStrategy = new GroundMovementStrategy();
            slopeMovementStrategy = new SlopeMovementStrategy();
            airMovementStrategy = new AirMovementStrategy();
            gravityStrategy = new GravityStrategy();
            slidingStrategy = new SlidingStrategy();
            jumpingStrategy = new JumpingStrategy();
            crouchingStrategy = new CrouchingStrategy();
        }

        void Awake()
        {
            Capsule = GetComponent<CharacterController>();
        }
        void OnEnable() 
        {
            Velocity = Capsule.velocity;
        }

        void Update()
        {
            if (!useFixedUpdate) Update_();
        }
        void FixedUpdate()
        {
            if (useFixedUpdate) Update_();
        }


        private void Update_()
        {
            groundMovementStrategy.Update( this );
            slopeMovementStrategy.Update( this );
            airMovementStrategy.Update( this );
            gravityStrategy.Update( this );
            slidingStrategy.Update( this );
            jumpingStrategy.Update( this );
            crouchingStrategy.Update( this );


            var isPrevGrounded = IsGrounded;
            Move();

            Ground = null;
            if (CanStandOnGround()) 
            {
                Ground = GetGround( Capsule, Hits );
            }

            IsGroundedFirstFrame = IsGrounded && !isPrevGrounded;
            IsUngroundedFirstFrame = IsUngrounded && isPrevGrounded;

            if (IsGrounded) LastGroundedPosition = transform.position;
            if (IsUngroundedFirstFrame) UpperUngroundedPosition = transform.position;
            if (IsUngrounded) UpperUngroundedPosition = MaxY( UpperUngroundedPosition, transform.position );


            Debug.DrawRay( transform.position, Velocity, Color.blue );
            if (IsGrounded) Debug.DrawRay( Ground.point, Ground.normal, Color.red );
        }


        private void Move() 
        {
            var isPrevGrounded = IsGrounded;
            Hits.Clear();
            Capsule.Move( Velocity * Time.deltaTime );
            Velocity = Capsule.velocity;
            PreventUnwantedPushingUp();
        }

        void OnControllerColliderHit(ControllerColliderHit hit) 
        {
            Debug.DrawRay( hit.point, hit.normal, Color.green );
            Hits.Add( hit );
        }

        // Helpers
        private bool CanStandOnGround() 
        {
            // for case when we jump on steep slope or wall
            if (IsJumping && Velocity.y > 0) return false; // we can not stand on ground when jumping up
            return true;
        }

        private void PreventUnwantedPushingUp() //предотвращение подталкивания
        {
            if (IsGrounded && !IsJumping) 
            Velocity = MinY( Velocity, 0 );
        }

        private static ControllerColliderHit GetGround(CharacterController capsule, IEnumerable<ControllerColliderHit> hits) 
        {
            ControllerColliderHit result = null;
            foreach (var hit in hits.Where( i => IsGround( capsule, i ) ))
            {
                result = GetGround( result, hit );
            }
            return result;
        }
        private static bool IsGround(CharacterController capsule, ControllerColliderHit hit) 
        {
            return hit.normal.y > float.Epsilon && IsBelowPart( capsule, hit.point.y );
        }
        private static bool IsSlope(CharacterController capsule, ControllerColliderHit ground) 
        {
            return ground.normal.y <= Mathf.Cos( capsule.slopeLimit * Mathf.Deg2Rad );
        }
        private static ControllerColliderHit GetGround(ControllerColliderHit old, ControllerColliderHit @new) 
        {
            if (old == null) return @new;
            if (@new.normal.y > old.normal.y) return @new;
            return old;
        }

        internal static bool IsBelowPart(CharacterController capsule, float y)
        {
            var bottomSphereCenter = capsule.transform.position.y + capsule.center.y - capsule.height / 2f + capsule.radius;
            return y < bottomSphereCenter;
        }

        private static Vector3 MinY(Vector3 v, float y)
        {
            v.y = Mathf.Min( v.y, y );
            return v;
        }

        private static Vector3 MaxY(Vector3 a, Vector3 b) 
        {
            return a.y > b.y ? a : b;
        }
    }
}