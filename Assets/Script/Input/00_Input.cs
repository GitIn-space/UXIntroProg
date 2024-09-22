using UnityEngine;
using UnityEngine.InputSystem;

//public - accessible from everywhere, class - collection of things, Input - name, : MonoBehaviour - inheritence (ask a programmer or something, it's supposed to be there)
public class Input : MonoBehaviour
{
    /*
    SerializeField - exposes the variable to the inspector
    private - opposite of public, only accessible to Input itself
    float - decimal number
    speed - name
    = - assignment operator, gives the thing on the left the value of the right (in this case, speed is given the value 1)
    1 - default value, can be changed in the inspector
    */
    [SerializeField] private float speed = 1;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float gravityScale = 0;

    //Not exposed to the inspector
    private Vector2 dir = new Vector2();
    private Rigidbody2D body;

    //void - return type of the function, nothing in this case
    //OnMove - name
    //input - argument, something to send into the function
    private void OnMove(InputValue input)
    {
        dir = input.Get<Vector2>(); //store the buttons pressed as they get pressed or released
    }

    private void Move()
    {
        Vector3 velocity = body.velocity;
        //add movement speed to our current speed in x (left-right) only, maintain speed in y (up-down)
        velocity[0] = Mathf.Clamp(transform.right.normalized.x * dir.x * speed, -speed, speed);
        body.velocity = velocity;
    }

    //fixedUpdate or update - called a certain times per frame on all MonoBehaviour
    private void FixedUpdate()
    {
        Move();

        Gravity();
    }

    //awake - called on objects in the scene when the scene is started
    private void Awake()
    {
        //get our own rigidBody and save for later
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Gravity()
    {
        //apply gravity to our current velocity
        //(Vector2) - interpret the next variable as this data type
        body.velocity -= (Vector2) transform.up.normalized * gravityScale;
    }

    private void OnJump(InputValue input)
    {
        //one line if doesn't need {}
        if (input.isPressed)
            //apply a "push" upwards
            body.velocity += (Vector2) transform.up.normalized * jumpForce;
    }
}
