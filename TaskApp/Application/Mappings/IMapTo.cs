using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Application.Mappings
{
    public interface IMapTo<T>
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap(GetType(), typeof(T));
        }
    }
}
