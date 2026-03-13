using UnityEngine;

public abstract class BattleComponent : MonoBehaviour
{
    public abstract void BattleSetUp(BattleInitiator battleSystem);

    //When save system is completed this could probably be deleted
    public abstract void ResumeBattle(BattleInitiator battleSystem);
}
