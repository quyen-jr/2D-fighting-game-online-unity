using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    SlashSkill,
    SweepSkill,
    UltiSkill,
    DashSkill
}
public class AsakuraSkill : Skill
{
    // Start is called before the first frame update
    // public SkillManager skill { get; private set; }

    public Asakura_SkillManager skill { get; private set; }
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();
    }

    protected override void Start()
    {
        skill = GetComponentInChildren<Asakura_SkillManager>();
        player = skill.player;
    }

    protected override void Update()
    {
        base.Update();
    }
}
