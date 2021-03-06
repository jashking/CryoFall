﻿namespace AtomicTorch.CBND.CoreMod.StaticObjects.Structures.Manufacturers
{
    using AtomicTorch.CBND.CoreMod.Items.Generic;
    using AtomicTorch.CBND.CoreMod.SoundPresets;
    using AtomicTorch.CBND.CoreMod.Systems.Construction;
    using AtomicTorch.CBND.CoreMod.Systems.Physics;
    using AtomicTorch.CBND.GameApi.Data.State;
    using AtomicTorch.CBND.GameApi.Data.World;
    using AtomicTorch.CBND.GameApi.Resources;
    using AtomicTorch.CBND.GameApi.Scripting;
    using AtomicTorch.CBND.GameApi.ServicesClient.Components;

    public class ObjectOilPump : ProtoObjectOilPump
    {
        private readonly TextureAtlasResource textureAtlasOilPumpActive;

        public ObjectOilPump()
        {
            this.textureAtlasOilPumpActive = new TextureAtlasResource(
                this.GenerateTexturePath() + "Active",
                columns: 4,
                rows: 2,
                isTransparent: true);
        }

        public override byte ContainerFuelSlotsCount => 1;

        public override byte ContainerInputSlotsCountExtractorWithDeposit => 1;

        public override byte ContainerOutputSlotsCountExtractorWithDeposit => 1;

        public override string Description =>
            "Allows extraction of petroleum oil from underground reservoir or directly from an oil seep for increased efficiency.";

        public override double LiquidCapacity => 10;

        public override double LiquidProductionAmountPerSecond => 0.05;

        public override string Name => "Oil pump";

        public override ObjectMaterial ObjectMaterial => ObjectMaterial.Metal;

        public override double ObstacleBlockDamageCoef => 1;

        public override float StructurePointsMax => 5000;

        protected override void ClientInitialize(ClientInitializeData data)
        {
            base.ClientInitialize(data);
            var worldObject = data.GameObject;
            var publicState = data.PublicState;

            var animatedSpritePositionOffset = (84 / (double)ScriptingConstants.TileSizeRealPixels,
                                                122 / (double)ScriptingConstants.TileSizeRealPixels);

            this.ClientSetupExtractorActiveAnimation(
                worldObject,
                publicState,
                this.textureAtlasOilPumpActive,
                animatedSpritePositionOffset,
                frameDurationSeconds: 0.08,
                autoInverseAnimation: false,
                playAnimationSounds: false);

            var soundEmitter = this.ClientCreateActiveStateSoundEmitterComponent(worldObject);

            publicState.ClientSubscribe(_ => _.IsActive,
                                        _ => RefreshActiveState(),
                                        data.ClientState);

            RefreshActiveState();

            void RefreshActiveState()
            {
                soundEmitter.IsEnabled = publicState.IsActive;
            }
        }

        protected override void ClientSetupRenderer(IComponentSpriteRenderer renderer)
        {
            base.ClientSetupRenderer(renderer);
            renderer.DrawOrderOffsetY = 1.8;
        }

        protected override void CreateLayout(StaticObjectLayout layout)
        {
            layout.Setup("###",
                         "###",
                         "###");
        }

        protected override void PrepareConstructionConfig(
            ConstructionStageConfig build,
            ConstructionStageConfig repair,
            out ProtoStructureCategory category)
        {
            category = GetCategory<StructureCategoryIndustry>();

            build.StagesCount = 10;
            build.StageDurationSeconds = BuildDuration.Medium;
            build.AddStageRequiredItem<ItemPlanks>(count: 10);
            build.AddStageRequiredItem<ItemIngotIron>(count: 1);
            build.AddStageRequiredItem<ItemCement>(count: 2);

            repair.StagesCount = 10;
            repair.StageDurationSeconds = BuildDuration.Medium;
            repair.AddStageRequiredItem<ItemPlanks>(count: 10);
            repair.AddStageRequiredItem<ItemIngotIron>(count: 1);
        }

        protected override void PrepareDefense(DefenseDescription defense)
        {
            defense.Set(ObjectDefensePresets.Tier1);
        }

        protected override ReadOnlySoundPreset<ObjectSound> PrepareSoundPresetObject()
        {
            // use the oil pump active sound
            return base.PrepareSoundPresetObject().Clone().Replace(
                ObjectSound.Active,
                "Objects/Structures/" + nameof(ObjectOilPump) + "/Active");
        }

        protected override void SharedCreatePhysics(CreatePhysicsData data)
        {
            data.PhysicsBody
                .AddShapeRectangle((3.0, 2.2), (0, 0))
                .AddShapeRectangle((2.8, 1.7), (0.1, 0.7), CollisionGroups.HitboxMelee)
                .AddShapeRectangle((2.8, 1.6), (0.1, 0.8), CollisionGroups.HitboxRanged)
                .AddShapeRectangle((2.8, 2.8), (0.1, 0),   CollisionGroups.ClickArea);
        }
    }
}