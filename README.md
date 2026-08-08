# Baxi: an efficient and modern solution for the everyday challenges of getting to Baierbrunn

[![License: All Rights Reserved](https://img.shields.io/badge/License-All%20Rights%20Reserved-red.svg)](https://github.com/lizzie992/Baxi/blob/master/LICENSE)

Baxi was created to fill the need for reliable transport options to get to/from the office whenever public transport is not available. It offers a protected ride-sharing platform where colleagues at a company can safely find potential drivers or passengers.

## Features

As a user-friendly application, Baxi offers:

- **The possibility to post ads** — both as passenger or driver — in order to find or offer a ride from / to work
- **Browsing through all ads** with an interactive map visualizing all options
- **Matching ad recommendations** based on efficient calculation of overlapping routes and distances
- **A built-in chat** for quick and safe contact with other users regarding a posted ad
- **An efficient email notification system**
- **Safety features**, including password-protected registration and reporting options where admins can take action (deleting ads, warning or banning users)
- **Language selection** to support all cultures

## Demo Videos

<table>
<tr>
<td align="center" width="33%">
<a href="https://www.youtube.com/watch?v=1QEUl1pSrtI">
<img src="https://img.youtube.com/vi/1QEUl1pSrtI/maxresdefault.jpg" width="100%" alt="Watch the English demo" />
<br />
<strong>▶ English</strong>
</a>
</td>
<td align="center" width="33%">
<a href="https://www.youtube.com/watch?v=5GR0aDssb9s">
<img src="https://img.youtube.com/vi/5GR0aDssb9s/maxresdefault.jpg" width="100%" alt="Watch the German demo" />
<br />
<strong>▶ German</strong>
</a>
</td>
<td align="center" width="33%">
<a href="https://www.youtube.com/watch?v=4YBxYwHhnbE">
<img src="https://img.youtube.com/vi/4YBxYwHhnbE/maxresdefault.jpg" width="100%" alt="Watch the Hungarian demo" />
<br />
<strong>▶ Hungarian</strong>
</a>
</td>
</tr>
</table>

## Tech Stack

- **C# / .NET** — core application language and runtime
- **ASP.NET Core** — web application framework
- **Blazor** — component-based UI, written entirely in C# rather than JavaScript
- **Entity Framework Core** — ORM and data access layer
- **MySQL** — relational database
- **HTML5 & CSS3**
- **JavaScript** — used selectively for browser interop (see below)
- **Google Maps API** — geolocation and route visualization

## How It Works

**Matching engine.** When a user posts an ad, Baxi compares its route against all open ads in the opposite role (driver ↔ passenger) and scores them by geographic overlap and distance, rather than requiring an exact start/end match. This surfaces rides that are a good-enough fit — for example a driver whose route passes near a passenger's stop — instead of only exact matches, which would make the platform far less useful given how few colleagues share an identical commute.

**Real-time chat.** Built with event handlers rather than polling, so messages appear immediately without the client repeatedly asking the server for updates. This keeps the app responsive without adding unnecessary server load.

**Geolocation.** Integrated with the Google Maps JavaScript API for interactive map browsing and distance/route calculations. Since Blazor components are C#-first, the Maps JS SDK is loaded and called via JavaScript interop — one of the few places in the app where JavaScript is used directly.

**Safety and moderation.** Registration is password-protected, and a reporting system lets admins take action on flagged ads or users (removal, warnings, bans) without needing direct database access.

**Localization.** UI strings are built to support multiple languages from the start, reflecting the multicultural makeup of the intended user base.

### What I'd improve next

- Move the ride-matching calculation to a background job for larger ad volumes, rather than computing it on request
- Add automated tests around the matching logic, which is currently the most complex part of the app and the easiest to regress
- Replace the current email notification setup with a queued/retryable delivery system

## Screenshots

**Browsing ads, with an interactive map and filters for direction, address, date, and seats**
<img width="1597" height="719" alt="image" src="https://github.com/user-attachments/assets/04ec909d-f4e1-4091-8471-58ea643e676d" />

**Posting a ride ad — pin-drop location picker for exact pickup/drop-off**
<img width="1580" height="600" alt="image" src="https://github.com/user-attachments/assets/ed13945d-ea4a-48b1-85b2-aa046b2371ee" />

**Admin panel for user management, filtering, and moderation actions**
<img width="1523" height="729" alt="image" src="https://github.com/user-attachments/assets/568b83d6-7bef-411b-9db9-ea9342b6bdb5" />


## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (see `global.json` or the `.csproj` file for the exact version)
- [MySQL](https://dev.mysql.com/downloads/) running locally or accessible remotely
- A [Google Maps API key](https://developers.google.com/maps/documentation/javascript/get-api-key)

### Setup

1. Clone the repository
```bash
   git clone https://github.com/lizzie992/Baxi.git
   cd Baxi
```
2. Configure your connection string and Google Maps API key in `appsettings.json` (or as user secrets / environment variables — do not commit real keys)
3. Apply database migrations
```bash
   dotnet ef database update
```
4. Run the application
```bash
   dotnet run
```

## Contact

**Developer**: Katalin Gulyás
**Email**: gulyaskata99@gmail.com
**GitHub**: [@lizzie992](https://github.com/lizzie992)
**LinkedIn**: [linkedin.com/in/katalin-gulyas](https://www.linkedin.com/in/katalin-gulyas/)

## Acknowledgements

Grateful for Florian and all members of Rakete Mentoring for their endless support.

## License & Copyright

**Copyright © 2024–2025 Katalin Gulyás. All Rights Reserved.**

This project is proprietary software. Unauthorized copying, modification, distribution, or use of this software, via any medium, is strictly prohibited without explicit written permission from the copyright holder.

### Restrictions

- ❌ No commercial use
- ❌ No redistribution
- ❌ No modification without permission
- ❌ No derivative works

For licensing inquiries or permission requests, please contact gulyaskata99@gmail.com
