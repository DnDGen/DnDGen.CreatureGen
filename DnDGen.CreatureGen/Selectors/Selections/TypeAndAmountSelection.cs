﻿namespace DnDGen.CreatureGen.Selectors.Selections
{
    internal class TypeAndAmountSelection
    {
        public const char Divider = '@';

        public string Type { get; set; }
        public string RawAmount { get; set; }
        public int Amount { get; set; }

        public TypeAndAmountSelection()
        {
            Type = string.Empty;
            RawAmount = string.Empty;
        }
    }
}
