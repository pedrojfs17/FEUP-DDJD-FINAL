using UnityEngine;

public class HumanMovement : MonoBehaviour
{
    private Quaternion startRotation;
    private Vector3 startPosition;
    private float timer = 0;
    private int timeOut = 0;
    private Rigidbody humanRb;
    private bool isDistracted = true;
    public bool BreadIsMissing = false;
    private BirdMovement birdMovement;
    private BirdAction birdAction;
    private HumanAction humanAction;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
        startPosition = transform.position;
        humanRb = GetComponent<Rigidbody>();
        birdMovement = GameObject.FindWithTag("Player").GetComponent<BirdMovement>();
        birdAction = GameObject.FindWithTag("Player").GetComponent<BirdAction>();
        humanAction = GameObject.FindWithTag("Human").GetComponent<HumanAction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BreadIsMissing)
            RetrieveBread();
        else
        {
            if (!isDistracted) // if the human is looking to the bread
                CheckBread(); // check the bread situation

            timer += Time.deltaTime;
            if (timer >= timeOut)
                GetFocusedOrDistracted(); // the human will be focused if distracted, or distracted if focused
        }
    }

    private void CheckBread()
    {
        // if bird is moving
        if (birdMovement.Direction.sqrMagnitude > 0)
        {
            if (birdAction.HasBreadAttached) // if bird has taken the bread
            {
                birdMovement.Velocity = 0;
                BreadIsMissing = true; // the human will walk towards the bird.
            }
            else
            {
                birdMovement.Velocity = 0.5f; //TODO: bird afraid animation?
            }
        }
        else
        {
            if (BreadIsMissing)
            {
                // TODO: human confused animation?
            }
        }
    }

    private void RetrieveBread()
    {
        // if the human already has the bread, he will return to his position. Otherwise, he will go towards the bird
        var target = humanAction.HasBreadAttached ? startPosition : birdMovement.BirdRb.position;
        var direction = target - humanRb.position;

        if (transform.position.z >= startPosition.z - 0.15 && target == startPosition) // if he arrives at his original position
        {
            transform.position = startPosition;
            Turn();
            humanAction.Release();
            BreadIsMissing = false;
        }
        else
        {
            if (direction.magnitude < 0.8 && target == birdMovement.BirdRb.position)
            {
                // TODO: stab animation?
                // TODO: bird stunned animation?
                humanAction.Grab();
                Turn();
                birdAction.HasBreadAttached = false;
                humanAction.HasBreadAttached = true;
                birdMovement.Turn();
            }
            else
            {
                humanRb.MovePosition(humanRb.position + (direction * Time.deltaTime));
            }
        }
    }

    private void GetFocusedOrDistracted()
    {
        timer = 0;
        if (isDistracted) // if is already distracted
        {
            timeOut = Random.Range(1, 5);   // generate an time interval focusing
            transform.rotation = startRotation; // is looking to the bread
        }
        else
        {
            timeOut = Random.Range(1, 10); // generate an time interval distracted
            transform.Rotate(Vector3.up, -90 + 180 * Random.Range(0, 2)); // //TODO: rotate animation?
            birdMovement.Velocity = 2;
        }

        isDistracted = !isDistracted;
    }

    private void Turn()
    {
        //TODO: turning animation?
        humanRb.transform.localScale = new Vector3(  // spin the human
            humanRb.transform.localScale.x,
            humanRb.transform.localScale.y,
            humanRb.transform.localScale.z * -1
        );
    }
}
