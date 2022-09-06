using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetPlayer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float minX;
    [SerializeField] private float minY;
    [SerializeField] private float maxX;
    [SerializeField] private float maxY;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private AudioSource pauseAudio;
    private bool isPaused;

    private float cameraShakePower;
    private Vector3 shakeActive;

    private void Update()
    {
        HandleTimeShake();
        HandlePause();
    }

    private void LateUpdate()
    {
        HandleCameraFollow();
        HandleCameraClamp();
    }

    void HandleCameraFollow()
    {
        transform.position = Vector3.Lerp(transform.position, targetPlayer.position, moveSpeed * Time.deltaTime);
    }

    void HandleCameraClamp()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), -10);
    }

    public void StartCameraShake(float _amount)
    {
        cameraShakePower = _amount;
    }

    public void HandleTimeShake()
    {
        if (cameraShakePower > 0)
        {
            shakeActive = new Vector3(Random.Range(-cameraShakePower, cameraShakePower), Random.Range(-cameraShakePower, cameraShakePower), 0f);
            cameraShakePower -= Time.deltaTime;
        }
        else
        {
            shakeActive = Vector3.zero;
        }
        transform.position += shakeActive;
    }

    void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                pauseAudio.Play();
                Time.timeScale = 0;
                pauseCanvas.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pauseCanvas.SetActive(false);
            }
        }
    }
}
