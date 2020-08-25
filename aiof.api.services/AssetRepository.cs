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
    public class AssetRepository : IAssetRepository
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
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _assetDtoValidator = assetDtoValidator ?? throw new ArgumentNullException(nameof(assetDtoValidator));
        }

        private IQueryable<Asset> GetAssetsQuery(bool asNoTracking = true)
        {
            return asNoTracking
                ? _context.Assets
                    .Include(x => x.Type)
                    .AsNoTracking()
                    .AsQueryable()
                : _context.Assets
                    .Include(x => x.Type)
                    .AsQueryable();
        }

        private IQueryable<AssetType> GetAssetTypesQuery(bool asNoTracking = true)
        {
            return asNoTracking
                ? _context.AssetTypes
                    .AsNoTracking()
                    .AsQueryable()
                : _context.AssetTypes
                    .AsQueryable();
        }

        public async Task<IAsset> GetAssetAsync(int id)
        {
            return await GetAssetsQuery()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"{nameof(Asset)} with Id='{id}' was not found");
        }

        public async Task<IAsset> GetAssetAsync(
            string name,
            string typeName,
            decimal? value,
            int? userId = null)
        {
            return userId is null
                ? await GetAssetsQuery()
                    .FirstOrDefaultAsync(x => x.Name == name
                        && x.TypeName == typeName
                        && x.Value == value)
                : await GetAssetsQuery()
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

            await _context.Assets
                .AddAsync(asset);

            await _context.SaveChangesAsync();

            await _context.Entry(asset)
                .Reference(x => x.Type)
                .LoadAsync();
            
            _logger.LogInformation($"Created {nameof(Asset)} with Id='{asset.Id}', PublicKey='{asset.PublicKey}' and UserId='{asset.UserId}'");

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
            await _assetDtoValidator.ValidateAndThrowAsync(assetDto);

            var asset = await GetAssetAsync(id);

            _context.Assets
                .Update(_mapper.Map(assetDto, asset as Asset));

            await _context.SaveChangesAsync();

            await _context.Entry(asset)
                .Reference(x => x.Type)
                .LoadAsync();

            _logger.LogInformation($"Updated {nameof(Asset)} with Id='{asset.Id}', PublicKey='{asset.PublicKey}' and UserId='{asset.UserId}'");

            return asset;
        }

        public async Task DeleteAsync(
            string name,
            string typeName,
            decimal value,
            int? userId = null)
        {
            var asset = await GetAssetAsync(name, typeName, value) as Asset
                ?? throw new AiofNotFoundException($"{nameof(Asset)} with Name='{name}', TypeName='{typeName}' and Value='{value}' was not found");

            _context.Assets.Remove(asset);

            await _context.SaveChangesAsync();

            _logger.LogInformation($"Deleted {nameof(Asset)}='{JsonSerializer.Serialize(asset)}'");
        }
        public async Task DeleteAsync(IAsset asset)
        {
            if (asset is null)
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"{nameof(asset)} cannot be null");

            _context.Assets.Remove(asset as Asset);

            await _context.SaveChangesAsync();

            _logger.LogInformation($"Deleted {nameof(Asset)}='{JsonSerializer.Serialize(asset)}'");
        }
    }
}