using System.Collections.Generic;

public class Wearpons
{
    private List<IWearpon> _wearpons;

    public List<IWearpon> Weapons => _wearpons;

    public Wearpons()
    {
        _wearpons = new List<IWearpon>();

        _wearpons.Add(new Gun());
        _wearpons.Add(new Sword());
    }
}
