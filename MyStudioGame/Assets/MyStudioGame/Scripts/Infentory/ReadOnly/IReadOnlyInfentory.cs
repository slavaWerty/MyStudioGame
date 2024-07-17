using System;

namespace Infentory.ReadOnly
{
    public interface IReadOnlyInfentory
    {
        int GetAmount(string itemID);
        bool Has(string itemID, int amount);
    }
}
