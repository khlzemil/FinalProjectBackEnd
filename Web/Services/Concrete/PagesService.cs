using DataAccess.Repositories.Abstract;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Services.Concrete
{
    public class PagesService : IPagesService
    {
        private readonly IPricingRepository _pricingRepository;

        public PagesService(IPricingRepository pricingRepository)
        {
            _pricingRepository = pricingRepository;
        }

        public async Task<PagesIndexVM> GetAllAsync()
        {
            var model = new PagesIndexVM
            {
                Pricings = await _pricingRepository.GetAllAsync()
            };
            return model;
        }
    }
}
