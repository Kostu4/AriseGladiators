using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject levelSelectPanel;
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private Button openLevelSelectButton;

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
            if (i + 1 <= unlockedLevel)
            { 
                levelButtons[i].interactable = true;
                int levelIndex = i + 1;
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void OpenLevelSelect()
    {
        levelSelectPanel.SetActive(!levelSelectPanel.activeSelf);
    }
    public void LoadLevel(int levelIndex)
    { 
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        //if
    }
}
