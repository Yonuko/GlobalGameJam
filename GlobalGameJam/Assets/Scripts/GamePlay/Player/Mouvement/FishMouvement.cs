using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FishMouvement : MonoBehaviour {

    public Image waterTintPanel;

    public float jumpSpeed;
    public float gravity;
    public float runSpeed;
    public float walkSpeed;
    public float rotateSpeed;

    public float SwimingSpeed;

    float DefaultRunSpeed;
    float DefaultWalkSpeed;

    private bool grounded = false;
    private Vector3 moveDirection = Vector3.zero;
	private bool isWalking = true;
	private bool isRunning;

    private bool canJump = true;

    public bool jump, stopMoving;

    public bool isOnWater;

    private CharacterController controller;

    Animator anim;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();

        waterTintPanel = GameObject.Find("WaterTint").GetComponent<Image>();

        DefaultRunSpeed = runSpeed;
        DefaultWalkSpeed = walkSpeed;
    }

    void Update()
    {
        if (stopMoving)
        {
            return;
        }

        Cursor.lockState = CursorLockMode.Confined;

        waterTintPanel.enabled = isOnWater;

        // Permet de se déplacer que lorsque l'on se trouve sur le sol
        if (grounded && !isOnWater)
        {
            waterTintPanel.enabled = false;
            jump = false;
            canJump = true;
            walkSpeed = DefaultWalkSpeed;
            runSpeed = DefaultRunSpeed;
            moveDirection = new Vector3(((Input.GetMouseButton(1) || (Input.GetMouseButton(0))) ? Input.GetAxis("Horizontal") : 0), 0, Input.GetAxis("Vertical"));
        }
        else if (isOnWater)
        {
            canJump = true;
            walkSpeed = SwimingSpeed;
            runSpeed = SwimingSpeed;
            moveDirection = new Vector3(((Input.GetMouseButton(1) || (Input.GetMouseButton(0))) ? Input.GetAxis("Horizontal") : 0), 0, Input.GetAxis("Vertical"));
        }
        else
        {
            canJump = false;
            moveDirection = new Vector3(0, moveDirection.y / (isWalking || Input.GetAxis("Vertical") < 0 ? walkSpeed : runSpeed), Input.GetAxis("Vertical"));
        }

        if (Input.GetKeyDown(GameObject.FindWithTag("Loader").GetComponent<Loader>().datas.keys["Sprint"]))
        {
            isWalking = !isWalking;
            isRunning = !isRunning;
        }

        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= (isWalking || Input.GetAxis("Vertical") < 0) ? walkSpeed : runSpeed;

        if (Input.GetKeyDown(Loader.get().datas.keys["Jump"]) && canJump)
        {
            jump = true;
            moveDirection.y = jumpSpeed;
        }

        if (!isOnWater)
        {
            //Ajoute la gravité
            moveDirection.y -= gravity * Time.deltaTime;
        }
        else
        {
            if (Input.GetKey(Loader.get().datas.keys["Monter (poisson)"]))
            {
                moveDirection = new Vector3(moveDirection.x, 2 * jumpSpeed, moveDirection.z);
            }
            else if (Input.GetKey(Loader.get().datas.keys["Descendre (poisson)"]))
            {
                moveDirection = new Vector3(moveDirection.x, -2 * jumpSpeed, moveDirection.z);
            }
        }

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
        if (Input.GetMouseButton(1) || Input.GetMouseButton(0))
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        }

        //Déplace le controller
        CollisionFlags flags = controller.Move(moveDirection * Time.deltaTime);
        grounded = (flags & CollisionFlags.Below) != 0;


        if (Input.GetMouseButton(1) || (Input.GetMouseButton(0)))
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

    private void OnTriggerStay(Collider hit)
    {
        if (hit.tag == "Water")
        {
            isOnWater = true;
            Camera.main.GetComponent<CameraController>().SwimingModeEnable(true);
        }
    }

    private void OnTriggerExit(Collider hit)
    {
        if (hit.tag == "Water")
        {
            isOnWater = false;
            Camera.main.GetComponent<CameraController>().SwimingModeEnable(false);
        }
    }
}
