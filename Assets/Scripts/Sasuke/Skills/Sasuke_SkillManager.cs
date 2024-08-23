using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sasuke_SkillManager : SkillManager
{
    // public SkillManager instance;
    public Entity player;
    public SasukeChidoriSkill chidoriSkill {  get; private set; }
    public SasukeSwapPosSkill swapPosSkill { get; private set; }
    public SasukeSusanoSkill susanoSkill { get; private set; }
    private void Start()
    {
        chidoriSkill=GetComponent<SasukeChidoriSkill>();
        swapPosSkill= GetComponent<SasukeSwapPosSkill>();
        susanoSkill=GetComponent<SasukeSusanoSkill>();
    }
}
