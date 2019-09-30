using System;
using System.Collections.Generic;
using System.Text;

namespace UlearnExperiments
{
    class Program
    {
	    static void Main()
	    {
		    string[] numbers = new String[] {"push text", "push vs9kyu hrenka", "push vs9kyu hren",  "pop 26", "pop 5"};


		    var code = new StringBuilder();


		    foreach (var command in numbers)
		    {
			    if (command.StartsWith("push"))
			    {
				    code.Append(command.Remove(0, 5));
			    }
			    else
			    {
				    var a = int.Parse(command.Remove(0, 4));
				    code.Remove(code.Length - a, a);
			    }
		    }
			Console.Write(code);
		    //return code.ToString();
	    }








		 //   foreach (string i in numbers)
		 //   {
			//    Console.WriteLine(i);
		 //   }
	  //  }

	  //  private static string ApplyCommands(string[] commands)
	  //  {
		 //   var code = new StringBuilder();
		 //   string[] numbers = new String[] { "push text", "push vs9kyu hrenka", "push vs9kyu hren", "pop 26", "pop 5" };



			//foreach (var command in commands)
		 //   {
			//    if (command.StartsWith("push"))
			//    {
			//	    code.Append(command.Remove(0, 5));
			//    }
			//    else
			//    {
			//	    var a = int.Parse(command.Remove(0, 4));
			//			code.Remove(code.Length, a);
			//    }
			//}
		 //   return code.ToString();
	  //  }

	}
}
