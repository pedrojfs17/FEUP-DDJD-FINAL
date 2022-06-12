using UnityEngine;

public class HumanAction : MonoBehaviour
{
    public GameObject Bread;
    public bool HasBreadAttached = false;
    private Rigidbody humanRb;

    // Start is called before the first frame update
    void Start()
    {
        Bread = GameObject.FindWithTag("Bread");
        humanRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasBreadAttached)
            BreadTaken();
    }

    private void BreadTaken()
    {
        Bread.transform.position = new Vector3( // the bread will be attached to the human
            transform.position.x,
            transform.position.y,
            transform.position.z + 0.5f
        );
    }

    public void Grab()
    {
        // TODO: make grabbing animation
        HasBreadAttached = true;
    }

    public void Release()
    {
        // TODO: make grabbing animation
        HasBreadAttached = false;

        // TODO: optimize bread's position
        Bread.transform.position = new Vector3( // the bread will return to original position
        0,
        0.1f,
        transform.position.z - 0.5f);
    }

    public void Kick()
    {
        // TODO: make the bird move some steps away?
    }
}
