
using ICaNet.GenerateKey;

var gnSK = new GenerateKey();

string strongKey = gnSK.GenerateStrongKey();
Console.WriteLine(strongKey);