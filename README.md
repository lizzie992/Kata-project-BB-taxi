BB Taxi - Baierbrunn Carpool Coordination Platform

A real-time carpooling solution for Baierbrunn office employees, addressing the challenges of unreliable public transportation in the Munich metropolitan area.


The Problem
Baierbrunn's location outside Munich presents unique commuting challenges:

Limited public transport: Only one S-Bahn line connects to the office
Frequent disruptions: Deutsche Bahn delays, weather conditions, and maintenance work regularly interrupt service
Remote work constraints: On-site presence is often required
Coordination difficulties: Finding colleagues who share similar routes and schedules in a 100+ employee company

The existing solution—a static Excel spreadsheet—had significant limitations:
Outdated information and forgotten entries
No real-time updates for daily schedule changes, holidays, or sick leave
Lack of mobile accessibility during emergencies
No built-in communication tools
Poor user experience when urgent coordination is needed


The Solution
BB Taxi transforms carpooling coordination into a dynamic, user-friendly experience. Think of it as a localized ride-sharing platform specifically designed for the Baierbrunn workforce—where colleagues can offer and request rides with real-time information, interactive maps, and instant communication.


Secure Access

Password-protected registration ensuring only company employees can join
Admin moderation system with user warnings and deactivation capabilities
Content reporting system for ads and chat conversations


Smart Ad System

Post rides as driver or passenger with customizable details
Interactive Google Maps integration for precise pickup location marking
Profile defaults for streamlined ad creation (saved addresses, seat capacity, etc.)
Visual map overview showing all available rides at a glance


Intelligent Matching

Personalized recommendations that analyze:
Matching roles (drivers ↔ passengers)
Overlapping routes and shared journey segments
Compatible time schedules
Proximity of pickup locations
Email notifications for new matching ads


Built-in Chat System

Real-time messaging between users
Email notifications for new messages
Event-driven architecture for instant communication
Mobile-accessible for on-the-go coordination


Multilingual Support

Full localization in German and English
Seamless language switching for better user experience



Technical Stack

Frontend

Blazor (Server-side) - Modern web UI framework with C# and .NET
HTML5 & CSS3 - Responsive and accessible design
JavaScript - Enhanced interactivity for:
Google Maps API integration
Modal popup windows
Chat auto-scroll functionality

Backend

C#/.NET - Core application logic and business rules
ASP.NET Core - Web framework and server infrastructure
Entity Framework Core - ORM for database operations

Database

MySQL - Relational database for persistent data storage


Key Technical Features

Localization (i18n) - Resource-based multilingual support
Real-time messaging - Custom-built chat system with event handlers
Geolocation services - Google Maps API for route visualization and address mapping
Email notifications - Automated alerts for matches and messages
Role-based access control - Admin and user permission management
Responsive design - Mobile-first approach for accessibility


Architecture Highlights

Component-based architecture leveraging Blazor's component model
Event-driven messaging for real-time chat functionality
Database-first design with normalized data structures
Security layers including authentication, authorization, and content moderation
Recommendation algorithm for intelligent ride matching based on spatial and temporal data


Getting Started
Prerequisites
bash.NET 6.0 SDK or later
MySQL Server 8.0+
Google Maps API key
Installation
bash# Clone the repository
git clone https://github.com/lizzie992/Kata-project-BB-taxi.git

# Navigate to project directory
cd Kata-project-BB-taxi

# Restore dependencies
dotnet restore

# Update database connection string in appsettings.json
# Add your Google Maps API key

# Apply database migrations
dotnet ef database update

# Run the application
dotnet run



Screenshots
<img width="1906" height="718" alt="image" src="https://github.com/user-attachments/assets/af624114-b6b8-4b30-8bd9-67aa1a6256e9" />
<img width="1553" height="605" alt="image" src="https://github.com/user-attachments/assets/53773492-6bd3-4863-88ab-2c667c9c1017" />
<img width="1548" height="727" alt="image" src="https://github.com/user-attachments/assets/d83de371-6d12-4a55-80db-a89a323961e3" />




Future Enhancements

Integration with company calendar systems
Reputation system for drivers and passengers
Recurring ride schedules with exception handling
Analytics dashboard for admin monitoring



About This Project
This is my first full-stack web application, developed to solve a real-world problem affecting my colleagues and myself. The project demonstrates:

Problem-solving skills: Identifying pain points and designing practical solutions
Full-stack development: From database design to interactive UI
Real-time communication: Building a chat system from scratch
User experience focus: Intuitive design with multilingual support
Security awareness: Implementing authentication, authorization, and moderation
API integration: Working with third-party services (Google Maps)
Modern web technologies: Leveraging Blazor for efficient C#-based web development



License & Copyright
Copyright © 2024-2025 Katalin Gulyas. All Rights Reserved.
This project is proprietary software. Unauthorized copying, modification, distribution, or use of this software, via any medium, is strictly prohibited without explicit written permission from the copyright holder.
Restrictions

❌ No commercial use
❌ No redistribution
❌ No modification without permission
❌ No derivative works

For licensing inquiries or permission requests, please contact gulyaskata99@gmail.com .



Acknowledgments

Grateful to Florian Rakete and the entire Rakete Mentoring team for their guidance and support throughout this learning journey 
Inspired by the original "Baxi" Excel initiative by a colleague
Built for the Baierbrunn office community
Special thanks to all colleagues who provided feedback during development



Contact
Developer: Katalin Gulyas
Email: gulyaskata99@gmail.com
LinkedIn: Coming soon
GitHub: @lizzie992


Built with ❤️ and caffeine in Bavaria ☕🥨
