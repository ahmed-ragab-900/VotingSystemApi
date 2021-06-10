using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VotingSystemApi.Helper
{
    public class Helper
    {
        
        //public static string imagePath = HttpContext.Current.Server.MapPath("");
        public D AutoMap<S, D>(S source) 
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<S, D>();
            });
            IMapper iMapper = config.CreateMapper();
            D destination = iMapper.Map<S, D>(source);
            return destination;
        }

        public Image LoadBase64(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }
            //image.Save();
            return image;
        }
    }
}
