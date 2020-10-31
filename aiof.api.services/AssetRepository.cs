using System;
using System.Text.Json;
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
    public class AssetRepository : BaseRepository, IAssetRepository
    {
        private readonly ILogger<AssetRepository> _logger;
        private readonly IMapper _mapper;
        private readonly AiofContext _context;
        private readonly AbstractValidator<AssetDto> _assetDtoValidator;

        public AssetRepository(
            ILogger<AssetRepository> logger,
            IMapper mapper, 
            AiofContext context,
            AbstractValidator<AssetDto> assetDtoValidator)
            : base(logger, context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _assetDtoValidator = assetDtoValidator ?? throw new ArgumentNullException(nameof(assetDtoValidator));
        }

        private IQueryable<Asset> GetQuery(bool asNoTracking = true)
        {
            var assetsQuery = _context.Assets
                .Include(x => x.Type)
                .AsQueryable();

            return asNoTracking
                ? assetsQuery.AsNoTracking()
                : assetsQuery;
        }
        private IQueryable<AssetType> GetTypesQuery(bool asNoTracking = true)
        {
            var query = _context.AssetTypes
                .AsQueryable();

            return asNoTracking
                ? query.AsNoTracking()
                : query;
        }

        public async Task<IAsset> GetAsync(
            int id, 
            bool asNoTracking = true)
        {
            return await GetQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"{nameof(Asset)} with Id={id} was not found");
        }

        public async Task<IAsset> GetAsync(
            string name,
            string typeName,
            decimal? value,
            bool asNoTracking = true)
        {
            return await GetQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.Name == name
                    && x.TypeName == typeName
                    && x.Value == value);
        }
        public async Task<IAsset> GetAsync(AssetDto assetDto)
        {
            return await GetAsync(
                assetDto.Name,
                assetDto.TypeName,
                assetDto.Value);
        }

        public async Task<IEnumerable<IAsset>> GetAsync(string typeName)
        {
            return await GetQuery()
                .Where(x => x.TypeName == typeName)
                .OrderBy(x => x.TypeName)
                .ToListAsync();
        }

        public async Task<IEnumerable<IAsset>> GetAllAsync()
        {
            return await GetQuery()
                .ToListAsync();
        }

        public async Task<IEnumerable<IAssetType>> GetTypesAsync()
        {
            return await GetTypesQuery()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<IAsset> AddAsync(AssetDto assetDto)
        {
            await _assetDtoValidator.ValidateAndThrowAsync(assetDto);

            if (await GetAsync(assetDto) != null)
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"Asset already exists");

            var asset = _mapper.Map<Asset>(assetDto);

            asset.UserId = _context.Tenant.UserId;

            await _context.Assets.AddAsync(asset);
            await _context.SaveChangesAsync();

            await _context.Entry(asset)
                .Reference(x => x.Type)
                .LoadAsync();
            
            _logger.LogInformation("{Tenant} | Created Asset with Id={AssetId}, PublicKey={AssetPublicKey} and UserId={AssetUserId}",
                _context.Tenant.Log,
                asset.Id,
                asset.PublicKey,
                asset.UserId);

            return asset;
        }
        public async IAsyncEnumerable<IAsset> AddAsync(IEnumerable<AssetDto> assetsDto)
        {
            foreach (var assetDto in assetsDto)
                yield return await AddAsync(assetDto);
        }

        public async Task<IAsset> UpdateAsync(
            int id, 
            AssetDto assetDto)
        {
            var asset = await GetAsync(id, asNoTracking: false);
            var assetToAdd = _mapper.Map(assetDto, asset as Asset);

            _context.Assets
                .Update(assetToAdd);

            await _context.SaveChangesAsync();
            await _context.Entry(asset)
                .Reference(x => x.Type)
                .LoadAsync();

            _logger.LogInformation("{Tenant} | Updated Asset with Id={AssetId}, PublicKey={AssetPublicKey} and UserId={AssetUserId}",
                _context.Tenant.Log,
                asset.Id,
                asset.PublicKey,
                asset.UserId);

            return asset;
        }

        public async Task DeleteAsync(int id)
        {
            await base.SoftDeleteAsync<Asset>(id);
        }
    }
}