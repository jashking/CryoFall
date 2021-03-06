﻿namespace AtomicTorch.CBND.CoreMod.Items.Food
{
    using System;

    public class ItemCannedMeat : ProtoItemFood
    {
        public override string Description =>
            "Canned stewed meat with high concentration of fat. Stores for a long time.";

        public override float FoodRestore => 25;

        public override TimeSpan FreshnessDuration => ExpirationDuration.Unlimited;

        public override string Name => "Canned meat";

        public override ushort OrganicValue => 0;
    }
}