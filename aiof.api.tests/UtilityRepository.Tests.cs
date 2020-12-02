using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.tests
{
    public class UtilityRepositoryTests
    {
        private readonly IUtilityRepository _repo;

        public UtilityRepositoryTests()
        {
            _repo = new ServiceHelper().GetRequiredService<IUtilityRepository>() ?? throw new ArgumentNullException(nameof(UtilityRepository));
        }

        [Theory]
        [MemberData(nameof(Helper.UsefulDocumentationPage), MemberType = typeof(Helper))]
        public async Task GetUsefulDocumentationsByPageAsync_IsSuccessful(string page)
        {
            var docs = await _repo.GetUsefulDocumentationsByPageAsync(page);
            var doc = docs.FirstOrDefault();

            Assert.NotNull(docs);
            Assert.NotEmpty(docs);
            Assert.NotNull(doc);
            Assert.NotEqual(0, doc.Id);
            Assert.NotEqual(Guid.Empty, doc.PublicKey);
            Assert.NotNull(doc.Page);
            Assert.NotNull(doc.Name);
            Assert.NotNull(doc.Url);
            Assert.NotNull(doc.Category);
        }
        [Fact]
        public async Task GetUsefulDocumentationsByPageAsync_NotExistentPage_IsEmpty()
        {
            var docs = await _repo.GetUsefulDocumentationsByPageAsync("definitelydoesntexist");

            Assert.Empty(docs);
        }

        [Theory]
        [MemberData(nameof(Helper.UsefulDocumentationCategory), MemberType = typeof(Helper))]
        public async Task GetUsefulDocumentationsByCategoryAsync_IsSuccessful(string category)
        {
            var docs = await _repo.GetUsefulDocumentationsByCategoryAsync(category);
            var doc = docs.FirstOrDefault();

            Assert.NotNull(docs);
            Assert.NotEmpty(docs);
            Assert.NotNull(doc);
            Assert.NotEqual(0, doc.Id);
            Assert.NotEqual(Guid.Empty, doc.PublicKey);
            Assert.NotNull(doc.Page);
            Assert.NotNull(doc.Name);
            Assert.NotNull(doc.Url);
            Assert.NotNull(doc.Category);
        }
        [Fact]
        public async Task GetUsefulDocumentationsByCategoryAsync_NotExistentPage_IsEmpty()
        {
            var docs = await _repo.GetUsefulDocumentationsByCategoryAsync("definitelydoesntexist");

            Assert.Empty(docs);
        }

        [Theory]
        [MemberData(nameof(Helper.UsefulDocumentationPage), MemberType = typeof(Helper))]
        public async Task GetUsefulDocumentationsAsync_ByPage_IsSuccessful(string page)
        {
            var docs = await _repo.GetUsefulDocumentationsAsync(page: page);
            var doc = docs.FirstOrDefault();

            Assert.NotNull(docs);
            Assert.NotEmpty(docs);
            Assert.NotNull(doc);
            Assert.NotEqual(0, doc.Id);
            Assert.NotEqual(Guid.Empty, doc.PublicKey);
            Assert.NotNull(doc.Page);
            Assert.NotNull(doc.Name);
            Assert.NotNull(doc.Url);
            Assert.NotNull(doc.Category);
        }
        [Theory]
        [MemberData(nameof(Helper.UsefulDocumentationCategory), MemberType = typeof(Helper))]
        public async Task GetUsefulDocumentationsAsync_ByCategory_IsSuccessful(string category)
        {
            var docs = await _repo.GetUsefulDocumentationsAsync(category: category);
            var doc = docs.FirstOrDefault();

            Assert.NotNull(docs);
            Assert.NotEmpty(docs);
            Assert.NotNull(doc);
            Assert.NotEqual(0, doc.Id);
            Assert.NotEqual(Guid.Empty, doc.PublicKey);
            Assert.NotNull(doc.Page);
            Assert.NotNull(doc.Name);
            Assert.NotNull(doc.Url);
            Assert.NotNull(doc.Category);
        }
    }
}
