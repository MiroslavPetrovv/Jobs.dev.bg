Job Application Web Application

📖 Information Source - How It Works
The Job Application Web Application is designed to streamline the job application process for both applicants and employers. It provides a user-friendly interface for job seekers to find and apply for jobs, while allowing employers to post job listings and manage applications efficiently.

🚀 Features
User Authentication: Secure login and registration for both applicants and employers.
Job Listings: Employers can create, update, and delete job postings.
Application Submission: Applicants can submit their applications, including uploading their CVs.
Application Status Tracking: Applicants can track the status of their applications.
Dashboard: Separate dashboards for applicants and employers to manage their activities.
Responsive Design: The application is fully responsive, ensuring a seamless experience on all devices.

🛠️ Technologies Used
Backend: ASP.NET Core
Database: Entity Framework Core with an in-memory database for testing
Frontend: HTML, CSS, JavaScript, and Bootstrap for responsive design
Authentication: ASP.NET Core Identity for user management
API: RESTful APIs for data handling


📦 Installation
Clone the Repository:

Seed the Database: The application includes a method to seed initial user accounts. The following accounts will be created:

Applicant: firstUser @abv.bg with password Qw12234@
Company: Company@abv.bg with password Wq12345@
Admin: Admin@abv.bg with password Eq12345@
To seed the accounts, ensure that the Seed method is called in your application startup. This method checks if the users already exist and creates them if they do not.

git clone https://github.com/yourusername/job-application-web-app.git
cd job-application-web-app

Install Dependencies: Make sure you have the .NET SDK installed. Run the following command to restore the required packages:


dotnet restore
Set Up the Database: Run the following command to apply migrations and set up the database:

bash

Verify


dotnet ef database update
Run the Application: Start the application using:


dotnet run
Access the Application: Open your web browser and navigate to http://localhost:5000 to access the application.

🔍 Usage
For Applicants:

Register for an account and log in.
Browse job listings and apply for positions.
Upload your CV and track the status of your applications.
For Employers:

Register for an employer account and log in.
Create new job postings and manage existing ones.
Review applications and update their status.
🔒 Security
The application implements best practices for security, including:

Password hashing and salting using ASP.NET Core Identity.
Role-based access control to restrict access to certain features.
Input validation to prevent SQL injection and cross-site scripting (XSS) attacks.
🌐 Deployment
The application can be deployed to cloud services such as Azure or AWS. Follow the respective documentation for deploying ASP.NET Core applications.

📄 License
This project is licensed under the MIT License. See the LICENSE file for more details.

🤝 Contributing
Contributions are welcome! Please fork the repository and submit a pull request for any enhancements or bug fixes.

📞 Contact
For any inquiries or feedback, please reach out to [your-email@example.com].
