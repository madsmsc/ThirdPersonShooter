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

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        mouseDir = new Vector2();
    }

    void Update()
    {
        // mouse look
        mouseDir += new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        // clamp up/down rotation
        if (mouseDir.y > 2.5f)
            mouseDir.y = 2.5f;
        if (mouseDir.y < -2.5f)
            mouseDir.y = -2.5f;
        looking.transform.localRotation = Quaternion.AngleAxis(-mouseDir.y * mouseSpeed, Vector3.right);

        // clamp head rotation
        float headRotation = mouseDir.y;
        if (headRotation > 1)
            headRotation = 1;
        if (headRotation < -1)
            headRotation = -1;
        head.transform.localRotation = Quaternion.AngleAxis(-headRotation * mouseSpeed, Vector3.right);
        transform.localRotation = Quaternion.AngleAxis(mouseDir.x * mouseSpeed, Vector3.up);

        // keyboard move/rotate
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var movement = new Vector3(horizontal, 0, vertical);
        transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);
        if (vertical != 0)
        {
            characterController.SimpleMove(transform.forward * moveSpeed * vertical);
        }
    }
}