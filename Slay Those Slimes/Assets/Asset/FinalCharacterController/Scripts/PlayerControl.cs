using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Asset.FinalCharacterController
{

    public class PlayerControl : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Camera _playerCamera;

        public float runAcceleration = 0.25f;
        public float runSpeed = 4f;
        public float drag = 0.1f;

        private PlayerLocomotionInput _playerLocomotionInput;

        private void Awake()
        {
            _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();

        }

        private void Update()
        {
            Vector3 cameraForwardXZ = new Vector3(_playerCamera.transform.forward.x, 0f, _playerCamera.transform.forward.z).normalized;
            Vector3 cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0f, _playerCamera.transform.right.z).normalized;
            Vector3 movementDirection = cameraRightXZ * _playerLocomotionInput.MovementInput.x + cameraForwardXZ * _playerLocomotionInput.MovementInput.y;


            Vector3 movementDelta = movementDirection * runAcceleration * Time.deltaTime;
            Vector3 newVelocity = _characterController.velocity + movementDelta;


            //add drag to the player
            Vector3 currentDrag = newVelocity.normalized * drag * Time.deltaTime;
            newVelocity = (newVelocity.magnitude > drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;

            //unity suggests to call this only once per frame tick inside unity runtime
            _characterController.Move(newVelocity * Time.deltaTime);
        }
    }
}