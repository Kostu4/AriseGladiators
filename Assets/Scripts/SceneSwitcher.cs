using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    // Map button objects to scene names
    public ButtonSceneMapping[] buttonSceneMappings;

    void Start()
    {
        foreach (ButtonSceneMapping mapping in buttonSceneMappings)
        {
            mapping.button.onClick.AddListener(() => SwitchScene(mapping.sceneName));
        }
    }

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

[System.Serializable]
public class ButtonSceneMapping
{
    public Button button;
    public string sceneName;
}

