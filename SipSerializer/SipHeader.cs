using System;


public interface ISipHeader : ISipHeader<string>
{
}

public interface ISipHeader<T>
{
    string Name { get; }
    T Value { get; }
}

public class SipHeader : ISipHeader
{
    /// <summary>
    ///     Header name
    /// </summary>
    public string Name { get; private set; }   

    public string Value { get; private set; }
}

/// <summary>
///     Sip header
/// </summary>
/// <typeparam name="T">Sip header type</typeparam>
public class SipHeader<T> : ISipHeader<T>
{
    /// <summary>
    ///     Header name
    /// </summary>
    public string Name { get; private set; } 

    /// <summary>
    ///     Header value
    /// </summary>
    public T Value { get; private set; }

    public SipHeader(string name, T value)
    {
        Name = name;
        Value = value;
    }

    /// <summary>
    ///     Convert header into the string
    /// </summary>
    /// <returns>String in the ascii format</returns>
    public override string ToString()
    {
        return $"{Name}={Value.ToString()};";
    }

    public static SipHeader<T> CreateHeader(ISipHeader sipHeader, Func<T> translator)
    {
        return CreateHeader(sipHeader.Name, translator);
    }

    public static SipHeader<T> CreateHeader(string sipHeaderName, Func<T> translator)
    {
        return new SipHeader<T>(sipHeaderName, translator());
    }
}