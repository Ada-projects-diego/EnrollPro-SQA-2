//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using EnrolPro.src.Data;
//using EnrolPro.src.Models;
//using EnrolPro.src.Controllers;

//namespace EnrolPro.Tests.Controllers
//{
//    public class CoursesControllerTest
//    {
//        private readonly CoursesController _controller;
//        private readonly Mock<IStudentEnrolmentContext> _mockContext;

//        public CoursesControllerTest()
//        {
//            _mockContext = new Mock<IStudentEnrolmentContext>();
//            var mockSet = new Mock<DbSet<Courses>>();

//            _mockContext.Setup(m => m.Course).Returns(mockSet.Object);
//            _controller = new CoursesController(_mockContext.Object);
//        }

//        [Fact]
//        public async Task GetCourse_Returns_All_Courses()
//        {
//            // Arrange
//            var courses = new List<Courses>
//            {
//                new("Math", "Advanced mathematics course"),
//                new("Science", "General science course")
//            };

//            var mockSet = new Mock<DbSet<Courses>>();

//            mockSet.As<IAsyncEnumerable<Courses>>()
//                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
//                .Returns(new TestAsyncEnumerator<Courses>(courses.GetEnumerator()));

//            mockSet.As<IQueryable<Courses>>()
//                .Setup(m => m.Provider)
//                .Returns(new TestAsyncQueryProvider<Courses>(courses.AsQueryable().Provider));

//            mockSet.As<IQueryable<Courses>>().Setup(m => m.Expression).Returns(courses.AsQueryable().Expression);
//            mockSet.As<IQueryable<Courses>>().Setup(m => m.ElementType).Returns(courses.AsQueryable().ElementType);
//            mockSet.As<IQueryable<Courses>>().Setup(m => m.GetEnumerator()).Returns(courses.GetEnumerator());

//            _mockContext.Setup(c => c.Course).Returns(mockSet.Object);

//            // Act
//            var result = await _controller.GetCourse();

//            // Assert
//            var actionResult = Assert.IsType<ActionResult<IEnumerable<Courses>>>(result);
//            var returnValue = Assert.IsType<List<Courses>>(actionResult.Value);
//            Assert.Equal(2, returnValue.Count);
//        }

//        [Fact]
//        public async Task GetCourse_Returns_Course_By_Id()
//        {
//            // Arrange
//            var courseId = 0;
//            var course = new Courses("Maths", "Advanced mathematics course");

//            _mockContext.Setup(c => c.Course.FindAsync(courseId))
//                .ReturnsAsync(course);

//            // Act
//            var result = await _controller.GetCourse(courseId);

//            // Assert
//            var actionResult = Assert.IsType<ActionResult<Courses>>(result);
//            var returnValue = Assert.IsType<Courses>(actionResult.Value);
//            Assert.Equal(courseId, returnValue.CourseId);
//        }

//        [Fact]
//        public async Task GetCourse_Returns_NotFound_For_Invalid_Id()
//        {
//            // Arrange
//            var courseId = 999;

//            _mockContext.Setup(c => c.Course.FindAsync(courseId))
//                .ReturnsAsync((Courses)null);

//            // Act
//            var result = await _controller.GetCourse(courseId);

//            // Assert
//            Assert.IsType<NotFoundResult>(result.Result);
//        }

//        [Fact]
//        public async Task PostCourse_Creates_Course()
//        {
//            // Arrange
//            var course = new Courses("New course", "Course description");

//            // Act
//            var result = await _controller.PostCourse(course);

//            // Assert
//            var actionResult = Assert.IsType<ActionResult<Courses>>(result);
//            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
//            var returnValue = Assert.IsType<Courses>(createdAtActionResult.Value);
//            Assert.Equal(course.CourseName, returnValue.CourseName);
//        }

//        [Fact]
//        public async Task DeleteCourse_Removes_Course()
//        {
//            // Arrange
//            var courseId = 1;
//            var course = new Courses("Maths","Mathematics advanced courses");

//            _mockContext.Setup(c => c.Course.FindAsync(courseId))
//                .ReturnsAsync(course);

//            // Act
//            var result = await _controller.DeleteCourse(courseId);

//            // Assert
//            Assert.IsType<NoContentResult>(result);
//            _mockContext.Verify(c => c.Course.Remove(course), Times.Once);
//            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
//        }

//        [Fact]
//        public async Task DeleteCourse_Returns_NotFound_For_Invalid_Id()
//        {
//            // Arrange
//            var courseId = 999;

//            _mockContext.Setup(c => c.Course.FindAsync(courseId))
//                .ReturnsAsync((Courses)null);

//            // Act
//            var result = await _controller.DeleteCourse(courseId);

//            // Assert
//            Assert.IsType<NotFoundResult>(result);
//        }
//    }
//}