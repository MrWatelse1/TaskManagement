TASK MANAGEMENT SYSTEM
 The Task Management System is a backend API designed to help users manage tasks, projects, and receive notifications. 
This README provides an overview of the project, instructions on setting up and running the API, and important information for developers.

Table of Contents
- Features
- Requirements
- Installation
- Configuration
- API Endpoints
- Error Handling
- Contributing

Features
- CRUD operations for Tasks, Projects, Users, and Notifications.
- Filter tasks based on status or priority.
- Retrieve tasks due for the current week.
- Assign tasks to projects or remove them.
- Mark notifications as read/unread.
- Automatic notifications for due dates, task completions, and assignments.


Requirements
- .NET Core SDK
- Entity Framework Core
- SQL Server (or another compatible database)
- AutoMapper

Installation
1. Clone the repository to your local machine:
	git clone https://github.com/MrWatelse1/TaskManagement.git
2. Navigate to the project directory:
	cd TaskManagement
3. Install dependencies using NuPackage Manager

Configuration
Configure your database connection in appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TaskManagement;Trusted_Connection=True;Encrypt=False"
  },
} #for local connection


Error Handling
The API provides proper error handling with descriptive error messages and HTTP status codes for various scenarios, including validation errors and not found errors.


API DOCUMENTATION

1. Projects
Create a Project
Endpoint: POST api/Project/CreateProject
Description: Allows users to create a new project.

Get All Projects
Endpoint: GET api/Project/GetAllProjects
Description: Retrieves a list of all projects.

Get Project by ID
Endpoint: GET api/Project/GetProjectById
Description: Retrieves a specific project by its ID.

Update Project
Endpoint: PUT api/Project/UpdateProject
Description: Allows users to update the details of a specific project by its ID.

Delete Project
Endpoint: DELETE api/Project/DeleteProject
Description: Allows users to delete a specific project by its ID.


2. Users
Create a User
Endpoint: POST api/Authentication/register
Description: Allows users to create a new user.
Request Body: JSON object representing user information (e.g., name, email).

Login User
Endpoint: GET api/Authentication/login
Description: Login a specific user with their email and password.


Get Logged In User
Endpoint: GET api/ApiBase/User
Description: Retrieves teh logged in user.

Update User
Endpoint: PUT /api/Authentication/updateUser
Description: Allows users to update the details of a specific user by their ID.

Delete User
Endpoint: DELETE /api/Authentication/{id}
Description: Allows users to delete a specific user by their ID.

4. Notifications

Get Logged In User
Endpoint: GET /api/Notification/GetUserAsync
Description: Retrieves the user making the request.

Create a Notification
Endpoint: POST /api/Notification/CreateNotification
Description: Allows the user use the system to create and send notifications.

Get All Notifications for User
Endpoint: GET /api/Notification/GetAllNotifications
Description: Retrieves all notifications for a specific user.

Get Notification By Id
Endpoint: GET /api/Notification/GetNotificationById
Description: Retrieves the notifications with the provided Id.

Update Notification
Endpoint: POST /api/Notification/UpdateNotification
Description: Allows the user update a notification.

Delete Notification
Endpoint: DELETE /api/Notification/DeleteNotification
Description: Allows the user delete a notification using the Id.

Mark Notification as Read/Unread
Endpoint: PUT /api/Notification/MarkNotificationAsReadOrUnread
Description: Allows users to mark a specific notification as read or unread using the readStatus boolean variable.


5. Tasks
Create a Task
Endpoint: POST api/Task/CreateTask
Description: Allows users to create a new task.

Get All Tasks
Endpoint: GET api/Task/GetAllTasks
Description: Retrieves a list of all tasks.

Get Task by ID
Endpoint: GET api/Task/GetTaskById
Description: Retrieves a specific task by its ID.

Update Task
Endpoint: PUT api/Task/UpdateTask
Description: Allows users to update the details of a specific task by its ID.

Delete Task
Endpoint: DELETE api/Task/DeleteTask
Description: Allows users to delete a specific task by its ID.

5. Additional Endpoints
Fetch Tasks by Status or Priority
Endpoint: POST /api/Task/GetTasksByStatus
Description: Retrieves a list of tasks based on status or priority using provided integer values priority (e.g., low=0, medium=1, high=2), and status (e.g., pending = 0, in-progress =1, completed=2).

Fetch Tasks Due for the Current Week
Endpoint: GET /api/Task/GetTasksDueThisWeek
Description: Retrieves tasks that are due for the current week.

Assign or Remove Task from Project
Endpoint: PUT /api/Task/AssignTaskToProject
Description: Allows users to assign or remove a task from a project.

Request Body: JSON object indicating the project assignment.
These endpoints cover the main functionality of the Task Management System