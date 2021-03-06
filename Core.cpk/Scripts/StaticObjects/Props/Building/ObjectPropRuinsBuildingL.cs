﻿namespace AtomicTorch.CBND.CoreMod.StaticObjects.Props.Building
{
    using AtomicTorch.CBND.GameApi.ServicesClient.Components;

    public class ObjectPropRuinsBuildingL : ProtoObjectProp
    {
        protected override void ClientSetupRenderer(IComponentSpriteRenderer renderer)
        {
            base.ClientSetupRenderer(renderer);
            renderer.DrawOrder = DrawOrder.OverDefault;
        }

        protected override void SharedCreatePhysics(CreatePhysicsData data)
        {
            AddRectangleWithHitboxes(data, size: (0.9, 1), offset: (0.1, 0));
        }
    }
}