﻿using UnityEngine;
using System.Collections;

public class PlayerMouvement : MonoBehaviour {

    public float jumpSpeed;
    public float gravity;
    public float runSpeed;
    public float walkSpeed;
    public float rotateSpeed;

    private bool grounded = false;
    private Vector3 moveDirection = Vector3.zero;
	private bool isWalking = true;
	private bool isRunning;

    private bool canJump = true;

    public bool jump, stopMoving;

    private CharacterController controller;

    Animator anim;

    [SerializeField]
    Coroutine idleCoroutine = null;

    static PlayerMouvement instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        if (stopMoving)
        {
            //Ajoute la gravité
            moveDirection.y -= gravity * Time.deltaTime;
            //Déplace le controller
            controller.Move(moveDirection * Time.deltaTime);
            return;
        }

        Cursor.lockState = CursorLockMode.Confined;
				
        // Permet de se déplacer que lorsque l'on se trouve sur le sol
        if (grounded)
        {

            jump = false;

            moveDirection = new Vector3((Input.GetMouseButton(1) ? Input.GetAxis("Horizontal") : 0), 0, Input.GetAxis("Vertical"));

            if (Input.GetKeyDown(GameObject.FindWithTag("Loader").GetComponent<Loader>().datas.keys["Sprint"]))
            {
                isWalking = !isWalking;
                isRunning = !isRunning;
            }

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= (isWalking || Input.GetAxis("Vertical") < 0) ? walkSpeed : runSpeed;

            if (Input.GetKeyDown(GameObject.FindWithTag("Loader").GetComponent<Loader>().datas.keys["Jump"]) && canJump)
            {
                jump = true;
                moveDirection.y = jumpSpeed;
            }

        }
        else
        {
            //moveDirection = new Vector3((Input.GetMouseButton(1) ? Input.GetAxis("Horizontal") : 0), moveDirection.y, Input.GetAxis("Vertical"));

            /*moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= (isWalking || Input.GetAxis("Vertical") < 0) ? walkSpeed : runSpeed;*/
        }


        //Ajoute la gravité
        moveDirection.y -= gravity * Time.deltaTime;

        if (Input.GetAxis("Vertical") != 0 && Input.GetAxis("Vertical") <= 0.2 && Input.GetAxis("Vertical") >= -0.2)
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        }

        if(Input.GetAxis("Horizontal") > 0.1f && Input.GetAxis("Horizontal") != 0)
        {
            transform.Rotate(0,rotateSpeed * Time.deltaTime,0);
        }
        else if (Input.GetAxis("Horizontal") < 0.1f && Input.GetAxis("Horizontal") != 0)
        {
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
        }

        // Faire en sorte que le joueur regarde dans la même direction que la caméra grâce au clique droit
        if (Input.GetMouseButton(1))
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        }

        //Déplace le controller
        CollisionFlags flags = controller.Move(moveDirection * Time.deltaTime);
        grounded = (flags & CollisionFlags.Below) != 0;


        if (Input.GetMouseButton(1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
