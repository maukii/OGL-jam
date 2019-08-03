using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumbleweed : MonoBehaviour
{

    public float Speed = 1;
    public float AnimationOffset = 15;

    private void Start()
    {
        transform.position = new Vector3(AnimationOffset * -1, transform.position.y, transform.position.z);
    }

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * Speed, Space.World);

        if (transform.position.x >= 9)
            transform.position = new Vector3(AnimationOffset * -1, transform.position.y, transform.position.z);
    }
}
