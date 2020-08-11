using System;

namespace aiof.api.data
{
    public static class Keys
    {
        public const string FeatureManagement = nameof(FeatureManagement);

        public const string Database = nameof(Database);

        public const string Metadata = nameof(Metadata);
        public const string BaseUrl = nameof(BaseUrl);
        public const string DefaultFrequency = nameof(DefaultFrequency);
        
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
    }
}