using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image hpImg; // Ѫ����ʾ��Image�ؼ�
    public Image hpEffectImg; // Ѫ���仯Ч����Image�ؼ�

    public float maxHp = 100f; // ���Ѫ��
    public float currentHp; // ��ǰѪ��
    public float buffTime = 0.5f; // Ѫ������ʱ��

    private Coroutine updateCoroutine;

    private void Start()
    {
        currentHp = maxHp; // ��ʼʱ��Ϊ��Ѫ
        UpdateHealthBar(); // ����Ѫ����ʾ
    }

    public void SetHealth(float health)
    {
        // ����Ѫ����0�����Ѫ��֮��
        currentHp = Mathf.Clamp(health, 0f, maxHp);

        // ����Ѫ����ʾ
        UpdateHealthBar();

        // ��Ѫ��С�ڵ���0ʱ����������Ч��
        if (currentHp <= 0)
        {
            // Die();
        }
    }

    // ��������
    private void Die()
    {
        // �ڴ˴����������صĴ���
    }

    // �ڽ��ö���ʱֹͣЭ��
    private void OnDisable()
    {
        if (updateCoroutine != null)
        {
            StopCoroutine(updateCoroutine);
        }
    }

    // ����Ѫ��
    public void IncreaseHealth(float amount)
    {
        SetHealth(currentHp + amount);
    }

    // ����Ѫ��
    public void DecreaseHealth(float amount)
    {
        SetHealth(currentHp - amount);
    }

    // ����Ѫ����ʾ
    private void UpdateHealthBar()
    {
        // ���ݵ�ǰѪ�������Ѫ�����㲢����Ѫ����ʾ
        hpImg.fillAmount = currentHp / maxHp;

        // ��������Ѫ���仯Ч�������ֵ
        if (updateCoroutine != null)
        {
            StopCoroutine(updateCoroutine);
        }

        updateCoroutine = StartCoroutine(UpdateHpEffect());
    }

    // Э�̣�����ʵ�ֻ�������Ѫ���仯Ч�������ֵ
    private IEnumerator UpdateHpEffect()
    {
        float effectLength = hpEffectImg.fillAmount - hpImg.fillAmount; // ����Ч������
        float elapsedTime = 0f; // �ѹ�ȥ��ʱ��

        while (elapsedTime < buffTime && effectLength != 0)
        {
            elapsedTime += Time.deltaTime; // �����ѹ�ȥ��ʱ��
            hpEffectImg.fillAmount = Mathf.Lerp(hpImg.fillAmount + effectLength, hpImg.fillAmount, elapsedTime / buffTime); // ʹ�ò�ֵ��������Ч�����ֵ
            yield return null;
        }

        hpEffectImg.fillAmount = hpImg.fillAmount; // ȷ�����ֵ��Ѫ�����ֵһ��
    }
}
