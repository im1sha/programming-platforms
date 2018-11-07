namespace DllCreator
{
    public class A0
    {
    }

    [ExportDll.ExportClassAttribute("A.B.C.",Version = 1.0)]
    public class A
    {
    }

    [MultiDll.MultiAttribute("A")]
    [MultiDll.MultiAttribute("B")]
    [MultiDll.MultiAttribute("X")]
    [ExportDll.ExportClassAttribute("NN", Version = 1.2)]
    class B
    {
    }

    [MultiDll.MultiAttribute("C")]
    [ExportDll.ExportClassAttribute("NN2", Version = 1.22)]
    public class C
    {
    }

    [MultiDll.MultiAttribute("D")]
    public class D
    {
    }

    public class E
    {
    }
}
