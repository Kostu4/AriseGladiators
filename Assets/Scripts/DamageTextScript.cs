using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextScript : MonoBehaviour
{
    [SerializeField] private GameObject damageTextPrefab;  // Префаб текста урона
    [SerializeField] private Transform hpBarTransform;     // Трансформ для размещения текста урона


    // Метод для отображения текста урона
    public void ShowDamage(float damage)
    {
        Debug.Log("DA_DA_BLYAT");
        // Создаем экземпляр текста урона над указанной позицией
        GameObject damageText = Instantiate(damageTextPrefab, hpBarTransform.position, Quaternion.identity, hpBarTransform);

        // Получаем компонент TextMeshPro и устанавливаем текст урона
        TMP_Text tmpText = damageText.GetComponent<TMP_Text>();  // Используйте TextMeshPro, если это он
        tmpText.text = damage.ToString();

        // Запускаем анимацию
        Animator animator = damageText.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("DamageTextAnimation");  // Название вашей анимации
        }
    }
    void DestroyGO()
    { Destroy(gameObject); }
}
