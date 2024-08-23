using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsakuraSweepSkill : AsakuraSkill
{
    public int sweepSpeed;
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        if (!player.photonView.IsMine) return;
    }

    protected override void Update()
    {
        base.Update();
    }
}
