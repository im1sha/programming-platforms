using ClassPackage;

namespace EntryPoint
{
    public class App
    {
		public static void Main(string[]args)
		{
			ClassPackage.Math.Sum(5, 10, 15, 20);
			ClassPackage.AssemblyViewer v = new ClassPackage.AssemblyViewer();
			Console.Write(v.GetCallingAssembly().ToString());
		}
    }
}
