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
    public class LiabilityRepository : ILiabilityRepository
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
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _liabilityDtoValidator = liabilityDtoValidator ?? throw new ArgumentNullException(nameof(liabilityDtoValidator));
            _liabilityTypeValidator = liabilityTypeValidator ?? throw new ArgumentNullException(nameof(liabilityTypeValidator));
        }

        private IQueryable<Liability> GetLiabilitiesQuery(bool asNoTracking = true)
        {
            var liabilitiesQuery = _context.Liabilities
                .Include(x => x.Type)
                .AsQueryable();

            return asNoTracking
                ? liabilitiesQuery.AsNoTracking()
                : liabilitiesQuery;
        }

        private IQueryable<LiabilityType> GetLiabilityTypesQuery(bool asNoTracking = true)
        {
            var liabilityTypesQuery = _context.LiabilityTypes
                .AsQueryable();

            return asNoTracking
                ? liabilityTypesQuery.AsNoTracking()
                : liabilityTypesQuery;
        }

        public async Task<ILiability> GetLiabilityAsync(int id)
        {
            return await GetLiabilitiesQuery()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"{nameof(Liability)} with Id='{id}' was not found");
        }

        public async Task<ILiability> GetLiabilityAsync(LiabilityDto liabilityDto)
        {
            return await GetLiabilitiesQuery()
                .FirstOrDefaultAsync(x => x.Name == liabilityDto.Name
                    && x.TypeName == liabilityDto.TypeName
                    && x.Value == liabilityDto.Value);
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

            var liability = await GetLiabilityAsync(liabilityDto) is null
                ? _mapper.Map<Liability>(liabilityDto)
                : throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"{nameof(Liability)}='{JsonSerializer.Serialize(liabilityDto)}' already exists");

            await _context.Liabilities.AddAsync(liability);
            await _context.SaveChangesAsync();

            await _context.Entry(liability)
                .Reference(x => x.Type)
                .LoadAsync();

            _logger.LogInformation($"Created {nameof(Liability)}='{JsonSerializer.Serialize(liability)}'");

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

        public async Task<ILiabilityType> AddLiabilityTypeAsync(string name)
        {
            var liabilityType = new LiabilityType
            { 
                Name = name 
            };

            await _liabilityTypeValidator.ValidateAndThrowAsync(liabilityType);

            if ((await GetLiabilityTypesAsync()).Any(x => x.Name == name))
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"{nameof(LiabilityType)} with Name='{name}' already exists");

            await _context.LiabilityTypes
                .AddAsync(liabilityType);
            await _context.SaveChangesAsync();

            return liabilityType;
        }
    }
}