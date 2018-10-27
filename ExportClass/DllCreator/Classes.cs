namespace DllCreator
{
    public class A0
    {
    }

    [ExportClass.ExportClassAttribute("A.B.C.",Version = 1.0)]
    public class A
    {
    }

    [ExportClass.MultiAttribute("A")]
    [ExportClass.MultiAttribute("B")]
    [ExportClass.MultiAttribute("X")]
    [ExportClass.ExportClassAttribute("NN", Version = 1.2)]
    class B
    {
    }

    [ExportClass.MultiAttribute("C")]
    [ExportClass.ExportClassAttribute("NN2", Version = 1.22)]
    public class C
    {
    }

    [ExportClass.MultiAttribute("D")]
    public class D
    {
    }

    public class E
    {
    }
}
