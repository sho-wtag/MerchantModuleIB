using AutoMapper;
using MerchantPortal.Data.Models;
using MerchantPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantPortal.Helper
{
    public class ModelAdapter : IDisposable
    {
        public static T ModelMap<T,t>(T destination, t sourse)
        {
          
            return Mapper.Map(sourse, destination);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
