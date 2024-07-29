namespace Infentory
{
    public readonly struct AddItemsToInfentoryResult
    {
        public readonly int ItemsToAddAmount;
        public readonly int ItemsAddedAmount;

        public int ItemsNotAddedAmount => ItemsToAddAmount - ItemsAddedAmount;

        public AddItemsToInfentoryResult(int itemsToAddAmount, int itemsAddedAmount)
        {
            ItemsToAddAmount = itemsToAddAmount;
            ItemsAddedAmount = itemsAddedAmount;
        }
    }
}
