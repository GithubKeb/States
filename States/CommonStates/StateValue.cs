namespace Stateful.CommonStates;

public abstract class StateValue<T>
{
    public T Value { get; }

    protected StateValue(T value)
    {
        Value = value;
    }
    
    public static implicit operator T(StateValue<T> stateValue)
    {
        return stateValue.Value;
    }
}