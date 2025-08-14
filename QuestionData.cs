using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class QuestionData : MonoBehaviour
{
    public string question;
    public string[] answers = new string[3]; // Bisa diganti sesuai jumlah pilihan
    public int correctIndex;
}
