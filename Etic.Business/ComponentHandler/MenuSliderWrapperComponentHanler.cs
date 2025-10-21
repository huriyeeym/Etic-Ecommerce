using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etic.Business.Services;
using Etic.Entities;

namespace Etic.Business.ComponentHandler
{
    public class MenuSliderWrapperComponentHandler : IMenuSliderWrapperComponentHandler
    {
        private readonly ISliderService _sliderService;

        public MenuSliderWrapperComponentHandler(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public IList<Slider> GetSlider()
        {
            return _sliderService.GetAll("home"); //
        }
    }
    public interface IMenuSliderWrapperComponentHandler
    {
        IList<Slider> GetSlider();
    }
}
