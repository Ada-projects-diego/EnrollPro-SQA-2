//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using EnrolPro.src.Controllers;
//using EnrolPro.src.Data;
//using EnrolPro.src.Models;
//using Microsoft.EntityFrameworkCore.Query;
//using System.Linq.Expressions;

//namespace EnrolPro.Tests.Controllers
//{
//    public class StudentsControllerTest
//    {
//        private readonly StudentsController _controller;
//        private readonly Mock<IStudentEnrolmentContext> _mockContext;

//        public StudentsControllerTest()
//        {
//            _mockContext = new Mock<IStudentEnrolmentContext>();
//            var mockSet = new Mock<DbSet<Students>>();

//            _mockContext.Setup(m => m.Student).Returns(mockSet.Object);
//            _controller = new StudentsController(_mockContext.Object);
//        }

//        [Fact]
//        public async Task GetStudent_Returns_All_Students()
//        {
//            // Arrange
//            var students = new List<Students>
//            {
//                new Students("John", "Doe"),
//                new Students("Jane", "Doe")
//            };

//            var mockSet = new Mock<DbSet<Students>>();

//            mockSet.As<IAsyncEnumerable<Students>>()
//                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
//                .Returns(new TestAsyncEnumerator<Students>(students.GetEnumerator()));

//            mockSet.As<IQueryable<Students>>()
//                .Setup(m => m.Provider)
//                .Returns(new TestAsyncQueryProvider<Students>(students.AsQueryable().Provider));

//            mockSet.As<IQueryable<Students>>().Setup(m => m.Expression).Returns(students.AsQueryable().Expression);
//            mockSet.As<IQueryable<Students>>().Setup(m => m.ElementType).Returns(students.AsQueryable().ElementType);
//            mockSet.As<IQueryable<Students>>().Setup(m => m.GetEnumerator()).Returns(students.GetEnumerator());

//            _mockContext.Setup(c => c.Student).Returns(mockSet.Object);

//            // Act
//            var result = await _controller.GetStudent();

//            // Assert
//            var actionResult = Assert.IsType<ActionResult<IEnumerable<Students>>>(result);
//            var returnValue = Assert.IsType<List<Students>>(actionResult.Value);
//            Assert.Equal(2, returnValue.Count);
//        }

//        [Fact]
//        public async Task GetStudent_Returns_Student_By_Id()
//        {
//            // Arrange
//            var studentId = 1;
//            var student = new Students("John", "Doe") { StudentId = studentId };

//            _mockContext.Setup(c => c.Student.FindAsync(studentId))
//                .ReturnsAsync(student);

//            // Act
//            var result = await _controller.GetStudent(studentId);

//            // Assert
//            var actionResult = Assert.IsType<ActionResult<Students>>(result);
//            var returnValue = Assert.IsType<Students>(actionResult.Value);
//            Assert.Equal(studentId, returnValue.StudentId);
//        }

//        [Fact]
//        public async Task GetStudent_Returns_NotFound_For_Invalid_Id()
//        {
//            // Arrange
//            var studentId = 999;

//            _mockContext.Setup(c => c.Student.FindAsync(studentId))
//                .ReturnsAsync((Students)null);

//            // Act
//            var result = await _controller.GetStudent(studentId);

//            // Assert
//            Assert.IsType<NotFoundResult>(result.Result);
//        }

//        [Fact]
//        public async Task PostStudent_Creates_Student()
//        {
//            // Arrange
//            var student = new Students("New", "Student");

//            // Act
//            var result = await _controller.PostStudent(student);

//            // Assert
//            var actionResult = Assert.IsType<ActionResult<Students>>(result);
//            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
//            var returnValue = Assert.IsType<Students>(createdAtActionResult.Value);
//            Assert.Equal(student.FirstName, returnValue.FirstName);
//            Assert.Equal(student.LastName, returnValue.LastName);
//        }

//        [Fact]
//        public async Task DeleteStudent_Removes_Student()
//        {
//            // Arrange
//            var studentId = 1;
//            var student = new Students("John", "Doe") { StudentId = studentId };

//            _mockContext.Setup(c => c.Student.FindAsync(studentId))
//                .ReturnsAsync(student);

//            // Act
//            var result = await _controller.DeleteStudent(studentId);

//            // Assert
//            Assert.IsType<NoContentResult>(result);
//            _mockContext.Verify(c => c.Student.Remove(student), Times.Once);
//            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
//        }

//        [Fact]
//        public async Task DeleteStudent_Returns_NotFound_For_Invalid_Id()
//        {
//            // Arrange
//            var studentId = 999;

//            _mockContext.Setup(c => c.Student.FindAsync(studentId))
//                .ReturnsAsync((Students)null);

//            // Act
//            var result = await _controller.DeleteStudent(studentId);

//            // Assert
//            Assert.IsType<NotFoundResult>(result);
//        }
//    }

//    internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
//    {
//        private readonly IQueryProvider _inner;

//        internal TestAsyncQueryProvider(IQueryProvider inner)
//        {
//            _inner = inner;
//        }

//        public IQueryable CreateQuery(Expression expression)
//        {
//            return new TestAsyncEnumerable<TEntity>(expression);
//        }

//        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
//        {
//            return new TestAsyncEnumerable<TElement>(expression);
//        }

//        public object Execute(Expression expression)
//        {
//            return _inner.Execute(expression);
//        }

//        public TResult Execute<TResult>(Expression expression)
//        {
//            return _inner.Execute<TResult>(expression);
//        }

//        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
//        {
//            var expectedResultType = typeof(TResult).GetGenericArguments()[0];
//            var executionResult = typeof(IQueryProvider)
//                .GetMethod(
//                    name: nameof(IQueryProvider.Execute),
//                    genericParameterCount: 1,
//                    types: new[] { typeof(Expression) })
//                .MakeGenericMethod(expectedResultType)
//                .Invoke(this, new[] { expression });

//            return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))
//                .MakeGenericMethod(expectedResultType)
//                .Invoke(null, new[] { executionResult });
//        }
//    }

//    internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
//    {
//        public TestAsyncEnumerable(IEnumerable<T> enumerable)
//            : base(enumerable)
//        { }

//        public TestAsyncEnumerable(Expression expression)
//            : base(expression)
//        { }

//        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
//        {
//            return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
//        }

//        IQueryProvider IQueryable.Provider
//        {
//            get { return new TestAsyncQueryProvider<T>(this); }
//        }
//    }

//    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
//    {
//        private readonly IEnumerator<T> _inner;

//        public TestAsyncEnumerator(IEnumerator<T> inner)
//        {
//            _inner = inner;
//        }

//        public T Current
//        {
//            get
//            {
//                return _inner.Current;
//            }
//        }

//        public ValueTask<bool> MoveNextAsync()
//        {
//            return new ValueTask<bool>(_inner.MoveNext());
//        }

//        public ValueTask DisposeAsync()
//        {
//            _inner.Dispose();
//            return new ValueTask();
//        }
//    }
//}