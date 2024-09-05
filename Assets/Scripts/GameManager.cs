using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int coins; // Количество монет, доступное для сохранения

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Этот объект не будет уничтожен при переходе между сценами
        }
        else
        {
            Destroy(gameObject); // Удаляем дубликаты, если они есть
        }

        LoadCoins();
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt("Coins", GameManager.Instance.coins);
        PlayerPrefs.Save(); // Сохраняем данные на диск
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }
    public void LoadCoins()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            GameManager.Instance.coins = PlayerPrefs.GetInt("Coins");
        }
        else
        {
            GameManager.Instance.coins = 0; // Если данных нет, устанавливаем начальное значение
        }
    }
}