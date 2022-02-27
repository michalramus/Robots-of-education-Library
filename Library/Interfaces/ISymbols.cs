namespace ROELibrary
{
    interface ISymbols<T>
    {
        string getValue(T key);
        T getKey(string value);
    }
}
