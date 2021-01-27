using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Malzamaty.Model;
using Malzamaty.Dto;
namespace Malzamaty.Repositories.Profiles
{
    public class ValueResolver : IValueResolver<File, FileWithRatingReadDto, double>
    {
        public double Resolve(File file, FileWithRatingReadDto destination, double destMember, ResolutionContext context)
        {
            return file.Rating.Average(o => o.Rate);
        }
    }
}
