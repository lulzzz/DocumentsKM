using System;
using System.Collections.Generic;
using System.Linq;
using DocumentsKM.Data;
using DocumentsKM.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DocumentsKM.Tests
{
    public class GeneralDataPointRepoTest
    {
        private readonly Random _rnd = new Random();
        private readonly int _maxUserId = 3;
        private readonly int _maxSectionId = 3;

        private ApplicationContext GetContext(List<GeneralDataPoint> generalDataPoints)
        {
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            builder.UseInMemoryDatabase(databaseName: "GeneralDataPointTestDb");
            var options = builder.Options;
            var context = new ApplicationContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Users.AddRange(TestData.users);
            context.GeneralDataSections.AddRange(TestData.generalDataSections);
            context.GeneralDataPoints.AddRange(generalDataPoints);
            context.SaveChanges();
            return context;
        }

        [Fact]
        public void GetAllByUserAndSectionId_ShouldReturnGeneralDataPoints()
        {
            // Arrange
            var context = GetContext(TestData.generalDataPoints);
            var repo = new SqlGeneralDataPointRepo(context);

            var userId = _rnd.Next(1, _maxUserId);
            var sectionId = _rnd.Next(1, _maxSectionId);

            // Act
            var generalDataPoints = repo.GetAllByUserAndSectionId(userId, sectionId);

            // Assert
            Assert.Equal(TestData.generalDataPoints.Where(
                v => v.User.Id == userId && v.Section.Id == sectionId),
                generalDataPoints);

            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public void GetAllByUserAndSectionId_ShouldReturnEmptyArray_WhenWrongUserOrSectionId()
        {
            // Arrange
            var context = GetContext(TestData.generalDataPoints);
            var repo = new SqlGeneralDataPointRepo(context);

            var userId = _rnd.Next(1, _maxUserId);
            var wrongUserId = 999;
            var sectionId = _rnd.Next(1, _maxSectionId);
            var wrongSectionId = 999;

            // Act
            var generalDataPoints1 = repo.GetAllByUserAndSectionId(wrongUserId, sectionId);
            var generalDataPoints2 = repo.GetAllByUserAndSectionId(userId, wrongSectionId);

            // Assert
            Assert.Empty(generalDataPoints1);
            Assert.Empty(generalDataPoints2);

            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public void GetById_ShouldReturnGeneralDataPoint()
        {
            // Arrange
            var context = GetContext(TestData.generalDataPoints);
            var repo = new SqlGeneralDataPointRepo(context);

            int id = _rnd.Next(1, TestData.generalDataPoints.Count());

            // Act
            var generalDataPoint = repo.GetById(id);

            // Assert
            Assert.Equal(TestData.generalDataPoints.SingleOrDefault(v => v.Id == id),
                generalDataPoint);

            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public void GetById_ShouldReturnNull_WhenWrongId()
        {
            // Arrange
            var context = GetContext(TestData.generalDataPoints);
            var repo = new SqlGeneralDataPointRepo(context);

            // Act
            var generalDataPoint = repo.GetById(999);

            // Assert
            Assert.Null(generalDataPoint);

            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public void GetByUniqueKey_ShouldReturnGeneralDataPoint()
        {
            // Arrange
            var context = GetContext(TestData.generalDataPoints);
            var repo = new SqlGeneralDataPointRepo(context);

            int id = _rnd.Next(1, TestData.generalDataPoints.Count());
            var foundGeneralDataPoint = TestData.generalDataPoints.FirstOrDefault(v => v.Id == id);
            var userId = foundGeneralDataPoint.User.Id;
            var sectionId = foundGeneralDataPoint.Section.Id;
            var text = foundGeneralDataPoint.Text;

            // Act
            var generalDataPoint = repo.GetByUniqueKey(userId, sectionId, text);

            // Assert
            Assert.Equal(id, generalDataPoint.Id);

            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public void GetByUniqueKey_ShouldReturnNull_WhenWrongValues()
        {
            // Arrange
            var context = GetContext(TestData.generalDataPoints);
            var repo = new SqlGeneralDataPointRepo(context);

            var userId = TestData.users[0].Id;
            var sectionId = TestData.generalDataSections[0].Id;
            var text = TestData.generalDataPoints[0].Text;

            // Act
            var generalDataPoint1 = repo.GetByUniqueKey(999, sectionId, text);
            var generalDataPoint2 = repo.GetByUniqueKey(userId, 999, text);
            var generalDataPoint3 = repo.GetByUniqueKey(userId, sectionId, "NotFound");

            // Assert
            Assert.Null(generalDataPoint1);
            Assert.Null(generalDataPoint2);
            Assert.Null(generalDataPoint3);

            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public void Add_ShouldAddGeneralDataPoint()
        {
            // Arrange
            var context = GetContext(TestData.generalDataPoints);
            var repo = new SqlGeneralDataPointRepo(context);

            int userId = _rnd.Next(1, TestData.users.Count());
            int sectionId = _rnd.Next(1, TestData.generalDataSections.Count());
            var generalDataPoint = new GeneralDataPoint
            {
                User = TestData.users.SingleOrDefault(v => v.Id == userId),
                Section = TestData.generalDataSections.SingleOrDefault(v => v.Id == sectionId),
                Text = "NewCreate",
                OrderNum = 3,
            };

            // Act
            repo.Add(generalDataPoint);

            // Assert
            Assert.NotNull(repo.GetById(generalDataPoint.Id));

            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public void Update_ShouldUpdateGeneralDataPoint()
        {
            // Arrange
            var generalDataPoints = new List<GeneralDataPoint>{};
            foreach (var gdp in TestData.generalDataPoints)
            {
                generalDataPoints.Add(new GeneralDataPoint
                {
                    Id = gdp.Id,
                    User = gdp.User,
                    Section = gdp.Section,
                    Text = gdp.Text,
                    OrderNum = gdp.OrderNum,
                });
            }
            var context = GetContext(generalDataPoints);
            var repo = new SqlGeneralDataPointRepo(context);

            int id = _rnd.Next(1, generalDataPoints.Count());
            var generalDataPoint = generalDataPoints.FirstOrDefault(v => v.Id == id);
            generalDataPoint.Text = "NewUpdate";

            // Act
            repo.Update(generalDataPoint);

            // Assert
            Assert.Equal(generalDataPoint.Text, repo.GetById(id).Text);

            context.Database.EnsureDeleted();
            context.Dispose();
        }

        [Fact]
        public void Delete_ShouldDeleteGeneralDataPoint()
        {
            // Arrange
            var context = GetContext(TestData.generalDataPoints);
            var repo = new SqlGeneralDataPointRepo(context);

            int id = _rnd.Next(1, TestData.generalDataPoints.Count());
            var generalDataPoint = TestData.generalDataPoints.FirstOrDefault(v => v.Id == id);

            // Act
            repo.Delete(generalDataPoint);

            // Assert
            Assert.Null(repo.GetById(id));

            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
