using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 10f;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;

        transform.Translate(h, 0, v);
    }
}