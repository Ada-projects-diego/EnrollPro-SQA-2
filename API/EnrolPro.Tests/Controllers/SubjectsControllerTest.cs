//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using EnrolPro.src.Data;
//using EnrolPro.src.Models;
//using EnrolPro.src.Controllers;
//using System.Linq.Expressions;

//namespace EnrolPro.Tests.Controllers
//{
//    public class SubjectsControllerTest
//    {
//        private readonly SubjectsController _controller;
//        private readonly Mock<IStudentEnrolmentContext> _mockContext;

//        public SubjectsControllerTest()
//        {
//            _mockContext = new Mock<IStudentEnrolmentContext>();
//            var mockSet = new Mock<DbSet<Subjects>>();

//            _mockContext.Setup(m => m.Subject).Returns(mockSet.Object);
//            _controller = new SubjectsController(_mockContext.Object);
//        }

//        [Fact]
//        public async Task GetSubject_Returns_All_Subjects()
//        {
//            // Arrange
//            var subjects = new List<Subjects>
//            {
//                new("Algebra", "Study of mathematical symbols and rules"),
//                new("Biology", "Study of living organisms")
//            };

//            var mockSet = new Mock<DbSet<Subjects>>();

//            mockSet.As<IAsyncEnumerable<Subjects>>()
//                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
//                .Returns(new TestAsyncEnumerator<Subjects>(subjects.GetEnumerator()));

//            mockSet.As<IQueryable<Subjects>>()
//                .Setup(m => m.Provider)
//                .Returns(new TestAsyncQueryProvider<Subjects>(subjects.AsQueryable().Provider));

//            mockSet.As<IQueryable<Subjects>>().Setup(m => m.Expression).Returns(subjects.AsQueryable().Expression);
//            mockSet.As<IQueryable<Subjects>>().Setup(m => m.ElementType).Returns(subjects.AsQueryable().ElementType);
//            mockSet.As<IQueryable<Subjects>>().Setup(m => m.GetEnumerator()).Returns(subjects.GetEnumerator());

//            _mockContext.Setup(c => c.Subject).Returns(mockSet.Object);

//            // Act
//            var result = await _controller.GetSubject();

//            // Assert
//            var actionResult = Assert.IsType<ActionResult<IEnumerable<Subjects>>>(result);
//            var returnValue = Assert.IsType<List<Subjects>>(actionResult.Value);
//            Assert.Equal(2, returnValue.Count);
//        }

//        [Fact]
//        public async Task GetSubject_Returns_Subject_By_Id()
//        {
//            // Arrange
//            var subjectId = 0;
//            var subject = new Subjects("Algebra", "Study of mathematical symbols and rules");

//            _mockContext.Setup(c => c.Subject.FindAsync(subjectId))
//                .ReturnsAsync(subject);

//            // Act
//            var result = await _controller.GetSubject(subjectId);

//            // Assert
//            var actionResult = Assert.IsType<ActionResult<Subjects>>(result);
//            var returnValue = Assert.IsType<Subjects>(actionResult.Value);
//            Assert.Equal(subjectId, returnValue.SubjectId);
//        }

//        [Fact]
//        public async Task GetSubject_Returns_NotFound_For_Invalid_Id()
//        {
//            // Arrange
//            var subjectId = 999;

//            _mockContext.Setup(c => c.Subject.FindAsync(subjectId))
//                .ReturnsAsync((Subjects)null);

//            // Act
//            var result = await _controller.GetSubject(subjectId);

//            // Assert
//            Assert.IsType<NotFoundResult>(result.Result);
//        }

//        [Fact]
//        public async Task PostSubject_Creates_Subject()
//        {
//            // Arrange
//            var subject = new Subjects("Algebra", "Study of mathematical symbols and rules") { CourseId = 1 };
//            var mockCourseSet = new Mock<DbSet<Courses>>();
//            var courses = new List<Courses> { new("Maths", "Mathematics 101") { CourseId = 1 } }.AsQueryable();

//            mockCourseSet.As<IAsyncEnumerable<Courses>>()
//                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
//                .Returns(new TestAsyncEnumerator<Courses>(courses.GetEnumerator()));

//            mockCourseSet.As<IQueryable<Courses>>()
//                .Setup(m => m.Provider)
//                .Returns(new TestAsyncQueryProvider<Courses>(courses.Provider));

//            mockCourseSet.As<IQueryable<Courses>>().Setup(m => m.Expression).Returns(courses.Expression);
//            mockCourseSet.As<IQueryable<Courses>>().Setup(m => m.ElementType).Returns(courses.ElementType);
//            mockCourseSet.As<IQueryable<Courses>>().Setup(m => m.GetEnumerator()).Returns(courses.GetEnumerator());

//            _mockContext.Setup(m => m.Course).Returns(mockCourseSet.Object);

//            _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
//                .ReturnsAsync(1);

//            // Act
//            var result = await _controller.PostSubject(subject);

//            // Assert
//            var actionResult = Assert.IsType<ActionResult<Subjects>>(result);
//            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
//            var returnValue = Assert.IsType<Subjects>(createdAtActionResult.Value);
//            Assert.Equal(subject.SubjectName, returnValue.SubjectName);
//            Assert.Equal(subject.CourseId, returnValue.CourseId);

//            _mockContext.Verify(m => m.Subject.Add(It.IsAny<Subjects>()), Times.Once);
//            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
//        }


//        [Fact]
//        public async Task DeleteSubject_Removes_Subject()
//        {
//            // Arrange
//            var subjectId = 1;
//            var subject = new Subjects("Algebra", "Study of mathematical symbols and rules");

//            _mockContext.Setup(c => c.Subject.FindAsync(subjectId))
//                .ReturnsAsync(subject);

//            // Act
//            var result = await _controller.DeleteSubject(subjectId);

//            // Assert
//            Assert.IsType<NoContentResult>(result);
//            _mockContext.Verify(c => c.Subject.Remove(subject), Times.Once);
//            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
//        }

//        [Fact]
//        public async Task DeleteSubject_Returns_NotFound_For_Invalid_Id()
//        {
//            // Arrange
//            var subjectId = 999;

//            _mockContext.Setup(c => c.Subject.FindAsync(subjectId))
//                .ReturnsAsync((Subjects)null);

//            // Act
//            var result = await _controller.DeleteSubject(subjectId);

//            // Assert
//            Assert.IsType<NotFoundResult>(result);
//        }
//    }
//}