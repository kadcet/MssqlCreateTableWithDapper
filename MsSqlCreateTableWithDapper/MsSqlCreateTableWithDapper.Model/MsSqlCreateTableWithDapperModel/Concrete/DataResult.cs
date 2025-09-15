using MsSqlCreateTableWithDapper.Model.MsSqlCreateTableWithDapperModel.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Model.MsSqlCreateTableWithDapperModel.Concrete
{
    public class DataResult<T> : Result, IDataResult<T>
    {

        protected DataResult(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }

        protected DataResult(T data, bool success) : base(success)
        {
            Data = data;
        }

        public T Data { get; }

    }
}
