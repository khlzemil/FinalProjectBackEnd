using Core.Utilities.Abstract;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Services.Concrete
{
    public class HomeService : IHomeService
    {
        private readonly IAboutUsRepository _aboutUsRepository;
        private readonly IOurVisionRepository _ourVisionRepository;
        private readonly IHomeMainSliderRepository _homeMainSliderRepository;
        private readonly ICoverVideoRepository _coverVideoRepository;
        private readonly IWhyChooseRepository _whyChooseRepository;
        private readonly IStatisticRepository _statisticRepository;

        public HomeService(IAboutUsRepository aboutUsRepository,
                            IOurVisionRepository ourVisionRepository, 
                            IHomeMainSliderRepository homeMainSliderRepository,
                            ICoverVideoRepository coverVideoRepository,
                            IWhyChooseRepository whyChooseRepository,
                            IStatisticRepository statisticRepository)
        {
            _statisticRepository = statisticRepository;
            _aboutUsRepository = aboutUsRepository;
            _ourVisionRepository = ourVisionRepository;
            _homeMainSliderRepository = homeMainSliderRepository;
            _coverVideoRepository = coverVideoRepository;
            _whyChooseRepository = whyChooseRepository;
        }

        public async Task<HomeIndexVM> GetAllAsync()
        {
            var model = new HomeIndexVM
            {
                AboutUs = await _aboutUsRepository.GetWithPhotosAsync(),
                OurVisions = await _ourVisionRepository.GetAllAsync(),
                HomeMainSliders = await _homeMainSliderRepository.GetAllOrderedAsync(),
                CoverVideo = await _coverVideoRepository.GetAsync(),
                WhyChoose = await _whyChooseRepository.GetAsync(),
                Statistics = await _statisticRepository.GetAllAsync(),
            };
            return model;
        }
    }
}
