﻿namespace AtomicTorch.CBND.CoreMod.UI.Controls.Game.Map
{
    using System.Windows.Controls;
    using AtomicTorch.CBND.CoreMod.Characters;
    using AtomicTorch.CBND.CoreMod.UI.Controls.Game.Map.Data;
    using AtomicTorch.CBND.GameApi.Data.State;
    using AtomicTorch.CBND.GameApi.Scripting;

    public class ClientWorldMapBedVisualizer : IWorldMapVisualizer
    {
        private readonly PlayerCharacterPrivateState playerCharacterPrivateState;

        private readonly IStateSubscriptionOwner stateSubscriptionOwner;

        private readonly WorldMapController worldMapController;

        private WorldMapMarkCurrentBed currentBedMark;

        public ClientWorldMapBedVisualizer(WorldMapController worldMapController)
        {
            this.stateSubscriptionOwner = new StateSubscriptionStorage();
            this.worldMapController = worldMapController;
            this.playerCharacterPrivateState = Api.Client.Characters.CurrentPlayerCharacter
                                                  .GetPrivateState<PlayerCharacterPrivateState>();

            this.playerCharacterPrivateState.ClientSubscribe(
                _ => _.CurrentBedObjectPosition,
                _ => this.Refresh(),
                this.stateSubscriptionOwner);

            this.Refresh();
        }

        public bool IsEnabled { get; set; }

        public void Dispose()
        {
            this.stateSubscriptionOwner.ReleaseSubscriptions();
            this.DestroyControl();
        }

        private void DestroyControl()
        {
            if (this.currentBedMark is null)
            {
                return;
            }

            this.worldMapController.RemoveControl(this.currentBedMark);
            this.currentBedMark = null;
        }

        private void Refresh()
        {
            var bedWorldPosition = this.playerCharacterPrivateState.CurrentBedObjectPosition;
            if (!bedWorldPosition.HasValue)
            {
                this.DestroyControl();
                return;
            }

            if (this.currentBedMark is null)
            {
                this.currentBedMark = new WorldMapMarkCurrentBed();
                Panel.SetZIndex(this.currentBedMark, 9);
                this.worldMapController.AddControl(this.currentBedMark);
            }

            var canvasPosition = this.worldMapController.WorldToCanvasPosition(
                bedWorldPosition.Value.ToVector2D());

            Canvas.SetLeft(this.currentBedMark, canvasPosition.X);
            Canvas.SetTop(this.currentBedMark, canvasPosition.Y);
        }
    }
}