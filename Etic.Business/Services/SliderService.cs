using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etic.Data.Abstract;
using Etic.Entities;

namespace Etic.Business.Services
{
    public class SliderService : ISliderService
    {
        private ISliderDal _sliderDal;

        public SliderService(ISliderDal sliderDal)
        {
            _sliderDal = sliderDal;
        }

        public IList<Slider> GetAll(string name)
        {
            return _sliderDal.GetAll(x => x.Name == name);
        }

        public IList<Slider> GetAll()
        {
            return _sliderDal.GetAll().OrderBy(s => s.Sort).ToList();
        }

        public IList<Slider> GetAllActive()
        {
            return _sliderDal.GetAll(x => !x.IsDeleted && x.IsActive).OrderBy(s => s.Sort).ToList();
        }

        public Slider GetById(int id)
        {
            return _sliderDal.Get(x => x.Id == id);
        }

        public void Add(Slider slider)
        {
            _sliderDal.Add(slider);
        }

        public void Update(Slider slider)
        {
            _sliderDal.Update(slider);
        }

        public void Delete(int id)
        {
            var slider = GetById(id);
            if (slider != null)
            {
                slider.IsDeleted = true;
                slider.DeletedDate = DateTime.Now;
                _sliderDal.Update(slider);
            }
        }
    }

    public interface ISliderService
    {
        IList<Slider> GetAll(string name);
        IList<Slider> GetAll();
        IList<Slider> GetAllActive();
        Slider GetById(int id);
        void Add(Slider slider);
        void Update(Slider slider);
        void Delete(int id);
    }
}
