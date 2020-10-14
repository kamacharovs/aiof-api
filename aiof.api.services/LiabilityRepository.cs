using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using AutoMapper;
using FluentValidation;

using aiof.api.data;

namespace aiof.api.services
{
    public class LiabilityRepository : BaseRepository, ILiabilityRepository
    {
        private readonly ILogger<LiabilityRepository> _logger;
        private readonly IMapper _mapper;
        private readonly AiofContext _context;
        private readonly AbstractValidator<LiabilityDto> _liabilityDtoValidator;
        private readonly AbstractValidator<LiabilityType> _liabilityTypeValidator;

        public LiabilityRepository(
            ILogger<LiabilityRepository> logger,
            IMapper mapper, 
            AiofContext context,
            AbstractValidator<LiabilityDto> liabilityDtoValidator,
            AbstractValidator<LiabilityType> liabilityTypeValidator)
            : base(logger, context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _liabilityDtoValidator = liabilityDtoValidator ?? throw new ArgumentNullException(nameof(liabilityDtoValidator));
            _liabilityTypeValidator = liabilityTypeValidator ?? throw new ArgumentNullException(nameof(liabilityTypeValidator));
        }

        private IQueryable<Liability> GetQuery(bool asNoTracking = true)
        {
            var liabilitiesQuery = _context.Liabilities
                .Include(x => x.Type)
                .AsQueryable();

            return asNoTracking
                ? liabilitiesQuery.AsNoTracking()
                : liabilitiesQuery;
        }
        private IQueryable<LiabilityType> GetTypesQuery(bool asNoTracking = true)
        {
            var liabilityTypesQuery = _context.LiabilityTypes
                .AsQueryable();

            return asNoTracking
                ? liabilityTypesQuery.AsNoTracking()
                : liabilityTypesQuery;
        }

        public async Task<ILiability> GetAsync(
            int id,
            bool asNoTracking = true)
        {
            return await GetQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"{nameof(Liability)} with Id={id} was not found");
        }

        public async Task<ILiability> GetAsync(LiabilityDto liabilityDto)
        {
            return await GetQuery()
                .FirstOrDefaultAsync(x => x.Name == liabilityDto.Name
                    && x.TypeName == liabilityDto.TypeName
                    && x.Value == liabilityDto.Value);
        }

        public async Task<IEnumerable<ILiabilityType>> GetTypesAsync()
        {
            return await GetTypesQuery()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<ILiability> AddAsync(LiabilityDto liabilityDto)
        {
            await _liabilityDtoValidator.ValidateAndThrowAsync(liabilityDto);

            var liability = await GetAsync(liabilityDto) is null
                ? _mapper.Map<Liability>(liabilityDto)
                : throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"{nameof(Liability)}={JsonSerializer.Serialize(liabilityDto)} already exists");

            await _context.Liabilities.AddAsync(liability);
            await _context.SaveChangesAsync();

            _logger.LogInformation("{Tenant} | Created Liability={Liability}",
                _context._tenant.Log,
                JsonSerializer.Serialize(liability));

            return liability;
        }

        public async IAsyncEnumerable<ILiability> AddAsync(IEnumerable<LiabilityDto> liabilityDtos)
        {
            foreach (var liabilityDto in liabilityDtos)
                yield return await AddAsync(liabilityDto);
        }

        public async Task<ILiability> UpdateAsync(
            int id, 
            LiabilityDto liabilityDto)
        {
            await _liabilityDtoValidator.ValidateAndThrowAsync(liabilityDto);

            var liability = await GetAsync(id);

            _context.Liabilities
                .Update(_mapper.Map(liabilityDto, liability as Liability));

            await _context.SaveChangesAsync();

            _logger.LogInformation("{Tenant} | Updated Liability={Liability}",
                _context._tenant.Log,
                JsonSerializer.Serialize(liability));

            return liability;
        }

        public async Task<ILiabilityType> AddTypeAsync(string name)
        {
            var liabilityType = new LiabilityType
            { 
                Name = name 
            };

            await _liabilityTypeValidator.ValidateAndThrowAsync(liabilityType);

            if ((await GetTypesAsync()).Any(x => x.Name == name))
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"{nameof(LiabilityType)} with Name={name} already exists");

            await _context.LiabilityTypes
                .AddAsync(liabilityType);
            await _context.SaveChangesAsync();

            return liabilityType;
        }

        public async Task DeleteAsync(int id)
        {
            await base.SoftDeleteAsync<Liability>(id);
        }
    }
}