using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skill_UI : MonoBehaviour
{
    private Asakura player => GetComponentInParent<Asakura>();
    [SerializeField] private TextMeshProUGUI skillDuration;
    [SerializeField] Image skillImage;
    public SkillType skillType;
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
        if (skillType == SkillType.SweepSkill)
        {
            return player.skill.sweepSkill.coolDownTimer;
        }
        if (skillType == SkillType.SlashSkill)
        {
            return player.skill.slashSkill.coolDownTimer;
        }
        if (skillType == SkillType.UltiSkill)
        {
            return player.skill.ultiSkill.coolDownTimer;
        }
        if (skillType == SkillType.DashSkill)
        {
            return player.skill.dashSkill.coolDownTimer;
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
