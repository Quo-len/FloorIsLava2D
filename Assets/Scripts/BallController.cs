using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [Range(5f, 100f)]
    public float speed = 10f;
    [Range(0.01f, 5f)]
    public float scaleFactor = 1.5f;
    [Range(0f, 5f)]
    public float scaleChangeSpeed = 1f;
    public float massMultiplier = 1f;
    public float dragMultiplier = 1f;
    [Header("Lowest object size")]
    public float minScale = 0.1f;
    public static Vector3 initialPosition;

    public AudioClip shrinkSound;
    public AudioClip enlargeSound;
    public AudioClip jumpSound;
    private AudioSource audioSource;

    private Rigidbody2D rb;
    public float initialMass;
    public float initialDrag;

    private Camera mainCamera;

    private bool canJump = true;
    public float jumpHeight = 20f;

    void Start()
    {
        Debug.Log("Movement with -AD, Shrink - Ctrl, Enlarge - Shift, Jump - Space");
        rb = GetComponent<Rigidbody2D>();
        initialMass = rb.mass;
        initialDrag = rb.drag;

        mainCamera = Camera.main;

        initialPosition = transform.position;

        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0.0f);
        rb.AddForce(movement * speed);

        bool isEnlarging = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool isShrinking = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

        if (isEnlarging)
        {
            if (!audioSource.isPlaying || audioSource.clip != enlargeSound)
            {
                audioSource.clip = enlargeSound;
                audioSource.Play();
            }
            Enlarge();
        }
        else if (isShrinking)
        {
            if (!audioSource.isPlaying || audioSource.clip != shrinkSound)
            {
                audioSource.clip = shrinkSound;
                audioSource.Play();
            }
            Shrink();
        }

        AdjustMassAndDrag();

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            Jump();
        }
    }

    void AdjustMassAndDrag()
    {
        float scaleFactorMagnitude = transform.localScale.magnitude;
        rb.mass = initialMass * scaleFactorMagnitude * massMultiplier;
        rb.drag = initialDrag * scaleFactorMagnitude * dragMultiplier;
    }

    void Enlarge()
    {
        transform.localScale += Vector3.one * scaleFactor * scaleChangeSpeed * Time.deltaTime;
        transform.localScale = Vector3.Max(transform.localScale, new Vector3(minScale, minScale, minScale));
    }

    void Shrink()
    {
        transform.localScale -= Vector3.one * scaleFactor * scaleChangeSpeed * Time.deltaTime;
        transform.localScale = Vector3.Max(transform.localScale, new Vector3(minScale, minScale, minScale));
    }

    void LateUpdate()
    {
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            canJump = true;
        }
    }
    void Jump()
    {
        rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);

        if (jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }
}