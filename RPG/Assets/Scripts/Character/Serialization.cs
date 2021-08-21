using System.Collections.Generic;

[System.Serializable]
public class Serialization<T>
{
    public List<T> Target;
    public int     Gold;

    public Serialization(List<T> target, int gold)
    {
        Target = target;
        Gold   = gold;
    }

    public Serialization(List<T> target) : this(target, 0) { }
}
