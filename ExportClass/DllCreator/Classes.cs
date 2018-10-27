namespace DllCreator
{
    [ExportClass.ExportClassAttribute("A.B.C.",Version = 1.0)]
    public class A
    {
    }

    [ExportClass.ExportClassAttribute("NN", Version = 1.2)]
    class B
    {
    }

    [ExportClass.ExportClassAttribute("NN2", Version = 1.22)]
    public class C
    {
    }

    public class D
    {
    }
}
