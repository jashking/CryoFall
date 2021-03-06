﻿namespace AtomicTorch.CBND.CoreMod.Systems.CharacterDamageTrackingSystem
{
    using AtomicTorch.CBND.GameApi;
    using AtomicTorch.CBND.GameApi.Data;
    using AtomicTorch.CBND.GameApi.Scripting.Network;

    [NotPersistent]
    public readonly struct DamageSourceRemoteEntry : IRemoteCallParameter
    {
        public readonly float Fraction;

        public readonly string Name;

        public readonly IProtoEntity ProtoEntity;

        public DamageSourceRemoteEntry(IProtoEntity protoEntity, string name, float fraction)
        {
            this.ProtoEntity = protoEntity;
            this.Name = name;
            this.Fraction = fraction;
        }
    }
}