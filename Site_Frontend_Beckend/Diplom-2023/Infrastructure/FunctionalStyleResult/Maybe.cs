namespace Infrastructure.FunctionalStyleResult;

/// <summary> 
/// Maybe monad. 
/// https://enterprisecraftsmanship.com/2015/03/13/functional-c-non-nullable-reference-types/ 
/// TODO: use nuget package https://www.nuget.org/packages/CSharpFunctionalExtensions/ 
/// </summary> 
[Serializable]
public struct Maybe<T> : IEquatable<Maybe<T>>
{
    private readonly MaybeValueWrapper _value;

    public T Value
    {
        get
        {
            if (HasNoValue)
                throw new InvalidOperationException();

            return _value.Value;
        }
    }

    public static Maybe<T> Empty => new Maybe<T>();

    public bool HasValue => _value != null;

    public bool HasNoValue => !HasValue;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Maybe(T value)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
#pragma warning disable CS8601 // Possible null reference assignment.
        _value = value == null ? null : new MaybeValueWrapper(value);
#pragma warning restore CS8601 // Possible null reference assignment.
    }

    public static implicit operator Maybe<T>(T value)
    {
        if (value?.GetType() == typeof(Maybe<T>))
        {
            return (Maybe<T>)(object)value;
        }

        return new Maybe<T>(value);
    }

    public static bool operator ==(Maybe<T> maybe, T value)
    {
        if (value is Maybe<T>)
            return maybe.Equals(value);

        if (maybe.HasNoValue)
            return false;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return maybe.Value.Equals(value);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    public static bool operator !=(Maybe<T> maybe, T value)
    {
        return !(maybe == value);
    }

    public static bool operator ==(Maybe<T> first, Maybe<T> second)
    {
        return first.Equals(second);
    }

    public static bool operator !=(Maybe<T> first, Maybe<T> second)
    {
        return !(first == second);
    }

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public override bool Equals(object obj)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    {
        if (obj == null)
            return false;

        if (obj.GetType() != typeof(Maybe<T>))
        {
            if (obj is T)
            {
                obj = new Maybe<T>((T)obj);
            }

            if (!(obj is Maybe<T>))
                return false;
        }

        var other = (Maybe<T>)obj;
        return Equals(other);
    }

    public bool Equals(Maybe<T> other)
    {
        if (HasNoValue && other.HasNoValue)
            return true;

        if (HasNoValue || other.HasNoValue)
            return false;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return _value.Value.Equals(other._value.Value);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    public override int GetHashCode()
    {
        if (HasNoValue)
            return 0;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return _value.Value.GetHashCode();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    public override string ToString()
    {
        if (HasNoValue)
            return "No value";

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.
        return Value.ToString();
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    [Serializable]
    private class MaybeValueWrapper
    {
        public MaybeValueWrapper(T value)
        {
            Value = value;
        }

        internal readonly T Value;
    }
}