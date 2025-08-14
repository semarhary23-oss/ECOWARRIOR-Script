using UnityEngine;

public class TrashTrigger : MonoBehaviour

{
    private bool alreadyTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            alreadyTriggered = true;

            // Tampilkan pertanyaan
            GameManager.instance.TriggerQuestion(gameObject);

            // Hancurkan objek sampah SETELAH delay kecil (optional, supaya tidak langsung hilang sebelum pertanyaan muncul)
            Destroy(gameObject, 0.1f);
        }

    }

}

