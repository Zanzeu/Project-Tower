using System.Collections.Generic;

namespace Tower.Runtime.Gameplay
{
    public static class EffectConfigDic
    {
        public static readonly Dictionary<string, IEffectParam> Dic = new()
        {
            { "AddBuff", new EffectParam_AddBuff() },
            { "AddSpeedAllEnemy", new EffectParam_AddSpeedAllEnemy() },
            { "AttriModifier", new EffectParam_AttriModifier() },
            { "ChangeAttrType", new EffectParam_ChangeAttrType() },
            { "CreateAoE", new EffectParam_CreateAoE() },
            { "CreateBurnAoE", new EffectParam_CreateBurnAoE() },
            { "CreateDamageModifierAoE", new EffectParam_CreateDamageModifierAoE() },
            { "CreateIceAoE", new EffectParam_CreateIceAoE() },
            { "CreateSlowAoE", new EffectParam_CreateSlowAoE() },
            { "DamageModifier", new EffectParam_DamageModifier() },
            { "FireBullet", new EffectParam_FireBullet() },
            { "ReleasePrefab", new EffectParam_ReleasePrefab() },
            { "TakeDamage", new EffectParam_TakeDamage() },
        };
    }
}
