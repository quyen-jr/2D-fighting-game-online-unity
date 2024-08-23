public enum SasukeSkillType
{
    ChidoriSkill,
    SwapPosSkill,
    SusanoSkill,
}
public class SasukeSkill : Skill
{
    // Start is called before the first frame update
    // public SkillManager skill { get; private set; }

    public Sasuke_SkillManager skill { get; private set; }
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
        skill = GetComponentInChildren<Sasuke_SkillManager>();
        player = skill.player;
    }

    protected override void Update()
    {
        base.Update();
    }
}
