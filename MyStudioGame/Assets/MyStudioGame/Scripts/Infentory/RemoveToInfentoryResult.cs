namespace Infentory
{
    public readonly struct RemoveToInfentoryResult
    {
        public readonly int ItemsToRemoveAmount;
        public readonly bool Success;

        public RemoveToInfentoryResult(int itemsToRemoveAmount, bool success)
        {
            ItemsToRemoveAmount = itemsToRemoveAmount;
            Success = success;
        }
    }
}
