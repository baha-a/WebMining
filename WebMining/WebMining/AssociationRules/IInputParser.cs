using System.Collections.Generic;

namespace WebMining
{
    public interface IInputParser
    {
        IEnumerable<char> GetItems();

        IEnumerable<string> GetTransactions();

        int Count();
    }
}