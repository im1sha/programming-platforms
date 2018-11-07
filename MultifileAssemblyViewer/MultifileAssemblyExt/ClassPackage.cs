using System.Reflection;
using System.Runtime;

namespace ClassPackage
{
	public class A0
    {
    }

    [ExportDll.ExportClassAttribute("A.B.C.",Version = 1.0)]
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

    [MultiDll.MultiAttribute("A")]
    [MultiDll.MultiAttribute("B")]
    [MultiDll.MultiAttribute("X")]
    [ExportDll.ExportClassAttribute("NN", Version = 1.2)]
    class AssemblyViewer
    {
		public Assembly GetCallingAssembly()
		{
			return System.Reflection.Assembly.GetCallingAssembly();
		}
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
