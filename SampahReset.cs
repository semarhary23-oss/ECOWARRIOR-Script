using UnityEngine;

public class SampahReset : MonoBehaviour
{
    private Vector3 posisiAwal;
    private Quaternion rotasiAwal;

    void Start()
    {
        posisiAwal = transform.position;
        rotasiAwal = transform.rotation;
    }

    public void ResetSampah()
    {
        gameObject.SetActive(true);
        transform.position = posisiAwal;
        transform.rotation = rotasiAwal;
    }
}
