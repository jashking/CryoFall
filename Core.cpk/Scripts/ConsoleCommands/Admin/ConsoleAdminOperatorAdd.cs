﻿// ReSharper disable CanExtractXamlLocalizableStringCSharp

namespace AtomicTorch.CBND.CoreMod.ConsoleCommands.Admin
{
    using AtomicTorch.CBND.CoreMod.Systems.Console;
    using AtomicTorch.CBND.CoreMod.Systems.ServerOperator;
    using AtomicTorch.CBND.GameApi.Data.Characters;

    public class ConsoleAdminOperatorAdd : BaseConsoleCommand
    {
        public override string Alias => "opAdd";

        public override string Description
            => "Adds server operator.";

        public override ConsoleCommandKinds Kind => ConsoleCommandKinds.ServerOperator;

        public override string Name => "admin.operator.add";

        public string Execute([CurrentCharacterIfNull] ICharacter character)
        {
            if (ServerOperatorSystem.ServerIsOperator(character.Name))
            {
                return character.Name + " is already a server operator";
            }

            ServerOperatorSystem.ServerAdd(character);
            return character.Name + " added to the server operators list";
        }
    }
}