using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderDB.Models
{
    public class BaseResponseModel
    {
        public BaseResponseModel()
        {
            ErrorList = new List<ErrorListModel>();
            Message = "";
        }

        public string Message { get; set; }

        public List<ErrorListModel> ErrorList { get; set; }
    }

    public class ErrorListModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }

    public class GenericResponseModel<TResult> : BaseResponseModel
    {
        public GenericResponseModel()
        {
            Type t = typeof(TResult);
            if (t.GetConstructor(Type.EmptyTypes) != null)
                Data = Activator.CreateInstance<TResult>();
        }

        public TResult Data { get; set; }

    }
}
