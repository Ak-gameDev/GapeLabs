using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourPun
{
    private Rigidbody playerRigidBody;

    [SerializeField] private float speed = 0.5f;

    private Joystick joystick;

    private float x = 0;
    private float z = 0;

    // Start is called before the first frame update
    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        joystick = FindObjectOfType<Joystick>();
            
        if (!photonView.IsMine)
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (joystick)
        {
            x = joystick.Horizontal;
            z = joystick.Vertical;
        }
    }

    private void FixedUpdate()
    {
        playerRigidBody.AddForce(new Vector3(x, 0, z) * speed);
    }
}