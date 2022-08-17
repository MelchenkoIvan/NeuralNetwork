// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var a = new List<string>();
a.Add("a");
a.Add("b");
a.Add("c");
var b = new List<string>();
b.Add("a");
b.Add("b");
b.Add("d");

UsunZdublowane(a, b);


void UsunZdublowane(List<string> l1, List<string> l2)
{
    var a = l1.RemoveAll(x => l2.Contains(x));

}
