using UnityEngine;

namespace EternalBytes.Player.Controller.Helper
{
    public static class PlayerControllerHelper
    {
        #region Camera
        public static float CameraController(Camera camera, Transform player, float sensitivity, float multiplier, float rotation)
        {
            float _X = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime * multiplier;
            float _Y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime * multiplier;

            rotation -= _Y;
            rotation = Mathf.Clamp(rotation, -90f, 90);

            camera.transform.localRotation = Quaternion.Euler(rotation, 0f, 0f);
            player.Rotate(Vector3.up * _X);

            return rotation;
        }
        #endregion

        #region Movement
        public static Vector3 MovementController(Transform player ,Vector3 movement ,int speed, float multiplier)
        { 
            float X = Input.GetAxis("Horizontal");
            float Z = Input.GetAxis("Vertical");

            movement = player.right * X + player.forward * Z;
            movement = movement * speed * multiplier;

            return movement;
        }
        #endregion

        #region Gravity
        public static Vector3 GravityController(Vector3 velocity, float strength, bool isGrounded)
        {
            velocity.y += strength * Time.deltaTime;

            if (isGrounded)
            {
                velocity.y = 0f;
            }

            return velocity;
        }
        #endregion

        #region Ground
        public static bool GroundController(Transform groundCheck, LayerMask mask, float distance, bool isGrounded)
        {
            return isGrounded = Physics.CheckSphere(groundCheck.position, distance, mask);
        }
        #endregion

        #region Jump
        public static Vector3 JumpController(Vector3 velocity, int force, float multiplier, float strength)
        {
            velocity.y = Mathf.Sqrt(force * multiplier * -2f * strength);

            return velocity;
        }
        #endregion
    }
}
