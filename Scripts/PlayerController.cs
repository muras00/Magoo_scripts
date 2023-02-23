using EternalBytes.Player.Controller.Helper;
using UnityEngine;
using System;

namespace EternalBytes.Player.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [Space(5)]
        [SerializeField] private PlayerComponent PlayerComponents;

        [Space(5)]
        [SerializeField] private CameraSetting CameraSettings;

        [Space(5)]
        [SerializeField] private MovementSetting MovementSettings;

        [Space(5)]
        [SerializeField] private OtherSetting OtherSettings;

        [Header("Layermask")]
        [SerializeField] private LayerMask mask;

        public float range;

        

        #region Class
        [Serializable]
        private class PlayerComponent
        {
            [Header("Player")]
            [SerializeField] private Transform _playerObject;

            [Header("Controller")]
            [SerializeField] private CharacterController _playerController;

            [Header("Camera")]
            [SerializeField] private Camera _playerCamera;

            [Header("Ground")]
            [SerializeField] private Transform _groundCheck;

            [Header("Mask")]
            [SerializeField] private LayerMask _groundMask;

            #region Getter & Setter
            public Transform PlayerObject
            {
                get { return _playerObject; }

                set { _playerObject = value; }
            }

            public Camera PlayerCamera
            {
                get { return _playerCamera; }

                set { _playerCamera = value; }
            }

            public Transform GroundCheck
            {
                get { return _groundCheck; }

                set { _groundCheck = value; }
            }

            public LayerMask GroundMask
            {
                get { return _groundMask; }
            }

            public CharacterController PlayerController
            {
                get { return _playerController; }

                set { _playerController = value; }
            }
            #endregion
        }

        [Serializable]
        private class CameraSetting
        {
            [Header("Sensitivity")]
            [SerializeField][Range(0, 5)] private float _sensitivityMultiplier;
            private float _mouseSensitivity;

            private float _rotation;

            #region Getter & Setter
            public float SensitivityMultiplier
            {
                get { return _sensitivityMultiplier; }

                set
                {
                    if (value <= 0f)
                    {
                        _sensitivityMultiplier = 0f;
                    }
                    else if (value > 5f)
                    {
                        _sensitivityMultiplier = 5f;
                    }
                    else
                    {
                        _sensitivityMultiplier = value;
                    }
                }
            }

            public float MouseSensitivity
            {
                get { return _mouseSensitivity; }

                set
                {
                    if (value <= 0f)
                    {
                        _mouseSensitivity = 0f;
                    }
                    else if (value > 100f)
                    {
                        _mouseSensitivity = 100f;
                    }
                    else
                    {
                        _mouseSensitivity = value;
                    }
                }
            }

            public float Rotation
            {
                get { return _rotation; }

                set { _rotation = value; }
            }
            #endregion
        }

        [Serializable]
        private class MovementSetting
        {
            [Header("Walk Settings")]
            [SerializeField][Range(0, 2)] private float _walkSpeedMultiplier;
            private int _walkSpeed;

            [Header("Run Settings")]
            [SerializeField][Range(0, 2)] private float _runSpeedMultiplier;
            private int _runSpeed;

            [Header("Jump Settings")]
            [SerializeField][Range(0, 2)] private float _jumpForceMultiplier;
            [SerializeField] private int _jumpForce;

            [HideInInspector]
            public Vector3 Movement;

            #region Getter & Setter
            public int WalkSpeed
            {
                get { return _walkSpeed; }

                set { _walkSpeed = value; }
            }

            public int RunSpeed
            {
                get { return _runSpeed; }

                set { _runSpeed = value; }
            }

            public float WalkSpeedMultiplier
            {
                get { return _walkSpeedMultiplier; }

                set
                {
                    if (value <= 0f)
                    {
                        _walkSpeedMultiplier = 0f;
                    }
                    else if (value > 2f)
                    {
                        _walkSpeedMultiplier = 2f;
                    }
                    else
                    {
                        _walkSpeedMultiplier = value;
                    }
                }
            }

            public float RunSpeedMultiplier
            {
                get { return _runSpeedMultiplier; }

                set
                {
                    if (value <= 0f)
                    {
                        _runSpeedMultiplier = 0f;
                    }
                    else if (value > 2f)
                    {
                        _runSpeedMultiplier = 2f;
                    }
                    else
                    {
                        _runSpeedMultiplier = value;
                    }
                }
            }
            public int JumpForce
            {
                get { return _jumpForce; }

                set { _jumpForce = value; }
            }

            public float JumpForceMultiplier
            {
                get { return _jumpForceMultiplier; }

                set
                {
                    if (value <= 0f)
                    {
                        _jumpForceMultiplier = 0f;
                    }
                    else if (value > 2f)
                    {
                        _jumpForceMultiplier = 2f;
                    }
                    else
                    {
                        _jumpForceMultiplier = value;
                    }
                }
            }
            #endregion
        }

        [Serializable]
        private class OtherSetting
        {
            [Header("Gravity")]
            [SerializeField] private int _gravityStrength;

            [Header("Ground")]
            [SerializeField][Range(0, 1)] private float _groundDistance;

            private bool _isGrounded;

            [HideInInspector]
            public Vector3 Velocity;

            #region Getter & Setter
            public int GravityStrength
            {
                get { return _gravityStrength; }

                set
                {
                    if (value <= -50)
                    {
                        _gravityStrength = -50;
                    }
                    else if (value > 0)
                    {
                        _gravityStrength = 0;
                    }
                    else
                    {
                        _gravityStrength = value;
                    }
                }
            }

            public bool IsGrounded
            {
                get
                {
                    return _isGrounded;
                }
                set
                {
                    _isGrounded = value;
                }
            }

            public float GroundDistance
            {
                get { return _groundDistance; }

                set
                {
                    if (value <= 0f)
                    {
                        _groundDistance = 0f;
                    }
                    else if (value > 1f)
                    {
                        _groundDistance = 1f;
                    }
                    else
                    {
                        _groundDistance = value;
                    }
                }
            }
            #endregion
        }
        #endregion

        private void Start()
        {
            SetDefaultSettings();
        }

        #region Load
        private void SetDefaultSettings()
        {
            CameraSettings.MouseSensitivity = 100f;
            CameraSettings.SensitivityMultiplier = 2f;

            MovementSettings.WalkSpeed = 50;
            MovementSettings.WalkSpeedMultiplier = 1f;

            MovementSettings.RunSpeed = 48;
            MovementSettings.RunSpeedMultiplier = 1f;

            MovementSettings.JumpForce = 50;
            MovementSettings.JumpForceMultiplier = 1f;

            OtherSettings.GravityStrength = -60;
            OtherSettings.GroundDistance = 0.1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        #endregion

        private void Update()
        {
            Ground();

            Gravity();

            Camera();

            CheckInput();

            CalculateController();

            /*
            Ray ray = PlayerComponents.PlayerCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit, range, mask))
            {
                MonoBehaviour behaviour = hit.transform.GetComponent<MonoBehaviour>();


                Debug.Log("Hit");
            }
            */
        }

        #region Input
        private void CheckInput()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    Walk();
                }

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Run();
                }
            }
            else { MovementSettings.Movement = Vector3.zero; }

            if (Input.GetButtonDown("Jump") && OtherSettings.IsGrounded)
            {
                Jump();
            }
        }
        #endregion

        #region Final Vector
        private void CalculateController()
        {
            Vector3 finalVector = MovementSettings.Movement + OtherSettings.Velocity;
            PlayerComponents.PlayerController.Move(finalVector * Time.deltaTime);
        }
        #endregion

        #region Gravity
        private void Gravity()
        {
            OtherSettings.Velocity = PlayerControllerHelper.GravityController(OtherSettings.Velocity, OtherSettings.GravityStrength, OtherSettings.IsGrounded);
        }
        #endregion

        #region Walk
        private void Walk()
        {
            MovementSettings.Movement = PlayerControllerHelper.MovementController(PlayerComponents.PlayerObject, MovementSettings.Movement, MovementSettings.WalkSpeed, MovementSettings.WalkSpeedMultiplier);
        }
        #endregion

        #region Run
        private void Run()
        {
            MovementSettings.Movement = PlayerControllerHelper.MovementController(PlayerComponents.PlayerObject, MovementSettings.Movement, MovementSettings.RunSpeed, MovementSettings.RunSpeedMultiplier);
        }
        #endregion

        #region Ground
        private void Ground()
        {
            OtherSettings.IsGrounded = PlayerControllerHelper.GroundController(PlayerComponents.GroundCheck, PlayerComponents.GroundMask, OtherSettings.GroundDistance, OtherSettings.IsGrounded);
        }
        #endregion

        #region Jump
        private void Jump()
        {
            OtherSettings.Velocity = PlayerControllerHelper.JumpController(OtherSettings.Velocity, MovementSettings.JumpForce, MovementSettings.JumpForceMultiplier, OtherSettings.GravityStrength);
        }
        #endregion

        #region Camera
        private void Camera()
        {
            CameraSettings.Rotation = PlayerControllerHelper.CameraController(PlayerComponents.PlayerCamera, PlayerComponents.PlayerObject, CameraSettings.MouseSensitivity, CameraSettings.SensitivityMultiplier, CameraSettings.Rotation);
        }
        #endregion
    }
}
