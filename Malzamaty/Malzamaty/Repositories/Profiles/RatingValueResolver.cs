using AutoMapper;
using Malzamaty.Dto;
using Malzamaty.Model;
using System;
using System.Data;

namespace Malzamaty.Repositories.Profiles
{
    public class RatingValueResolver : IValueResolver<RatingWriteDto, File, File>
    {
        public File Resolve(RatingWriteDto source, File destination, File destMember, ResolutionContext context)
        {
            return new File() {ID=destination.ID };
        }
    }
}
