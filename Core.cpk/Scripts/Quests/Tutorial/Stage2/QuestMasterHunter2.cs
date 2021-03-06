﻿namespace AtomicTorch.CBND.CoreMod.Quests.Tutorial
{
    using System.Collections.Generic;
    using AtomicTorch.CBND.CoreMod.Characters.Mobs;
    using AtomicTorch.CBND.CoreMod.Items.Devices;
    using AtomicTorch.CBND.CoreMod.PlayerTasks;

    public class QuestMasterHunter2 : ProtoQuest
    {
        public override string Description =>
            "Time for some more hunting! There are still more animals and monsters to find.";

        public override string Hints =>
            @"[*] You can equip your hunter tools in the ""devices slot"" (to the right of your character portrait in the inventory menu).
              [*] Each animal and monster has their own area where they can normally be found. Try to explore the world and see all the different creatures that inhabit it.
              [*] Depending on how dangerous a particular animal or monster is, you will get different amounts of hunting experience.";

        public override string Name => "Master hunter—part two";

        public override ushort RewardLearningPoints => QuestConstants.TutorialRewardStage2;

        protected override void PrepareQuest(QuestsList prerequisites, TasksList tasks, HintsList hints)
        {
            tasks
                .Add(TaskHaveItemEquipped.Require<ItemHuntersTools>())
                .Add(TaskKill.Require<MobPangolin>(count: 1))
                .Add(TaskKill.Require<MobTropicalBoar>(count: 1))
                .Add(TaskKill.Require<MobTurtle>(count: 1))
                .Add(TaskKill.Require<MobSnakeGreen>(count: 1));

            prerequisites
                .Add<QuestMasterHunter1>();
        }
    }
}