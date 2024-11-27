using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnim;
    public Rigidbody rb;

    public float playerSpeed = 10f;
    public float playerJump = 5f;

    private bool isGrounded;
    [SerializeField] public Vector3 checkPoint;

    public AudioSource SFXSource;
    public AudioClip jumpAudio;

    void Awake()
    {
        if (rb != null)
        {
            rb.freezeRotation = true;
        }

        checkPoint = transform.position;
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;

        transform.Translate(h, 0, v);

        /*if (h != 0 || v != 0)
        {
            playerAnim.SetBool("isRuning", true);
        }
        else
        {
            playerAnim.SetBool("isRuning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            SFXSource.clip = jumpAudio;
            SFXSource.Play();
            rb.AddForce(Vector3.up * playerJump, ForceMode.Impulse);
        }*/
    }
    public void LoadCheckPoint()
    {
        transform.position = checkPoint;
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("NormalizePad") || collision.collider.CompareTag("SpeedPad") || collision.collider.CompareTag("SlowPad") || collision.collider.CompareTag("JumpPad"))
        {
            isGrounded = true;
            playerAnim.SetBool("isJumping", false);

        }

        if (collision.collider.CompareTag("NormalizePad"))
        {
            playerSpeed = 10f;
            playerJump = 5f;
        }

        if (collision.collider.CompareTag("SpeedPad"))
        {
            playerSpeed = 15f;
        }

        if (collision.collider.CompareTag("SlowPad"))
        {
            playerSpeed = 5f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
            playerAnim.SetBool("isJumping", true);
        }
    }*/ 
}
