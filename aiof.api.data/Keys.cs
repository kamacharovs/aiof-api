using System;

namespace aiof.api.data
{
    public static class Keys
    {
        public const string Accept = nameof(Accept);
        public const string ApplicationJson = "application/json";
        public const string ApplicationProblemJson = "application/problem+json";

        public const string FeatureManagement = nameof(FeatureManagement);

        public const string Data = nameof(Data);
        public const string InMemory = nameof(InMemory);
        public const string PostgreSQL = nameof(PostgreSQL);
        public const string DataInMemory = Data + ":" + InMemory;
        public const string DataPostgreSQL = Data + ":" + PostgreSQL;

        public const string Cors = nameof(Cors);
        public const string Portal = nameof(Portal);
        public const string CorsPortal = Cors + ":" + Portal;

        public const string Polly = nameof(Polly);
        public const string DefaultRetry = nameof(DefaultRetry);
        public const string PollyDefaultRetry = nameof(Polly) + ":" + nameof(DefaultRetry);

        public const string Metadata = nameof(Metadata);
        public const string BaseUrl = nameof(BaseUrl);
        public const string DefaultFrequency = nameof(DefaultFrequency);
        public const string MetadataBaseUrl = nameof(Metadata) + ":" + nameof(BaseUrl);
        public const string MetadataDefaultFrequency = nameof(Metadata) + ":" + nameof(DefaultFrequency);

        public const string Jwt = nameof(Jwt);
        public const string Bearer = nameof(Bearer);
        public const string Issuer = nameof(Issuer);
        public const string Audience = nameof(Audience);
        public const string PublicKey = nameof(PublicKey);
        public const string JwtIssuer = nameof(Jwt) + ":" + nameof(Issuer);
        public const string JwtAudience = nameof(Jwt) + ":" + nameof(Audience);
        public const string JwtPublicKey = nameof(Jwt) + ":" + nameof(PublicKey);

        public const string OpenApi = nameof(OpenApi);
        public const string Version = nameof(Version);
        public const string Title = nameof(Title);
        public const string Description = nameof(Description);
        public const string Contact = nameof(Contact);
        public const string Name = nameof(Name);
        public const string Email = nameof(Email);
        public const string Url = nameof(Url);
        public const string OpenApiVersion = nameof(OpenApi) + ":" + nameof(Version);
        public const string OpenApiTitle = nameof(OpenApi) + ":" + nameof(Title);
        public const string OpenApiDescription = nameof(Description) + ":" + nameof(Description);
        public const string OpenApiContactName = nameof(OpenApi) + ":" + nameof(Contact) + ":" + nameof(Name);
        public const string OpenApiContactEmail = nameof(OpenApi) + ":" + nameof(Contact) + ":" + nameof(Email);
        public const string OpenApiContactUrl = nameof(OpenApi) + ":" + nameof(Contact) + ":" + nameof(Url);
        public const string OpenApiLicenseName = nameof(OpenApi) + ":" + nameof(License) + ":" + nameof(Name);
        public const string OpenApiLicenseUrl = nameof(OpenApi) + ":" + nameof(License) + ":" + nameof(Url);
        
        public const string License = nameof(License);

        public static class Claim
        {
            public const string UserId = "user_id";
            public const string ClientId = "client_id";
            public const string PublicKey = "public_key";
        }

        public static class Entity
        {
            public static string User = nameof(data.User).ToSnakeCase();
            public static string UserProfile = nameof(data.UserProfile).ToSnakeCase();
            public static string Asset = nameof(data.Asset).ToSnakeCase();
            public static string Liability = nameof(data.Liability).ToSnakeCase();
            public static string Goal = nameof(data.Goal).ToSnakeCase();
            public static string AssetType = nameof(data.AssetType).ToSnakeCase();
            public static string LiabilityType = nameof(data.LiabilityType).ToSnakeCase();
            public static string GoalType = nameof(data.GoalType).ToSnakeCase();
            public static string Frequency = nameof(data.Frequency).ToSnakeCase();
            public static string Subscription = nameof(data.Subscription).ToSnakeCase();
            public static string Account = nameof(data.Account).ToSnakeCase();
            public static string AccountType = nameof(data.AccountType).ToSnakeCase();
            public static string AccountTypeMap = nameof(data.AccountTypeMap).ToSnakeCase();
        }
    }
}