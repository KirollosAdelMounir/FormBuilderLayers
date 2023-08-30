using FormBuilderServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderServiceLayer.UnitOfServices
{
    public class UnitOfServices : IUnitOfServices
    {
        public UnitOfServices(FormFieldResultService formFieldResult ,FormDataService formData , MainFormService mainform, ResponseService response,SubFormService subform)
        {
            FormDataService = formData;
            FormFieldResultService = formFieldResult;
            MainFormService = mainform;
            ResponseService = response;
            SubFormService = subform;
        }

        public FormDataService FormDataService { get; }
        public FormFieldResultService FormFieldResultService { get; }
        public MainFormService MainFormService { get; }
        public ResponseService ResponseService { get; }
        public SubFormService SubFormService { get; }
    }
}
