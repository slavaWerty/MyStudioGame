using System;

namespace Infentory.ReadOnly
{
    public interface IReadOnlyInfentory
    {
        public string OwnerID { get; }
        int GetAmount(string itemID);
        bool Has(string itemID, int amount);
    }
}
