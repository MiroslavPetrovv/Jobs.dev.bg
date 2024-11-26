using AutoMapper;
using JobApplications.Services.Interfaces;

namespace JobApplications.Areas.HR.Controllers
{
    public class CompanyRecrtuiterController : BaseController
    {
        private IMapper mapper;
        private readonly ICompanyService companyService;
        private readonly IJobService jobService;
        private readonly IIndustrieService industries;

        public CompanyRecrtuiterController(IMapper mappingProfile, ICompanyService companyService,
            IJobService jobService, IIndustrieService industrieService)
        {

            mapper = mappingProfile;
            this.companyService = companyService;
            this.jobService = jobService;
            this.industries = industrieService;
        }
    }
}
