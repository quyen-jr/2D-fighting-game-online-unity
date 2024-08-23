using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sasuke_Skill_Slot_UI : MonoBehaviour
{
    private Sasuke player => GetComponentInParent<Sasuke>();
    [SerializeField] private TextMeshProUGUI skillDuration;
    [SerializeField] Image skillImage;
    public SasukeSkillType skillType;
    private Skill skill;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float duration = GetTimeDuration();
        if (duration > 0)
        {
            DisableSkill();
            skillDuration.text = RoundToDecimal(duration, 1).ToString();
        }
        else
        {
            EnableSkill();
            skillDuration.text = "";
        }
    }
    private float GetTimeDuration()
    {
        if (skillType == SasukeSkillType.SwapPosSkill)
        {
            return player.skill.swapPosSkill.coolDownTimer;
        }
        if (skillType == SasukeSkillType.SusanoSkill)
        {
            return player.skill.susanoSkill.coolDownTimer;
        }
        if (skillType == SasukeSkillType.ChidoriSkill)
        {
            return player.skill.chidoriSkill.coolDownTimer;
        }
        return 0;
    }
    public static float RoundToDecimal(float number, int decimalPlaces)
    {
        float multiplier = Mathf.Pow(10, decimalPlaces);
        return Mathf.Round(number * multiplier) / multiplier;
    }
    private void EnableSkill()
    {
        Color imageColor = skillImage.color;
        imageColor.a = 1f;
        skillImage.color = imageColor;
    }
    private void DisableSkill()
    {
        Color imageColor = skillImage.color;
        imageColor.a = 0.25f;
        skillImage.color = imageColor;
    }
}
