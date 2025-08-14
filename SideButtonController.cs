using UnityEngine;

public class SideButtonController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    private int moveDirection = 0; // -1 = kiri, 1 = kanan, 0 = diam

    [Header("References")]
    public CharacterController characterController;
    public Animator animator;

    void Update()
    {
        // Cek apakah sedang menjawab pertanyaan
        if (GameManager.instance != null && GameManager.instance.IsQuestionActive())
        {
            moveDirection = 0;
        }

        Vector3 move = new Vector3(moveDirection, 0f, 0f); // hanya gerak X (kanan-kiri)

        if (move.magnitude > 0.1f)
        {
            characterController.Move(move * moveSpeed * Time.deltaTime);
            animator.SetBool("isWalking", true);

            // Putar hanya ke kanan (0°) atau kiri (180°)
            Vector3 scale = transform.localScale;
            scale.x = moveDirection; // 1 untuk kanan, -1 untuk kiri
            transform.localScale = scale;
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    // Dipanggil dari tombol
    public void MoveLeft()
    {
        moveDirection = -1;
    }

    public void MoveRight()
    {
        moveDirection = 1;
    }

    public void StopMove()
    {
        moveDirection = 0;
    }
}
