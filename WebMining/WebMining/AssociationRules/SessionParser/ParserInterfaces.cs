using System.Collections.Generic;

namespace WebMining
{
    public interface ITransactionInputParser
    {
        IEnumerable<char> GetItems();

        IEnumerable<string> GetTransactions();

        int Count();
    }

    public interface ITransactionOutputParser
    {
        IEnumerable<string> ParseToLines(string transaction);

        string Parse(string transaction);

        string Delimiter { get; set; }
    }
}