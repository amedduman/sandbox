using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
        [SerializeField] Transform _cameraPivot;
        [Range(-5f, -25f)] [SerializeField] float gravity = -15f;
        [Range(5f, 15f)] [SerializeField] float movementSpeed = 10f;
        [Range(5f, 15f)] [SerializeField] float jumpSpeed = 10f;
        [Range(1f, 90f)] [SerializeField] float maxPitch = 85f;
        [Range(-1f, -90f)] [SerializeField] float minPitch = -85f;
        [Range(0.5f, 5f)] [SerializeField] float mouseSensitivity = 2f;
        
        float yVelocity = 0f;
        float pitch = 0f;
        CharacterController cc;

        private void Start()
        {
            if (!photonView.IsMine) GetComponentInChildren<Camera>().gameObject.SetActive(false);
            cc = GetComponent<CharacterController>();
        }
        
        void Update()
        {
            if(!photonView.IsMine) return;
            Look();
            Move();
        }

        void Look()
        {
            //get the mouse inpuit axis values
            float xInput = Input.GetAxis("Mouse X") * mouseSensitivity;
            float yInput = Input.GetAxis("Mouse Y") * mouseSensitivity;
            //turn the whole object based on the x input
            transform.Rotate(0, xInput, 0);
            //now add on y input to pitch, and clamp it
            pitch -= yInput;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
            //create the local rotation value for the camera and set it
            Quaternion rot = Quaternion.Euler(pitch, 0, 0);
            _cameraPivot.localRotation = rot;
        }

        void Move()
        {
            //update speed based onn the input
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            input = Vector3.ClampMagnitude(input, 1f);
            //transofrm it based off the player transform and scale it by movement speed
            Vector3 move = transform.TransformVector(input) * movementSpeed;
            //is it on the ground
            if (cc.isGrounded)
            {
                //make sure we are defintely touch the ground
                yVelocity = -1f;
                //check for jump here
                if (Input.GetButtonDown("Jump"))
                {
                    yVelocity = jumpSpeed;
                }
            }
            //now add the gravity to the yvelocity
            yVelocity += gravity * Time.deltaTime;
            move.y = yVelocity;
            //and finally move
            cc.Move(move * Time.deltaTime);
        }
}
