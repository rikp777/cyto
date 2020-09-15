using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Context;
using DAL.Repository.Company;
using Domain.Entities;
using Domain.Resources;
using LOGIC.Services.Interfaces;

namespace LOGIC.Services.Company
{
    public class CompanyProjectService : IGenericRelationshipService<ProjectResource>
    {
        private readonly CompanyProjectRepository _companyProjectRepository;

        public CompanyProjectService()
        {
            _companyProjectRepository = new CompanyProjectRepository(new DatabaseContext());
        }

        public CompanyProjectService(IDatabaseContext context)
        {
            _companyProjectRepository = new CompanyProjectRepository(context);
   
        }

        public bool Attach(int companyId, int projectId) => _companyProjectRepository.Attach(companyId, projectId);
        public bool Detach(int companyId, int projectId) => _companyProjectRepository.Detach(companyId, projectId);

        public ProjectResource GetById(int companyId, int projectId)
        {
            var res = _companyProjectRepository.GetById(companyId, projectId);
            return res == null ? null : ProjectResource.FromEntity(res);
        }

        public List<ProjectResource> GetAll(int companyId)
            => _companyProjectRepository.GetAll(companyId)
                ?.Select(ProjectResource.FromEntity).ToList();
    }
}