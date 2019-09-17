using System;

/// <summary>
///     Sip header
/// </summary>
/// <typeparam name="T">Sip header type</typeparam>
public class SipHeader<T>
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

    public static SipHeader<T> CreateHeader(Func<T> translator)
    {
        T value = translator();
        return new SipHeader<T>("", value);
    } 
}