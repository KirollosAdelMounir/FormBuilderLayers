using FormBuilderServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderServiceLayer.UnitOfServices
{
    public interface IUnitOfServices
    {
        FormDataService FormDataService { get; }
        FormFieldResultService FormFieldResultService { get; }
        MainFormService MainFormService { get; }
        ResponseService ResponseService { get; }
        SubFormService SubFormService { get; }
    }
}
