using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asakura_SkillManager : SkillManager
{
   // public SkillManager instance;
    public Entity player;

    public AsakuraSlashSkill slashSkill { get; private set; }
    public AsakuraSweepSkill sweepSkill { get; private set; }
    public AsakuraUltiSkill ultiSkill { get; private set; }
    public AsakuraDashSkill dashSkill { get; private set; }
    //private void Awake()
    //{
    //    if (instance != null)
    //    {
    //        Destroy(instance.gameObject);
    //    }
    //    else
    //        instance = this;
    //}
    private void Start()
    {
        slashSkill= GetComponent<AsakuraSlashSkill>();
        sweepSkill= GetComponent<AsakuraSweepSkill>();
        ultiSkill= GetComponent<AsakuraUltiSkill>();
        dashSkill= GetComponent<AsakuraDashSkill>();
    }
}
