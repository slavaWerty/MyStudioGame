namespace Infentory
{
    public readonly struct RemoveToInfentoryResult
    {
        public readonly int ItemsToRemoveAmount;
        public readonly bool Success;
        public readonly PlayerBuffs PlayerBuffs;

        public RemoveToInfentoryResult(int itemsToRemoveAmount, bool success, PlayerBuffs playerBuffs)
        {
            ItemsToRemoveAmount = itemsToRemoveAmount;
            Success = success;
            PlayerBuffs = playerBuffs;
        }
    }
}
