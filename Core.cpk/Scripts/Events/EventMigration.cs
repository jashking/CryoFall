﻿namespace AtomicTorch.CBND.CoreMod.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AtomicTorch.CBND.CoreMod.Characters.Mobs;
    using AtomicTorch.CBND.CoreMod.Events.Base;
    using AtomicTorch.CBND.CoreMod.Triggers;
    using AtomicTorch.CBND.CoreMod.Zones;
    using AtomicTorch.CBND.GameApi;
    using AtomicTorch.CBND.GameApi.Data.Logic;
    using AtomicTorch.CBND.GameApi.Data.World;
    using AtomicTorch.CBND.GameApi.Data.Zones;
    using AtomicTorch.CBND.GameApi.Scripting;
    using AtomicTorch.GameEngine.Common.Extensions;
    using AtomicTorch.GameEngine.Common.Helpers;
    using AtomicTorch.GameEngine.Common.Primitives;

    public class EventMigration : ProtoEventDrop
    {
        private static readonly Lazy<IReadOnlyList<IServerZone>> ServerSpawnZones
            = new Lazy<IReadOnlyList<IServerZone>>(ServerSetupSpawnZones);

        public override ushort AreaRadius => 90;

        public override string Description =>
            "Native lifeforms of this world seem to be migrating to a different area, giving a great opportunity for hunting.";

        public override TimeSpan EventDuration => TimeSpan.FromMinutes(30);

        public override double MinDistanceBetweenSpawnedObjects => 11;

        [NotLocalizable]
        public override string Name => "Migration";

        public override bool ServerIsTriggerAllowed(ProtoTrigger trigger)
        {
            if (ServerSpawnZones.Value.All(z => z.IsEmpty))
            {
                Logger.Error("All zones are empty (not mapped in the world), no place to start the event: " + this);
                return false;
            }

            if (trigger is TriggerTimeInterval
                && Server.Game.HoursSinceWorldCreation < 24)
            {
                // too early
                return false;
            }

            return true;
        }

        protected override bool ServerIsValidSpawnPosition(Vector2Ushort spawnPosition)
        {
            foreach (var serverZone in ServerSpawnZones.Value)
            {
                if (serverZone.IsContainsPosition(spawnPosition))
                {
                    return true;
                }
            }

            return false;
        }

        protected override Vector2Ushort ServerPickEventPosition(ILogicObject activeEvent)
        {
            var zoneInstance = ServerSpawnZones.Value.TakeByRandom();

            var attempts = 1000;
            do
            {
                var result = zoneInstance.GetRandomPosition(RandomHelper.Instance);
                if (this.ServerIsValidEventPosition(result))
                {
                    return result;
                }
            }
            while (--attempts > 0);

            throw new Exception("Unable to pick an event position");
        }

        protected override void ServerPrepareDropEvent(Triggers triggers, List<IProtoWorldObject> spawnPreset)
        {
            triggers
                // trigger on time interval
                .Add(GetTrigger<TriggerTimeInterval>()
                         .Configure(
                             intervalFrom: TimeSpan.FromHours(4),
                             intervalTo: TimeSpan.FromHours(8)));

            for (var index = 0; index < 12; index++)
            {
                spawnPreset.Add(Api.GetProtoEntity<MobFloater>());
            }
        }

        private static IReadOnlyList<IServerZone> ServerSetupSpawnZones()
        {
            var result = new List<IServerZone>();
            AddZone(Api.GetProtoEntity<ZoneTemperateForest>());
            AddZone(Api.GetProtoEntity<ZoneBorealForest>());

            void AddZone(IProtoZone zone)
            {
                var instance = zone.ServerZoneInstance;
                result.Add(instance);
            }

            return result;
        }
    }
}