﻿namespace AtomicTorch.CBND.CoreMod.Skills
{
    using AtomicTorch.CBND.CoreMod.Stats;

    public class SkillWeaponsConventional : ProtoSkillWeaponsRanged
    {
        public override string Description =>
            "Gaining more experience with all conventional firearms makes you accustomed to all of their nuances and enables you to use any of them more effectively.";

        public override double ExperienceAddedOnKillPerMaxEnemyHealthMultiplier => 0.25;

        public override double ExperienceAddedPerDamageDoneMultiplier => 1.0;

        /// <summary>
        /// This is intended to reward experience per ammo expended. Basically resource->exp conversion.
        /// </summary>
        public override double ExperienceAddedPerShot => 5;

        public override double ExperienceToLearningPointsConversionMultiplier => 1.0;

        public override string Name => "Conventional weapons";

        public override StatName StatNameDamageBonusMultiplier
            => StatName.WeaponConventionalDamageBonusMultiplier;

        public override StatName StatNameDegrationRateMultiplier
            => StatName.WeaponConventionalDegradationRateMultiplier;

        public override StatName? StatNameReloadingSpeedMultiplier
            => StatName.WeaponConventionalReloadingSpeedMultiplier;

        public override StatName StatNameSpecialEffectChanceMultiplier
            => StatName.WeaponConventionalSpecialEffectChanceMultiplier;

        protected override void PrepareProtoWeaponsSkillRanged(SkillConfig config)
        {
            var statNameDamageBonus = this.StatNameDamageBonusMultiplier;
            var statNameReloadingSpeed = this.StatNameReloadingSpeedMultiplier;
            var statNameDegradationRate = this.StatNameDegrationRateMultiplier;

            config.AddStatEffect(
                statNameDamageBonus,
                level: 10,
                percentBonus: 2);

            config.AddStatEffect(
                statNameDamageBonus,
                level: 20,
                percentBonus: 3);

            if (statNameReloadingSpeed.HasValue)
            {
                config.AddStatEffect(
                    statNameReloadingSpeed.Value,
                    formulaPercentBonus: level => -level * 2);
            }

            config.AddStatEffect(
                statNameDegradationRate,
                formulaPercentBonus: level => -level * 2);
        }
    }
}