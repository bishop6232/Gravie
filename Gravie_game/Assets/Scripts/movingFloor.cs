using UnityEngine;

public class movingFloor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed; //speed of the moving floor
    public int startingPoint; //starting point index
    public Transform[] points; //array of points for the floor to move between

    private int i; //index of the array

    void Start()
    {
        transform.position = points[startingPoint].position; //set the initial position of the floor
    }

    // Update is called once per frame
    void Update()
    {
        //checking the distance of the platform and the point
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++; //increase the index
            if (i >= points.Length) //check if the platform was on the last point after the increase
            {
                i = 0; //reset the index
            }
        }

        //moving the platform to the point position with the index "i"
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) //check if the object that collided with the platform is the player
        {
            collision.gameObject.transform.SetParent(transform); //set the player as a child of the platform
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) //check if the object that collided with the platform is the player
        {
            collision.gameObject.transform.SetParent(null); //remove the player as a child of the platform
        }
    }
}


