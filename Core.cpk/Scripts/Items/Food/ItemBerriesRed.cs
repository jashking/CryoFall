﻿namespace AtomicTorch.CBND.CoreMod.Items.Food
{
    using System;
    using AtomicTorch.CBND.CoreMod.SoundPresets;

    public class ItemBerriesRed : ProtoItemFood
    {
        public override string Description => "Tasty red berries.";

        public override float FoodRestore => 2;

        public override TimeSpan FreshnessDuration => ExpirationDuration.Perishable;

        public override string Name => "Red berries";

        public override ushort OrganicValue => 3;

        public override float WaterRestore => 3;

        protected override ReadOnlySoundPreset<ItemSound> PrepareSoundPresetItem()
        {
            return ItemsSoundPresets.ItemFoodFruit;
        }
    }
}