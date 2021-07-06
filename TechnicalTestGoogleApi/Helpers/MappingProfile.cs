﻿using AutoMapper;
using Data.Entities;
using Service.DataTransferObjects.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class MappingProfile: Profile
    {

        public MappingProfile()
        {
            CreateMap<UserForCreationDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
