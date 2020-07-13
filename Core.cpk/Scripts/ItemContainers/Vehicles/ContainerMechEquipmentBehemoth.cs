﻿namespace AtomicTorch.CBND.CoreMod.ItemContainers.Vehicles
{
    using AtomicTorch.CBND.CoreMod.Vehicles;

    public class ContainerMechEquipmentBehemoth : BaseItemsContainerMechEquipment
    {
        public override byte AmmoSlotsCount => 3;

        public override VehicleWeaponHardpoint WeaponHardpointName => VehicleWeaponHardpoint.Large;

        public override byte WeaponSlotsCount => 1;
    }
}