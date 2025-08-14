using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    public GameObject questionPanel;
    public TMP_Text questionText;
    public Button[] answerButtons;
    public TMP_Text scoreText;
    public TMP_Text salahText;
    public GameObject levelCompletePanel;

    [Header("Soal")]
    public List<QuestionData> questions;
    private int currentQuestionIndex = -1;
    private int totalQuestions = 0;

    [Header("Gameplay")]
    public int score = 0;
    public int salah = 0;
    public int scoreMax = 100;
    public int maxKesalahan = 3;

    [Header("Referensi Player")]
    public Transform playerTransform;
    public Transform playerStartPoint;
    public TrashSpawner trashSpawner;

    private float scorePerQuestion = 0f;
    private int questionsAnswered = 0;
    private GameObject currentTrash;
    private bool isQuestionActive = false;
    private List<QuestionData> originalQuestions;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        totalQuestions = questions.Count;
        scorePerQuestion = (totalQuestions > 0) ? 100f / totalQuestions : 0f;

        originalQuestions = new List<QuestionData>(questions);

        UpdateScoreUI();
        levelCompletePanel.SetActive(false);

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBGMLevel();
        }
    }

    public void ShowQuestion()
    {
        if (questions.Count == 0)
        {
            Debug.LogWarning("No questions assigned!");
            return;
        }

        questionPanel.SetActive(true);
        currentQuestionIndex = Random.Range(0, questions.Count);
        QuestionData q = questions[currentQuestionIndex];

        questionText.text = q.question;

        for (int i = 0; i < answerButtons.Length; i++)
{
    TMP_Text answerLabel = answerButtons[i].GetComponentInChildren<TMP_Text>();

    if (i < q.answers.Length)
    {
        answerLabel.text = q.answers[i];
        int choiceIndex = i;

        answerButtons[i].interactable = true;
        answerButtons[i].onClick.RemoveAllListeners();
        answerButtons[i].onClick.AddListener(() => Answer(choiceIndex == q.correctIndex));
    }
    else
    {
        answerLabel.text = ""; // Kosongkan jika tidak ada jawaban
        answerButtons[i].interactable = false;
    }
}

    }

    public void Answer(bool isCorrect)
    {
        questionsAnswered++;

        if (isCorrect)
        {
            score += Mathf.RoundToInt(scorePerQuestion);
        }
        else
        {
            salah++;
        }

        if (AudioManager.Instance != null)
        {
            if (isCorrect)
                AudioManager.Instance.PlayBenar();
            else
                AudioManager.Instance.PlaySalah();
        }

        if (currentTrash != null)
        {
            Destroy(currentTrash);
            currentTrash = null;
        }

        if (currentQuestionIndex >= 0 && currentQuestionIndex < questions.Count)
        {
            questions.RemoveAt(currentQuestionIndex);
        }

        UpdateScoreUI();
        questionPanel.SetActive(false);
        isQuestionActive = false;

        CheckNextStep();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Skor: " + score;
        salahText.text = "Salah: " + salah;
    }

    void CheckNextStep()
    {
        if (salah >= maxKesalahan)
        {
            string currentLevel = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("LastFailedLevel", currentLevel);
            SceneManager.LoadScene("BattleScene");
        }
        else if (questionsAnswered >= totalQuestions)
        {
            if (score >= 70)
            {
                levelCompletePanel.SetActive(true);
            }
            else
            {
                ResetLevel();
            }
        }
    }

    void ResetLevel()
    {
        score = 0;
        salah = 0;
        questionsAnswered = 0;

        questions = new List<QuestionData>(originalQuestions);
        UpdateScoreUI();
        levelCompletePanel.SetActive(false);

        if (trashSpawner != null)
        {
            trashSpawner.RespawnTrash();
        }

        if (playerTransform != null && playerStartPoint != null)
        {
            CharacterController controller = playerTransform.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
                playerTransform.position = playerStartPoint.position;
                controller.enabled = true;
            }
            else
            {
                playerTransform.position = playerStartPoint.position;
            }
        }
    }

    public void TriggerQuestion(GameObject obj)
    {
        if (isQuestionActive) return;

        currentTrash = obj;
        isQuestionActive = true;

        AudioManager.Instance?.PlayAmbil();
        ShowQuestion();
    }

    public void LoadNextLevel()
    {
        string currentLevel = SceneManager.GetActiveScene().name;

        switch (currentLevel)
        {
            case "Level1":
                PlayerPrefs.SetInt("Level2Unlocked", 1);
                SceneManager.LoadScene("Level2");
                break;
            case "Level2":
                PlayerPrefs.SetInt("Level3Unlocked", 1);
                SceneManager.LoadScene("Level3");
                break;
            case "Level3":
                PlayerPrefs.SetInt("Level4Unlocked", 1);
                SceneManager.LoadScene("Level4");
                break;
            case "Level4":
                PlayerPrefs.SetInt("Level5Unlocked", 1);
                SceneManager.LoadScene("Level5");
                break;
            case "Level5":
                SceneManager.LoadScene("LevelSelect");
                break;
        }

        PlayerPrefs.Save();
    }

    public void BackToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void OnClickSetting()
    {
        AudioManager.Instance?.PlayClick();
    }

    public bool IsQuestionActive()
    {
        return isQuestionActive;
    }
}
