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

        private IQueryable<Asset> GetAssetsQuery(bool asNoTracking = true)
        {
            var assetsQuery = _context.Assets
                .Include(x => x.Type)
                .AsQueryable();

            return asNoTracking
                ? assetsQuery.AsNoTracking()
                : assetsQuery;
        }

        private IQueryable<AssetType> GetAssetTypesQuery(bool asNoTracking = true)
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
            return await GetAssetsQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"{nameof(Asset)} with Id={id} was not found");
        }

        public async Task<IAsset> GetAssetAsync(
            string name,
            string typeName,
            decimal? value,
            int? userId = null,
            bool asNoTracking = true)
        {
            return userId is null
                ? await GetAssetsQuery(asNoTracking)
                    .FirstOrDefaultAsync(x => x.Name == name
                        && x.TypeName == typeName
                        && x.Value == value)
                : await GetAssetsQuery(asNoTracking)
                    .FirstOrDefaultAsync(x => x.Name == name
                        && x.TypeName == typeName
                        && x.Value == value
                        && x.UserId == userId);
        }
        public async Task<IAsset> GetAssetAsync(AssetDto assetDto)
        {
            return await GetAssetAsync(
                assetDto.Name,
                assetDto.TypeName,
                assetDto.Value,
                assetDto.UserId);
        }

        public async Task<IEnumerable<IAsset>> GetAssetsAsync(string typeName)
        {
            return await GetAssetsQuery()
                .Where(x => x.TypeName == typeName)
                .OrderBy(x => x.TypeName)
                .ToListAsync();
        }

        public async Task<IEnumerable<IAssetType>> GetAssetTypesAsync()
        {
            return await GetAssetTypesQuery()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<IAsset> AddAssetAsync(AssetDto assetDto)
        {
            await _assetDtoValidator.ValidateAndThrowAsync(assetDto);

            if (await GetAssetAsync(assetDto) != null)
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"{nameof(Asset)} with the provided information already exists");

            var asset = _mapper.Map<Asset>(assetDto);

            await _context.Assets.AddAsync(asset);
            await _context.SaveChangesAsync();

            await _context.Entry(asset)
                .Reference(x => x.Type)
                .LoadAsync();
            
            _logger.LogInformation("{Tenant} | Created Asset with Id={AssetId}, PublicKey={AssetPublicKey} and UserId={AssetUserId}",
                _context._tenant.Log,
                asset.Id,
                asset.PublicKey,
                asset.UserId);

            return asset;
        }
        public async IAsyncEnumerable<IAsset> AddAssetsAsync(IEnumerable<AssetDto> assetsDto)
        {
            foreach (var assetDto in assetsDto)
                yield return await AddAssetAsync(assetDto);
        }

        public async Task<IAsset> UpdateAssetAsync(
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
                _context._tenant.Log,
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