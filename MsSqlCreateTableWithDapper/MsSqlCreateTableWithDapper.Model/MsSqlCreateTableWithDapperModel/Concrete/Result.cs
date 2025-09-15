using MsSqlCreateTableWithDapper.Model.MsSqlCreateTableWithDapperModel.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlCreateTableWithDapper.Model.MsSqlCreateTableWithDapperModel.Concrete
{
    public class Result : IResult
    {
        protected Result(bool success, string message) : this(success)
        {
            Message = message;
        }


        protected Result(bool success)
        {
            Success = success;
        }
        public bool Success { get; }
        public string Message { get; }

    }
}
