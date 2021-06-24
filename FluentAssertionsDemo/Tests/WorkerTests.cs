using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using FluentAssertionsDemo.Api;
using Xunit;

namespace FluentAssertionsDemo.Tests
{
    public class WorkerTests
    {
        private readonly Worker _sut;

        private readonly IFixture _fixture;
        
        public WorkerTests()
        {
            _sut = new Worker();
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }
        
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Work_ShouldWork_ShouldSetIsWorking(bool shouldWorkInput)
        {
            _sut.AddJobs(CreateMockJob());
            
            var result = _sut.Work(shouldWorkInput);

            result.Should().Be(shouldWorkInput);
        }

        [Fact]
        public void Work_ThisWillFail_KindaUgly()
        {
            var actualIsWorkingValue = _sut.Work(true);

            Assert.True(actualIsWorkingValue);
        }
        
        [Fact]
        public void Work_ThisWillFail_LessUgly()
        {
            var actualIsWorkingValue = _sut.Work(true);

            actualIsWorkingValue.Should().BeTrue();
        }

        [Fact]
        public void AddJob_NoJobs_ThrowsArgumentException_XUnit()
        {
            Job[] stubJobs = Array.Empty<Job>();
            
            Action addJobAction = () => _sut.AddJobs(stubJobs);

            ArgumentException actualArgumentException = Assert.Throws<ArgumentException>(addJobAction);
            Assert.Equal("Must add at least one job", actualArgumentException.Message);
        }
        
        [Fact]
        public void AddJob_NoJobs_ThrowsArgumentException_FluentAssertions()
        {
            Job[] stubJobs = Array.Empty<Job>();
            
            Action addJobAction = () => _sut.AddJobs(stubJobs);

            addJobAction.Should()
                .Throw<ArgumentException>()
                .WithMessage("Must add at least one job");
        }

        private Job CreateMockJob() => _fixture.Create<Job>();

        private Job CreateMockJob(string name, int timeTaken) => _fixture.Build<Job>()
            .With(x => x.Name, name)
            .With(x => x.TimeTaken, timeTaken)
            .Create();
    }
}
