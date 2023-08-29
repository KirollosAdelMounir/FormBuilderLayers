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
    public class UnitOfRepositories : IUnitOfRepositories
    {
        public UnitOfRepositories(IFormDataRepository iformDataRepository , IFormFieldResultRepository iformfieldresult,IMainFormRepository imain ,IResponseRepository iresponse,ISubFormRepository isub)
        { 
            formDataRepository = iformDataRepository;
            formFieldResultRepository = iformfieldresult;
            mainFormRepository = imain;
            responseRepository = iresponse;
            subFormRepository = isub;
        }
        public IFormDataRepository formDataRepository {get;}

        public IFormFieldResultRepository formFieldResultRepository {get;}

        public IMainFormRepository mainFormRepository {get;}

        public IResponseRepository responseRepository {get;}

        public ISubFormRepository subFormRepository {get;}
    }
}
