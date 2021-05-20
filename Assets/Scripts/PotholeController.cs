/* Daniel Eggers
 * 12/13/2020
 * Adds randomness to horizontal pothole placement
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotholeController : MonoBehaviour
{
    public float minXPos;
    public float maxXPos;

    // Start is called before the first frame update
    void Start()
    {
        float randomX = Random.Range(minXPos, maxXPos);
        transform.position = new Vector2(randomX, this.transform.position.y);
    }
}
