using Etic.Data.Abstract;
using Etic.Entities;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Etic.Business.Services
{
    public class SettingService : ISettingService
    {
        ISettingDal _settingDal;
        IMemoryCache _memoryCache;
        public SettingService(ISettingDal settingDal, IMemoryCache memoryCache)
        {
            _settingDal = settingDal;
            _memoryCache = memoryCache;
        }

        public Setting GetSetting(string name)
        {
            var cacheData = _memoryCache.Get("setting_"+name);
            if (cacheData == null)
            {
                var data = _settingDal.Get(x => x.Id == name);
                _memoryCache.Set("setting_" + name, data);
                return data;
            }
            return (Setting)cacheData;
           
        }

        public IList<Setting> GetSettings()
        {
            var cacheData = _memoryCache.Get("settings");
            if (cacheData == null)
            {
                var data = _settingDal.GetAll();
                _memoryCache.Set("settings", data, TimeSpan.FromMinutes(5));
                return data;
            }
            return (IList<Setting>)cacheData;
          
        }
    }
    public interface ISettingService
    {
        public IList<Setting> GetSettings();
        public Setting GetSetting(string name);
    }
}
