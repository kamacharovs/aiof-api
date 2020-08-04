using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using AutoMapper;
using FluentValidation;

using aiof.api.data;

namespace aiof.api.services
{
    public class LiabilityRepository : ILiabilityRepository
    {
        private readonly ILogger<LiabilityRepository> _logger;
        private readonly IMapper _mapper;
        private readonly AiofContext _context;
        private readonly AbstractValidator<LiabilityDto> _liabilityDtoValidator;

        public LiabilityRepository(
            ILogger<LiabilityRepository> logger,
            IMapper mapper, 
            AiofContext context,
            AbstractValidator<LiabilityDto> liabilityDtoValidator)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _liabilityDtoValidator = liabilityDtoValidator ?? throw new ArgumentNullException(nameof(liabilityDtoValidator));
        }

        private IQueryable<Liability> GetLiabilitiesQuery(bool asNoTracking = true)
        {
            return asNoTracking
                ? _context.Liabilities
                    .Include(x => x.Type)
                    .AsNoTracking()
                    .AsQueryable()
                : _context.Liabilities
                    .Include(x => x.Type)
                    .AsQueryable();
        }

        private IQueryable<LiabilityType> GetLiabilityTypesQuery(bool asNoTracking = true)
        {
            return asNoTracking
                ? _context.LiabilityTypes
                    .AsNoTracking()
                    .AsQueryable()
                : _context.LiabilityTypes
                    .AsQueryable();
        }

        public async Task<ILiability> GetLiabilityAsync(int id)
        {
            return await GetLiabilitiesQuery()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"{nameof(Liability)} with Id='{id}' was not found");
        }

        public async Task<IEnumerable<ILiabilityType>> GetLiabilityTypesAsync()
        {
            return await GetLiabilityTypesQuery()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<ILiability> AddLiabilityAsync(LiabilityDto liabilityDto)
        {
            await _liabilityDtoValidator.ValidateAndThrowAsync(liabilityDto);

            var liability = _mapper.Map<Liability>(liabilityDto);

            await _context.Liabilities
                .AddAsync(liability);

            await _context.SaveChangesAsync();

            await _context.Entry(liability)
                .Reference(x => x.Type)
                .LoadAsync();

            _logger.LogInformation($"Created {nameof(Liability)} with Id='{liability.Id}', PublicKey='{liability.PublicKey}' and UserId='{liability.UserId}'");

            return liability;
        }

        public async IAsyncEnumerable<ILiability> AddLiabilitiesAsync(IEnumerable<LiabilityDto> liabilityDtos)
        {
            foreach (var liabilityDto in liabilityDtos)
                yield return await AddLiabilityAsync(liabilityDto);
        }

        public async Task<ILiability> UpdateLiabilityAsync(
            int id, 
            LiabilityDto liabilityDto)
        {
            await _liabilityDtoValidator.ValidateAndThrowAsync(liabilityDto);

            var liability = await GetLiabilityAsync(id);

            _context.Liabilities
                .Update(_mapper.Map(liabilityDto, liability as Liability));

            await _context.SaveChangesAsync();

            return liability;
        }
    }
}