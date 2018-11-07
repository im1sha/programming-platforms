using System.Reflection;

namespace ClassPackage
{
    public class A0
    {
    }

    [ExportClass.ExportClassAttribute("A.B.C.",Version = 1.0)]
    public static class Math
    {
		public static int Sum(params int[] args)
		{
			int sum = 0;
			
			foreach (var a in args) 
			{
				sum += a;
			}
			
			return sum;
		}
    }

    [ExportClass.MultiAttribute("A")]
    [ExportClass.MultiAttribute("B")]
    [ExportClass.MultiAttribute("X")]
    [ExportClass.ExportClassAttribute("NN", Version = 1.2)]
    class AssemblyViewer
    {
		public Assembly GetCallingAssembly()
		{
			return System.Reflection.Assembly.GetCallingAssembly();
		}
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
