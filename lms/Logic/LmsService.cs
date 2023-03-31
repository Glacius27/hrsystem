using System;
using lms.DB;
using lms.Models;
using shraredclasses.Commands;

namespace lms.Logic
{
	public class LmsService
	{
        public async Task AddWelcomeCourse(CreateLearningTrackRequest createLearningTrackRequest)
        {
            var course = new Course()
            {
                EmployeeID = createLearningTrackRequest.EmployeeId,
                CourseID = Guid.NewGuid().ToString(),
                Status = "In progress"
            };

            using (CourseContext db = new CourseContext())
            {
                await db.AddAsync(course);
                await db.SaveChangesAsync();
            }
        }
    }
}

