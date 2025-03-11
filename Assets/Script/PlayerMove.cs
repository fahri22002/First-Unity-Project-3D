using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed;
    public Vector2 mouse;
    public Transform cameraTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float inputAD;
        float inputWS;
        float inputEsc;
        Vector3 inputWASD;

        inputAD = Input.GetAxis("Horizontal");
        inputWS = Input.GetAxis("Vertical");
        inputEsc = Input.GetAxis("Cancel");

        // Ambil arah kamera
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Hilangkan pergerakan vertikal agar tidak terbang
        forward.y = 0;
        right.y = 0;

        // Normalisasi agar kecepatan tetap stabil
        forward.Normalize();
        right.Normalize();

        // Hitung arah gerakan relatif terhadap kamera
        inputWASD = (forward * inputWS + right * inputAD).normalized;

        // Gunakan arah gerakan yang sudah dikoreksi
        controller.Move(inputWASD * moveSpeed * Time.deltaTime);

        mouse.x += Input.GetAxis("Mouse X");
        mouse.y += Input.GetAxis("Mouse Y");
        transform.localRotation = Quaternion.Euler(-mouse.y, mouse.x, 0);
        if (inputEsc > 0)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadSceneAsync(0);
        }
        
    }
}
