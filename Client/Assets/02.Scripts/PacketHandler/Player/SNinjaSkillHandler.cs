using Google.Protobuf;
using ImpelDown.Proto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNinjaSkillHandler : IPacketHandler {
    public void Process(IMessage packet) {
        S_Ninja_Skill msg = packet as S_Ninja_Skill;


    }
}
