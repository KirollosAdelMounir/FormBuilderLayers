using FormBuilderRepositoryLayer.FormBuilderRepositories.FormDataRepos;
using FormBuilderRepositoryLayer.FormBuilderRepositories.FormFieldResultRepos;
using FormBuilderRepositoryLayer.FormBuilderRepositories.MainFormRepos;
using FormBuilderRepositoryLayer.FormBuilderRepositories.ResponseRepos;
using FormBuilderRepositoryLayer.FormBuilderRepositories.SubFormRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilderRepositoryLayer.UnitOfWork
{
    public interface IUnitOfRepositories
    {
        public IFormDataRepository formDataRepository { get; }
        public IFormFieldResultRepository formFieldResultRepository { get; }
        public IMainFormRepository mainFormRepository { get; }
        public IResponseRepository responseRepository { get; }
        public ISubFormRepository subFormRepository { get; }
            
    }
}
