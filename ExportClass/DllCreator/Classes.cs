namespace DllCreator
{
    [ExportClass.ExportClass("A.B.C.",Version = 1.0)]
    public class A
    {
    }

    [ExportClass.ExportClass("NN", Version = 1.2)]
    class B
    {
    }

    [ExportClass.ExportClass("NN2", Version = 1.22)]
    public class C
    {
    }

    public class D
    {
    }
}
