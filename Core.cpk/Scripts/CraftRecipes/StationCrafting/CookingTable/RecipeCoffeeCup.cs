﻿namespace AtomicTorch.CBND.CoreMod.CraftRecipes
{
    using System;
    using AtomicTorch.CBND.CoreMod.Items.Food;
    using AtomicTorch.CBND.CoreMod.Items.Generic;
    using AtomicTorch.CBND.CoreMod.StaticObjects.Structures.CraftingStations;
    using AtomicTorch.CBND.CoreMod.Systems;
    using AtomicTorch.CBND.CoreMod.Systems.Crafting;

    public class RecipeCoffeeCup : Recipe.RecipeForStationCrafting
    {
        protected override void SetupRecipe(
            StationsList stations,
            out TimeSpan duration,
            InputItems inputItems,
            OutputItems outputItems)
        {
            stations.Add<ObjectCookingTable>();

            duration = CraftingDuration.VeryShort;

            inputItems.Add<ItemCoffeeBeans>(count: 10);
            inputItems.Add<ItemBottleWater>(count: 1);

            outputItems.Add<ItemCoffeeCup>(count: 1);
            outputItems.Add<ItemBottleEmpty>(count: 1);
        }
    }
}