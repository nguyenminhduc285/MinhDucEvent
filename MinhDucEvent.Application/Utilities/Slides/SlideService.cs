using MinhDucEvent.Application.System.Roles;
using MinhDucEvent.Data.EF;
using MinhDucEvent.Data.Entities;
using MinhDucEvent.ViewModels.System.Roles;
using MinhDucEvent.ViewModels.Utilities.Slides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhDucEvent.Application.Utilities.Slides
{
    public class SlideService : ISlideService
    {
        private readonly MinhDucEventDbContext _context;

        public SlideService(MinhDucEventDbContext context)
        {
            _context = context;
        }

        public async Task<List<SlideVm>> GetAll()
        {
            var slides = await _context.Slides.OrderBy(x => x.SortOrder)
                .Select(x => new SlideVm()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Url = x.Url,
                    Image = x.Image
                }).ToListAsync();

            return slides;
        }
    }
}