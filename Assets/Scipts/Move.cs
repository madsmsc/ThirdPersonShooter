using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;
    public float mouseSpeed;
    public GameObject looking;
    public GameObject head;

    private Vector2 mouseDir;
    private CharacterController characterController;
    private Animator animator;

    private static float MAX_MOUSE_DIR = 50;
    private static float MAX_HEAD_ROTATINO = 20;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        mouseDir = new Vector2();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        mouseLook();
        keyboardMove();
    }

    private void mouseLook()
    {
        mouseDir += new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        float lookingRotation = clamp(-mouseDir.y * mouseSpeed, MAX_MOUSE_DIR);
        looking.transform.localRotation = Quaternion.AngleAxis(lookingRotation, Vector3.right);

        float headRotation = clamp(-mouseDir.y * mouseSpeed, MAX_HEAD_ROTATINO);
        head.transform.localRotation = Quaternion.AngleAxis(headRotation, Vector3.right);
        transform.localRotation = Quaternion.AngleAxis(mouseDir.x * mouseSpeed, Vector3.up);
    }

    private void keyboardMove()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var movement = new Vector3(horizontal, 0, vertical);
        transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);
        if (vertical != 0)
        {
            characterController.SimpleMove(transform.forward * moveSpeed * vertical);
        }
    }

    private float clamp(float f, float max)
    {
        if (f > MAX_MOUSE_DIR)
            return MAX_MOUSE_DIR;
        if (f < -MAX_MOUSE_DIR)
            return -MAX_MOUSE_DIR;
        return f;
    }
}