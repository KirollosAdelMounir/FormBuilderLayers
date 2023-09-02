using AutoMapper;
using FormBuilderDB.Models;
using FormBuilderRepositoryLayer.FormBuilderRepositories.MainFormRepos;
using FormBuilderRepositoryLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FormBuilderServiceLayer.DTOs;

namespace FormBuilderServiceLayer.Services
{
    public class MainFormService
    {
        private readonly IMainFormRepository mainFormRepository;
        private readonly IMapper mapper;
        public MainFormService(IMainFormRepository mainFormRepository, IMapper mapper) 
        {
            this.mainFormRepository = mainFormRepository;
            this.mapper = mapper;
        }

        public async Task CreateForm(string FormName)
        {
            MainForm form = new MainForm { Name = FormName };
            await mainFormRepository.AddAsync(form);
        }

        public async Task<MainForm> GetForm(int id)
        {
            return await mainFormRepository.GetById(id);
        }

        public async Task<List<MainForm>> GetAllForms()
        {
            return await mainFormRepository.GetUndeleted();
        }

        public async Task DeleteForm(int id) 
        {
            var mainForm = await GetForm(id);
            if (mainForm != null)
            {
                await mainFormRepository.SoftDelete(mainForm);
            }
        }

        public async Task EditForm(int id,string name)
        {
            var mainform = await GetForm(id);
            if(mainform != null)
            {
                mainform.Name = name;
                await mainFormRepository.UpdateAsync(mainform);
            }
        }
    }
}
