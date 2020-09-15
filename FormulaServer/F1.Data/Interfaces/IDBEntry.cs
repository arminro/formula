using System;

namespace F1.Data.Interfaces
{
    /// <summary>
    /// An interface making the application interoperable with multiple data types. 
    /// </summary>
    /// This is a kind-of hack questionable to be used in a real-life scenario, but this is a demo app.
    public interface IDbEntry
    {
        Guid Id { get; set; }
    }
}
