using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BR_PlayerController : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;

    public Slider hpBar;

    private void Start()
    {
        hpBar = GameObject.Find("PlayerHP").GetComponent<Slider>();
        currentHP = maxHP;
        UpdateHPBar();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BossAttack"))
        {
            Debug.Log("플레이어");
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHPBar();

        if (currentHP <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void UpdateHPBar()
    {
        if (hpBar != null)
        {
            hpBar.value = currentHP;
        }
    }
}
