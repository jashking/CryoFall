﻿namespace AtomicTorch.CBND.CoreMod.StaticObjects.Props.Road
{
    using AtomicTorch.CBND.GameApi.Data.World;

    public class ObjectPropRoadHorizontal2 : ProtoObjectPropRoad
    {
        protected override void CreateLayout(StaticObjectLayout layout)
        {
            layout.Setup("##",
                         "##");
        }
    }
}