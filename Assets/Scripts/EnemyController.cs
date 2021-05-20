/* Daniel Eggers
 * 12/13/2020
 * Enemy vehicle logic
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float forwardSpeed;  //vspeed
    public float slideSpeed;    //hspeed
    private float steer;        //h-axis
    private bool isAccel;       //used to keep enemy from accelerating until its within view
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        isAccel = false;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Renderer>().isVisible)
            isAccel = true;
        else
            isAccel = false;
        
        if (player.transform.position.x > this.transform.position.x)
            steer = 1;
        else
            steer = -1;
    }

    // FixedUpdate is in sync with physics engine
    void FixedUpdate()
    {
        if (isAccel)
            rb2d.velocity = new Vector2(steer * slideSpeed * Time.fixedDeltaTime, forwardSpeed * Time.fixedDeltaTime);
        else
            rb2d.velocity = new Vector2(0.0f, 0.0f);
    }
}
