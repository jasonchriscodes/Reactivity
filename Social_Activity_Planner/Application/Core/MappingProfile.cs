using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Activities;
using AutoMapper;
using Domain;

namespace Application.Core
{
 public class MappingProfile : Profile
 {
  public MappingProfile()
  {
   CreateMap<Activity, Activity>(); // map from Activity to Activity must match with that in edit handler
   CreateMap<Activity, ActivityDto>(); // map from Activity to ActivityDto
  }
 }
}
