using System.Collections.Generic;

[System.Serializable]
public class Serialization<T>
{
    public List<T> target;

    public Serialization(List<T> _target) => target = _target;
}
