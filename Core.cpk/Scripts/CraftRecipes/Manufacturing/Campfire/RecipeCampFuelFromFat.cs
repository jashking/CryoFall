﻿namespace AtomicTorch.CBND.CoreMod.CraftRecipes
{
    using System;
    using AtomicTorch.CBND.CoreMod.Items;
    using AtomicTorch.CBND.CoreMod.Items.Generic;
    using AtomicTorch.CBND.CoreMod.StaticObjects.Structures.Manufacturers;
    using AtomicTorch.CBND.CoreMod.Systems;
    using AtomicTorch.CBND.CoreMod.Systems.Crafting;

    public class RecipeCampFuelFromFat : Recipe.RecipeForManufacturing
    {
        protected override void SetupRecipe(
            StationsList stations,
            out TimeSpan duration,
            InputItems inputItems,
            OutputItems outputItems)
        {
            stations.Add<ObjectCampfire>();
            stations.Add<ObjectStove>();
            stations.Add<ObjectStoveElectric>();

            duration = CraftingDuration.Medium;

            inputItems.Add<ItemAnimalFat>(count: 2);

            outputItems.Add<ItemCampFuel>();

            this.Icon = ClientItemIconHelper.CreateComposedIcon(
                name: this.Id + "Icon",
                primaryIcon: GetItem<ItemCampFuel>().Icon,
                secondaryIcon: GetItem<ItemAnimalFat>().Icon);
        }
    }
}