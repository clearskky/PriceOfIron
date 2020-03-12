using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image ForegroundImage;
    [SerializeField] private float HealthUpdateTime;
    
    public void HandleHealthChange(float NewHealthPercentage)
    {
        StartCoroutine(ChangeHealthToNewPercentage(NewHealthPercentage));
    }
    private IEnumerator ChangeHealthToNewPercentage(float NewHealthPercentage)
    {
        float PercentBeforeDamage = ForegroundImage.fillAmount;
        float ElapsedTime = 0;

        while (ElapsedTime < HealthUpdateTime)
        {
            ElapsedTime += Time.deltaTime;
            ForegroundImage.fillAmount = Mathf.Lerp(PercentBeforeDamage, NewHealthPercentage, ElapsedTime / HealthUpdateTime);
            yield return null;
        }

        ForegroundImage.fillAmount = NewHealthPercentage;
    }
    private void LookAtCamera()
    {
        Vector3 rot = Camera.main.transform.rotation.eulerAngles;
        rot.y = 0;
        rot.z = 0;
        transform.rotation = Quaternion.Euler(rot);
    }
    void LateUpdate()
    {
        LookAtCamera();
    }
}
