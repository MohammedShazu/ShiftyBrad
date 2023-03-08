using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed;
    //private float jumpHeight = 1.0f;
    //private float gravityValue = -9.81f;
    float horizontal;
    float vertical;
    float speed = 0.5f;


    void Start()
    {
        horizontal = Input.GetAxis("Horizontal"); //giving the player greenlight for its movement
        vertical = Input.GetAxis("Vertical");
        controller = gameObject.AddComponent<CharacterController>();
    }
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //whenever I'm going horizontally, vertically is not coming into play and vice versa
        controller.Move(move * Time.deltaTime * playerSpeed); //define speed of player
    }

}
