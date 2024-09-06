using UnityEngine;

public class CoinsController : MonoBehaviour
{
    public static CoinsController Instance { get; private set; }

    public int coins; // Количество монет, доступное для сохранения

    public Coins coinsScript;

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

        coinsScript.coinsBalanceText.text = coins.ToString();
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt("Coins", CoinsController.Instance.coins);
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
            CoinsController.Instance.coins = PlayerPrefs.GetInt("Coins");
        }
        else
        {
            CoinsController.Instance.coins = 0; // Если данных нет, устанавливаем начальное значение
        }
    }
    public void ReduceCoins(int amount)
    {
        coins -= amount;
    }
}