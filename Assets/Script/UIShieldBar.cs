using UnityEngine;

public class UIShieldBar : MonoBehaviour
{
    public ShieldController shield;
    public RectTransform bar;

    private void Update()
    {
        if (shield == null || bar == null) return;

        float ratio = (float)shield.CurrentHealth / shield.maxHealth;
        bar.localScale = new Vector3(1f, ratio, 1f);
    }

}
