using UnityEngine;
using UnityEngine.InputSystem;

public class BirdMovement : MonoBehaviour
{
    public float Velocity = 2;
    public Vector3 Direction;
    public Rigidbody BirdRb;
    private BirdAction birdAction;
    public InputAction PlayerControl;

    // Start is called before the first frame update
    void Start()
    {
        BirdRb = GetComponent<Rigidbody>();
        birdAction = GetComponent<BirdAction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z <= -4.4f && birdAction.HasBreadAttached)
        {
            birdAction.Eat();
            Direction = Vector3.zero;
        }
        else
            Direction = new Vector3(0, 0, birdAction.HasBreadAttached ? -PlayerControl.ReadValue<float>() : PlayerControl.ReadValue<float>()); // TODO: change the control
    }

    private void FixedUpdate()
    {
        BirdRb.MovePosition
            (BirdRb.position +
            (Direction * Time.deltaTime * Velocity));
    }

    public void Turn()
    {
        //TODO: turning animation?
        BirdRb.transform.localScale = new Vector3(  // spin the bird
            BirdRb.transform.localScale.x,
            BirdRb.transform.localScale.y,
            BirdRb.transform.localScale.z * -1
        );
    }

    private void OnEnable()
    {
        PlayerControl.Enable();
    }

    private void OnDisable()
    {
        PlayerControl.Disable();
    }
}
