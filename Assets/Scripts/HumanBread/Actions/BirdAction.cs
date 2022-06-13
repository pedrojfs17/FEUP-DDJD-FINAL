using UnityEngine;
using UnityEngine.InputSystem;

public class BirdAction : MonoBehaviour
{
    public GameObject Bread;
    public bool HasBreadAttached = false;
    private Rigidbody birdRb;
    private BirdMovement birdMovement;
    public HumanMovement humanMovement;
    public InputAction PlayerControl;

    // Start is called before the first frame update
    void Start()
    {
        birdRb = GetComponent<Rigidbody>();
        birdMovement = GetComponent<BirdMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerControl.IsPressed() && !HasBreadAttached) // TODO: change the control
            Grab();

        if (HasBreadAttached)   // if bird has taken the bread
            BreadTaken();
    }

    private void BreadTaken()
    {
        Bread.transform.position = new Vector3( // the bread will be attached to the bird
            transform.position.x,
            transform.position.y,
            transform.position.z - 0.5f
        );
    }

    private void Grab()
    {
        // TODO: make grabbing animation
        if (Vector3.Distance(transform.position, Bread.transform.position) < 1) // if the bird is close to the bread
        {
            HasBreadAttached = true;
            birdMovement.Turn();
        }
    }

    public void Eat()
    {
        // TODO: eating animation?
        // TODO: human pissed animation?
        humanMovement.getBreadBack = false;
        // GameSettings.setClassification(this.gameObject);
        // print(GameSettings.Classification.Count);
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
