using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

public class IDGenerator : ValueGenerator<int>
{
    private int _current;

    public override bool GeneratesTemporaryValues => false;

    public override int Next(EntityEntry entry)
        => Interlocked.Increment(ref _current);
}