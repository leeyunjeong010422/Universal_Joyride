using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHp : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;

    public Slider hpBar;

    private void Start()
    {
        hpBar = GameObject.Find("BossHP").GetComponent<Slider>();
        currentHP = maxHP;
        UpdateHPBar();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            TakeDamage(2);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHPBar();

        if (currentHP <= 0)
        {
            SoundManager.Instance.StopBGM();
            SceneManager.LoadScene("GameClear");
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
