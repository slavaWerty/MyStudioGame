namespace Infentory
{
    public readonly struct AddItemsToInfentoryResult
    {
        public readonly int ItemsToAddAmount;
        public readonly int ItemsAddedAmount;
        public readonly PlayerBuffs PlayerBuffs;

        public int ItemsNotAddedAmount => ItemsToAddAmount - ItemsAddedAmount;

        public AddItemsToInfentoryResult(int itemsToAddAmount, int itemsAddedAmount, PlayerBuffs playerBuffs)
        {
            ItemsToAddAmount = itemsToAddAmount;
            ItemsAddedAmount = itemsAddedAmount;
            PlayerBuffs = playerBuffs;
        }
    }
}
