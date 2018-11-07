using ClassPackage;

namespace EntryPoint
{
    public class App
    {
		public static void Main(string[]args)
		{
			var sum = ClassPackage.Math.Sum(5, 10, 15, 20);
			ClassPackage.AssemblyViewer v = new ClassPackage.AssemblyViewer();
								
			System.Console.WriteLine(v.GetCallingAssembly().ToString());
			System.Console.WriteLine("Sum is {0}\n", sum);
			System.Console.ReadLine();
			
			
		}
    }
}
