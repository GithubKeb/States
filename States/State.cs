using System.Runtime.InteropServices;
using OneOf.Types;

namespace Stateful;

public interface IReadOnlyState<T>
{
    T Value { get; }
}

public interface IStateSetter<T> : IReadOnlyState<T>
{
    OneOf.OneOf<Success, AlreadySet> Set(T value);
}

public class State<T> : IStateSetter<T>
{
    private bool initialized;
    public T Value { get; private set; }

    public OneOf.OneOf<Success, AlreadySet> Set(T value)
    {
        if (initialized)
        {
            throw new Exception("Use modifiable");
        }

        Value =  value;
        initialized = true;
        return new Success();
    }
}

public class ModifiableState<T> : IStateSetter<T>
{
    public T Value { get; private set; }

    public OneOf.OneOf<Success, AlreadySet> Set(T value)
    {
        Value =  value;
        return new Success();
    }
}

[StructLayout(LayoutKind.Sequential, Size = 1)]
public struct AlreadySet
{
}