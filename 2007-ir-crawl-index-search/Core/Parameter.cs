using System;

namespace IrProject.Core
{
    /// <summary>
    /// Stores a parameter's value; used to pass parameters and modify their values
    /// </summary>
    public class Parameter
    {
        public Parameter(string dataField, object val)
        {
            this.dataField = dataField;
            this.Value = val;
        }

        public string DataField { get { return dataField; } }

        public object Value;

        private string dataField;
    }
}
