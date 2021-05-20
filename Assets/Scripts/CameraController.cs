/* Daniel Eggers
 * 12/13/2020
 * Main camera control
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float offsetVertical;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.position.x, (player.position.y + offsetVertical), this.transform.position.z);
    }
}
