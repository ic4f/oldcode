using System.Collections;

namespace WebControls
{
    public interface IDataTable : IEnumerable
    {
        int TotalRowCount { get; }

        int RowCount { get; }

        int FieldCount { get; }

        ArrayList Rows { get; }
    }
}
