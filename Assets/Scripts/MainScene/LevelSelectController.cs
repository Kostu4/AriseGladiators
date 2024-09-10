using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject levelSelectPanel;
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private Button openLevelSelectButton;

    public UnityEvent LevelIsCompleted = new();

    private void Start()
    {
        levelSelectPanel.SetActive(false);
        openLevelSelectButton.onClick.AddListener(OpenLevelSelect);
        UpdateLevelButtons();
    }
    private void UpdateLevelButtons()
    { 
        int unlockedLevel = PlayerPrefs.GetInt("unlockedLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;
            levelButtons[i].interactable = levelIndex <= unlockedLevel; // Активируем кнопки до разблокированного уровня
            levelButtons[i].onClick.RemoveAllListeners(); // Очищаем все слушатели, чтобы избежать повторного добавления
            if (levelButtons[i].interactable)
            {
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex)); // Добавляем слушатель загрузки уровня
            }
        }
    }

    public void OpenLevelSelect()
    {
        levelSelectPanel.SetActive(!levelSelectPanel.activeSelf);
    }
    public void LoadLevel(int levelIndex)
    {
        LevelManager levelManager = LevelManager.Instance;
        if (levelManager != null) 
        {
            levelManager.SetupLevel(levelIndex);
            SceneManager.LoadScene("InstanceScene");
            levelSelectPanel.SetActive(false);
        }
    }

    public void UnlockNextLevel(int completedLevel)
    {
        int highestUnlockedLevel = PlayerPrefs.GetInt("unlockedLevel", 1);

        if (completedLevel >= highestUnlockedLevel)
        {
            PlayerPrefs.SetInt("unlockedLevel", completedLevel + 1);
            PlayerPrefs.Save();
            UpdateLevelButtons();
        }
    }
}
