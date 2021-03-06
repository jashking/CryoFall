﻿namespace AtomicTorch.CBND.CoreMod.Items.Equipment
{
    using AtomicTorch.CBND.CoreMod.SoundPresets;

    public class ItemWoodArmor : ProtoItemEquipmentArmor
    {
        public override string Description =>
            "Necessity is the mother of invention. Not the most comfy, but quite decent protection early on.";

        public override uint DurabilityMax => 500;

        public override ObjectMaterial Material => ObjectMaterial.HardTissues;

        public override string Name => "Wooden armor";

        protected override void PrepareDefense(DefenseDescription defense)
        {
            defense.Set(
                impact: 0.40,
                kinetic: 0.40,
                explosion: 0.30,
                heat: 0.15,
                cold: 0.10,
                chemical: 0.15,
                radiation: 0.10,
                psi: 0);
        }
    }
}