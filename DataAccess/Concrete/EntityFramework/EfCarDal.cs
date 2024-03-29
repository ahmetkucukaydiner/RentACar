﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>>? filter = null)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                                 on c.BrandId equals b.Id
                             join co in context.Colors
                                 on c.ColorId equals co.Id
                             join ci in context.CarImages on c.Id equals ci.CarId

                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 BrandId = b.Id,
                                 ColorId = co.Id,
                                 CarName = c.Description,
                                 BrandName = b.Name,
                                 ColorName = co.Name,
                                 ModelYear = c.ModelYear,
                                 DailyPrice = c.DailyPrice,
                                 ImagePath = ci.ImagePath
                             };

                return filter != null ? result.Where(filter).ToList() : result.ToList();
            }
        }
    }
}
